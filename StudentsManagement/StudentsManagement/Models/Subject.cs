using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.Models
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StudyYear { get; set; }

        public int Semester { get; set; }

        public int NumberOfCredits { get; set; }
    }
}