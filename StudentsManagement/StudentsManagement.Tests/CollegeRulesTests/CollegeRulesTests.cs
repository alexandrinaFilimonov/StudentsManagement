using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentsManagement.Models;
using System.Collections.Generic;
using StudentsManagement.College;
using System.Linq;

namespace StudentsManagement.Tests.CollegeRulesTests
{
    [TestClass]
    public class CollegeRulesTests
    {
        private Random random = new Random();
        private readonly int credits = 5;
        private readonly int semester = 2;
        private readonly int academicYear = 2;

        [TestMethod]
        public void TestGetTotalCreditsPerYear()
        {
            // Arrange: create data
            var numberOfSubjects = 4;
            
            var subjects = GetSubjectsForThisSemester(numberOfSubjects);
            var student = GetStudentWithSubjects(subjects);
            var collegeRules = new CollegeRules(academicYear, semester);

            // Arrange: prepare input
            var subjectPreviousYear = subjects.ElementAt(0);
            subjectPreviousYear.Semester = semester - 1;
            subjectPreviousYear.StudyYear = academicYear - 1;

            var subjectNextYear = subjects.ElementAt(3);
            subjectNextYear.Semester = semester + 1;
            subjectNextYear.StudyYear = academicYear + 1;

            var subjectsThisSemester = student.SubjectsList.Skip(1).Take(2);
                               
            var expectedTotalCredits = 0;
            foreach(var subjectThisSemester in subjectsThisSemester){
                expectedTotalCredits += subjectThisSemester.Grade * subjectThisSemester.Subject.Credits;
            }

            // Act
            var totalCredits = collegeRules.GetTotalCreditsPerYear(student, academicYear, semester);

            // Assert 
            Assert.AreEqual(expectedTotalCredits, totalCredits, "The number of credits is not equal to the expected one");
        }

        [TestMethod]
        public void TestGetTotalCreditsPerYearWrongSemesters()
        {
            // Arrange: create data
            var semester = 2;
            var academicYear = 2;

            var subjects = GetSubjectsForThisSemester(2);
            var student = GetStudentWithSubjects(subjects);
            var collegeRules = new CollegeRules(academicYear, semester);

            // Arrange: prepare input
            var subjectPreviousYear = subjects.ElementAt(0);
            subjectPreviousYear.Semester = semester - 1;
            subjectPreviousYear.StudyYear = academicYear - 1;

            var subjectNextYear = subjects.ElementAt(1);
            subjectNextYear.Semester = semester + 1;
            subjectNextYear.StudyYear = academicYear + 1;

            // Act
            var totalCredits = collegeRules.GetTotalCreditsPerYear(student, academicYear, semester);

            // Assert 
            Assert.AreEqual(0, totalCredits, "The number of credits is not equal to the expected one");
        }

        [TestMethod]
        public void TestGetBugetedStudents()
        {
            // Arrange: create data
            var numberOfStudents = 5;
            var students = new List<Student>();

            var subjects = GetSubjectsForThisSemester(5);
            for (var i = 0; i < numberOfStudents; i++)
            {
                var student = GetStudentWithSubjects(subjects);
                students.Add(student);
            }

            var collegeRules = new CollegeRules(academicYear, semester);

            // Arrange: prepare input
            var grade = 5;
            foreach (var student in students)
            {
                foreach(var subjectStudent in student.SubjectsList){
                    subjectStudent.Grade = grade;
                }
                grade++;
            }

            var expectedNumberOfFinancedStudents = numberOfStudents * 6 / 10;
            var expectedNumberOfFeePayingStudents = numberOfStudents - expectedNumberOfFinancedStudents;
            
            // Act
            var bugetStudents = collegeRules.GetBugetStudents(students);

            // Assert 
            var expectedFinancedStudents = bugetStudents
                .OrderByDescending(student => student.Credits)
                .Take(expectedNumberOfFinancedStudents);

            var firstSegmentShouldBeFinanced = expectedFinancedStudents.All(student => student.Financed);
            var lastSegmentShouldBeFeePayers = bugetStudents.Except(expectedFinancedStudents).All(student => !student.Financed);

            Assert.IsTrue(firstSegmentShouldBeFinanced, string.Format("{0} students shoud've been promoted", expectedNumberOfFinancedStudents));
            Assert.IsTrue(lastSegmentShouldBeFeePayers, string.Format("{0} students shoud've failed", expectedNumberOfFeePayingStudents));
        }

        [TestMethod]
        public void TestGetPromotionDetails()
        {
            // Arrange: create data
            var numberOfStudents = 3;
            var students = new List<Student>();

            var subjects = GetSubjectsForThisSemester(12);
            for (var i = 0; i < numberOfStudents; i++)
            {
                var student = GetStudentWithSubjects(subjects);
                students.Add(student);
            }

            var collegeRules = new CollegeRules(academicYear, semester);

            // Arrange: prepare input
            var failStudent = students.ElementAt(0);
            SetPromotedSubjects(failStudent, 5);
            var nearlyPromotedStudent = students.ElementAt(1);
            SetPromotedSubjects(nearlyPromotedStudent, 6);
            var promotedStudent = students.ElementAt(2);
            SetPromotedSubjects(promotedStudent, 7);

            // Act
            var promotionDetails = collegeRules.GetPromotionDetails(students);

            // Assert 
            Assert.IsFalse(promotionDetails.First(student => student.Student.Id.Equals(failStudent.Id)).Promoted, "Student should've fail to promote");
            Assert.IsFalse(promotionDetails.First(student => student.Student.Id.Equals(nearlyPromotedStudent.Id)).Promoted, "Student should've fail to promote");
            Assert.IsTrue(promotionDetails.First(student => student.Student.Id.Equals(promotedStudent.Id)).Promoted,"Student should've promoted");
        }

