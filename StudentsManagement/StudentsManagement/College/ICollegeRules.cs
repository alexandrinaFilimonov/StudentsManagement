using StudentsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManagement.College
{
    public interface ICollegeRules
    {
        IEnumerable<StudentBudgetStatus> GetBugetStudents(IEnumerable<Student> allStudents);
      
        IEnumerable<StudentPromotionDetails> GetPromotionDetails(IEnumerable<Student> students);

        StudentCollegeDetails GetStudentCollgeStatus(Student student, int academicYear, int semester);

        int GetTotalCreditsPerYear(Student student, int academicYear, int semester);
      
    }
}
