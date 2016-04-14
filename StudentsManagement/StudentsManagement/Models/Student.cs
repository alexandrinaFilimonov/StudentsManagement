using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsManagement.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FathersInitial { get; set; }

        public string Cnp { get; set; }

        public string StudentId { get; set; }

        public List<Tuple<StudentToSubject, Subject>> SubjectsList { get; set; } 
             
    }
}