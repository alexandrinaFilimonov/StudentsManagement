using StudentsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.College
{
    public class CollegeRules
    {
        public int AcademicYear { get; set; }
        public int Semester { get; set; }

        public const int FinancedPercentege = 60;

        public IEnumerable<StudentBudgetStatus> GetBugetStudents(IEnumerable<Student> allStudents)
        {
            var studentsBudgetDetails = new List<StudentBudgetStatus>();
            foreach (var student in allStudents)
            {
                var numberOfCredits = GetTotalCreditsPerYear(student, AcademicYear, Semester);
                var studentBudgetDetails = new StudentBudgetStatus
                {
                    Student = student,
                    Credits = numberOfCredits
                };
                studentsBudgetDetails.Add(studentBudgetDetails);
            }

            var numberOfFinancedStudents = allStudents.Count() * FinancedPercentege / 100;
            var financedStudents = studentsBudgetDetails
                .OrderByDescending(studentsDetails => studentsDetails.Credits)
                .Take(numberOfFinancedStudents);

            foreach(var financedStudent in financedStudents){
                financedStudent.Financed = true;
            }
            
            return studentsBudgetDetails;
        }

        public IEnumerable<StudentPromotionDetails> GetPromotionDetails(IEnumerable<Student> students)
        {
            var studentPromotionDetails = new List<StudentPromotionDetails>();

            foreach(var student in students){
                var promoted = HasStudentPromoted(student);
                var studentPromotionDetail = new StudentPromotionDetails
                {
                    Student = student,
                    Promoted = promoted
                };

                studentPromotionDetails.Add(studentPromotionDetail);
            }

            return studentPromotionDetails;
        }

        public StudentCollegeDetails GetStudentCollgeStatus(Student student, int academicYear, int semester)
        {
            var studentDetails = new StudentCollegeDetails
            {
                Student = student,
                Credits = GetTotalCreditsPerYear(student, academicYear, semester),
                Average = GetAveragePerYear(student, academicYear, semester),
                ExaminationResults = GetExaminationResultsPerYear(student, academicYear, semester)
            };
            
            return studentDetails;
        }

        private IEnumerable<StudentSubjectJoin> GetExaminationResultsPerYear(Student student, int academicYear, int semester)
        {
            return student.SubjectsList
                .Where(join => join.Subject.StudyYear.Equals(academicYear) && join.Subject.Semester.Equals(semester));
        }

        private int GetAveragePerYear(Student student, int academicYear, int semester)
        {
            return (int)student.SubjectsList
                .Where(join => join.Subject.StudyYear.Equals(academicYear) && join.Subject.Semester.Equals(semester))
                .Average(join => join.Grade);
        }

        public int GetTotalCreditsPerYear(Student student, int academicYear, int semester) {
            return student.SubjectsList
                .Where(join => join.Subject.StudyYear.Equals(academicYear) && join.Subject.Semester.Equals(semester))
                .Sum(join => join.Grade * join.Subject.Credits); 
        }

        private bool HasStudentPromoted(Student student)
        {
            return !(student.SubjectsList.Where(join => join.Grade < 5).Count() > 6);
        }
    }
}