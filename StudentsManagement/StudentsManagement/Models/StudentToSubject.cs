using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.Models
{
    public class StudentToSubject 
    {
        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public int Grade { get; set; }
    }
}