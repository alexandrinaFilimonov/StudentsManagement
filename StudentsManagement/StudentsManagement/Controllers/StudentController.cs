﻿using StudentsManagement.DataLayer;
using StudentsManagement.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace StudentsManagement.Controllers
{
    
    public class StudentController : ApiController
    {
        private readonly IDataLayer<Student> StudentService;

        public StudentController()
        {
            StudentService = new StudentService();
        }

        // GET: api/Student
        [Route("api/Student")]
        public IEnumerable<Student> Get()
        {
            return this.StudentService.GetAll();
        }

        // GET: api/Student/5
        [HttpGet]
        [Route("api/Student/{id}")]
        public Student Get(int id)
        {
            return this.StudentService.Get(id);
        }

        // Get: api/Student/Financed
        //[HttpGet]
        //public IEnumerable<Student> Financed()
        //{
        //    return this.StudentService.GetAll();
        //}

        // Get: api/Student/FeePayer
        //[HttpGet]
        //public IEnumerable<Student> FeePayer()
        //{
        //    return this.StudentService.GetAll();
        //}

        // Get: api/Student/Promotion
        //[HttpGet]
        //public IEnumerable<Student> Promotion()
        //{
        //    return this.StudentService.GetAll();
        //}

        // POST: api/Student
        [HttpPost]
        [Route("api/Student/Add")]
        public void Add(Student student)
        {
            this.StudentService.Add(student);
        }

        [HttpPost]
        [Route("api/Student/Update")]
        public void Update(int id, [FromBody] Student student)
        {
            this.StudentService.Update(id, student);
        }

        // DELETE: api/Student/5
        public void Delete(int id)
        {
            this.StudentService.Delete(id);
        }
    }
}
