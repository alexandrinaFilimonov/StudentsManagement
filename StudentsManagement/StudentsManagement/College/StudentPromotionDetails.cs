using StudentsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.College
{
    public class StudentPromotionDetails
    {
        public Student Student { get; set; }

        public bool Promoted { get; set; }
    }
}