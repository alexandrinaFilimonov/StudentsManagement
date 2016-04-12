using StudentsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.DataLayer
{
    public class StudentService : IDataLayer<Student>
    {
        public IEnumerable<Student> GetAll()
        {
            return new List<Student>(){ new Student { Id = 3 , FirstName = "Larry" } };
        }

        public Student Get(int Id)
        {
            return new Student { Id = 3, FirstName = "Larry" };
        }

        public void Update(Student model)
        {
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}