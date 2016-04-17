using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.Models
{
    public class DetailsQuery
    {
        public int StudentId { get; set; }

        public int AcademicYear { get; set; }

        public int Semester { get; set; }
    }
}