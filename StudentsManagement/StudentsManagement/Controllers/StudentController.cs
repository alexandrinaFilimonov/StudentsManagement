using StudentsManagement.DataLayer;
using StudentsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentsManagement.Controllers
{
    public class StudentController : ApiController
    {
        public IDataLayer<Student> StudentService;

        public StudentController()
        {
            StudentService = new StudentService();
        }

        // GET: api/Student
        public IEnumerable<Student> Get()
        {
            return this.StudentService.GetAll();
        }

        // GET: api/Student/5
        public Student Get(int id)
        {
            return this.StudentService.Get(id);
        }

        // POST: api/Student
        public void Post([FromBody]Student student)
        {
            this.StudentService.Update(student);
        }

        // DELETE: api/Student/5
        public void Delete(int id)
        {
            this.StudentService.Delete(id);
        }
    }
}
