using StudentsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.College
{
    public class StudentBudgetStatus
    {
        public Student Student { get; set; }

        public bool Financed { get; set; }

        public int Credits { get; set; }
    }
}