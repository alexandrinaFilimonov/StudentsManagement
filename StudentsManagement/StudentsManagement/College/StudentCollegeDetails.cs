using StudentsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.College
{
    public class StudentCollegeDetails
    {
        public Student Student { get; set; }

        public int Credits { get; set; }

        public int Average { get; set; }

        public IEnumerable<StudentSubjectJoin> ExaminationResults { get; set; }
    }
}