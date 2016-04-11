using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public char Fathers_initial { get; set; }

        public string Cnp { get; set; }

        public string Sid { get; set; }
    }
}