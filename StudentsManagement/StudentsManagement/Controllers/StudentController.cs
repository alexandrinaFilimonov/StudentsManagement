using StudentsManagement.College;
using StudentsManagement.DataLayer;
using StudentsManagement.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace StudentsManagement.Controllers
{
    
    public class StudentController : ApiController
    {
        private readonly IDataLayer<Student> StudentService;
        private readonly CollegeRules CollegeRules;

        public StudentController()
        {
            var studentSubjectJoiner = new StudentSubjectJoiner(new SubjectService(), new StudentToSubjectService());
            StudentService = new StudentService(studentSubjectJoiner);

            CollegeRules = new CollegeRules { AcademicYear = 1, Semester = 2 };
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

        // Get: api/Student/Buget
        [HttpGet]
        [Route("api/Student/Budget")]
        public IEnumerable<StudentBudgetStatus> Budget()
        {
            var students = this.StudentService.GetAll();
            return CollegeRules.GetBugetStudents(students);
        }

        // Get: api/Student/Promotion
        [HttpGet]
        [Route("api/Student/Promotion")]
        public IEnumerable<StudentPromotionDetails> Promotion()
        {
            var students = this.StudentService.GetAll();
            return CollegeRules.GetPromotionDetails(students);
        }

        // POST: api/Student
        [HttpPost]
        [Route("api/Student/Add")]
        public void Add(Student student)
        {
            this.StudentService.Add(student);
        }

        // POST: api/Student/Details
        [HttpPost]
        [Route("api/Student/Details")]
        public StudentCollegeDetails Details(DetailsQuery query)
        {
            var student = this.StudentService.Get(query.StudentId);
            return CollegeRules.GetStudentCollgeStatus(student, query.AcademicYear, query.Semester);
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