        [TestMethod]
        public void TestGetStudentCompleteStatus()
        {
            // Arrange: create data
            var numberOfSubjects = 4;

            var subjects = GetSubjectsForThisSemester(numberOfSubjects);
            var student = GetStudentWithSubjects(subjects);
            var collegeRules = new CollegeRules(academicYear, semester);

            // Arrange: prepare input
            var expectedTotalCredits = 0;
            var gradesSum = 0;
            var subjectsThisYear = student.SubjectsList;
            foreach (var subject in student.SubjectsList)
            {
                expectedTotalCredits += subject.Grade * subject.Subject.Credits;
                gradesSum += subject.Grade;
            }
            var expectedAverage = gradesSum / subjectsThisYear.Count();

            // Act
            var studentDetails = collegeRules.GetStudentCollgeStatus(student, academicYear, semester);

            // Assert 
            Assert.AreEqual(expectedTotalCredits, studentDetails.Credits, "The number of credits is not equal to the expected one");
            Assert.AreEqual(expectedAverage, studentDetails.Average, "The Ects averae is not equal to the expected one");
            Assert.IsTrue(subjectsThisYear.SequenceEqual(studentDetails.ExaminationResults));
        }

        [TestMethod]
        public void TestGetStudentCompleteStatusIncludingPreviousSubjects()
        {
            // Arrange: create data
            var numberOfSubjects = 4;

            var subjects = GetSubjectsForThisSemester(numberOfSubjects);
            var student = GetStudentWithSubjects(subjects);
            var collegeRules = new CollegeRules(academicYear, semester);

            // Arrange: prepare input
            var subjectPreviousYear = subjects.ElementAt(0);
            subjectPreviousYear.Semester = semester - 1;
            subjectPreviousYear.StudyYear = academicYear - 1;

            var subjectNextYear = subjects.ElementAt(3);
            subjectNextYear.Semester = semester + 1;
            subjectNextYear.StudyYear = academicYear + 1;

            var expectedTotalCredits = 0;
            var gradesSum = 0;
            var subjectsThisYear = student.SubjectsList.Skip(1).Take(2);
            foreach (var subject in subjectsThisYear)
            {
                expectedTotalCredits += subject.Grade * subject.Subject.Credits;
                gradesSum += subject.Grade;
            }
            var expectedAverage = gradesSum / subjectsThisYear.Count();

            // Act
            var studentDetails = collegeRules.GetStudentCollgeStatus(student, academicYear, semester);

            // Assert 
            Assert.AreEqual(expectedTotalCredits, studentDetails.Credits, "The number of credits is not equal to the expected one");
            Assert.AreEqual(expectedAverage, studentDetails.Average, "The Ects averae is not equal to the expected one");
            Assert.IsTrue(subjectsThisYear.SequenceEqual(studentDetails.ExaminationResults));
        }

        private static void SetPromotedSubjects(Student student, int numberOfPromotedSubjects)
        {
            var promotionGrade = 5;
            var failGrade = 4;
            var index = 0;
            foreach (var subjectStudent in student.SubjectsList)
            { 
                index++;
                if(numberOfPromotedSubjects >= index){
                    subjectStudent.Grade = promotionGrade;
                }
                else
                {
                    subjectStudent.Grade = failGrade;
                }
            }
        }

        private Student GetStudentWithSubjects(IEnumerable<Subject> subjects)
        {
            var id = random.Next(100);
            return new Student
            {
                Id = id,
                SubjectsList = GetSubjectList(id, subjects)
            };
        }

        private List<StudentSubjectJoin> GetSubjectList(int studetnId, IEnumerable<Subject> subjects)
        {
            var subjectList = new List<StudentSubjectJoin>();
            foreach(var subject in subjects){
                var grade = random.Next(9) + 1;
                var studentToSubject = new StudentToSubject {
                    StudentId = studetnId,
                    Grade = grade,
                    SubjectId = subject.Id };
                subjectList.Add(new StudentSubjectJoin(studentToSubject, subject));
            }

            return subjectList;
        }

        private IEnumerable<Subject> GetSubjectsForThisSemester(int numberOfSubjects)
        {
            var subjects = new List<Subject>();
            for (int i = 0; i < numberOfSubjects; i++)
            {
                var subject = new Subject
                {
                    Credits = credits,
                    Id = random.Next(100),
                    Semester = this.semester,
                    StudyYear = this.academicYear
                };
                subjects.Add(subject);
            }

            return subjects;
        }
    }
}
