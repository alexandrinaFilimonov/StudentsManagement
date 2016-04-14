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
                var numberOfCredits = GetTotalCreditsPerYear(student);
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

        internal static IEnumerable<StudentPromotionDetails> GetPromotionDetails(IEnumerable<Student> students)
        {
            var studentPromotionDetails = new List<StudentPromotionDetails>();

            foreach(var student in students){
                var promoted = !(student.SubjectsList.Where(subject => subject.Item1.Grade < 5).Count() > 6);
                var studentPromotionDetail = new StudentPromotionDetails
                {
                    Student = student,
                    Promoted = promoted
                };

                studentPromotionDetails.Add(studentPromotionDetail);
            }

            return studentPromotionDetails;
        }

        public int GetTotalCreditsPerYear(Student student) {
            return student.SubjectsList
                .Where(subject => subject.Item2.StudyYear.Equals(AcademicYear) && subject.Item2.Semester.Equals(Semester))
                .Sum(subject => subject.Item1.Grade * subject.Item2.NumberOfCredits); 
        }     
    }
}