using StudentsManagement.College;
using StudentsManagement.DataLayer;
using StudentsManagement.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using StudentsManagement.Helpers;

namespace StudentsManagement.Controllers
{
    public class StudentController : ApiController
    {
        private readonly StudentService StudentService;
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

        [HttpPost]
        [Route("api/Student/Update/{id}")]
        public void Update(int id, [FromBody] Student student)
        {
            this.StudentService.Update(id, student);
        }

        // DELETE: api/Student/5
        [HttpDelete]
        [Route("api/Student/Delete/{id}")]
        public void Delete(int id)
        {
            this.StudentService.Delete(id);
        }

        [HttpPost]
        [Route("api/Student/Upload")]
        public async Task<List<string>> Upload()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                string uploadPath = HttpContext.Current.Server.MapPath("~\\App_Data\\App_LocalResources\\");

                var streamProvider = new MyStreamProvider(uploadPath);

                await Request.Content.ReadAsMultipartAsync(streamProvider);

                var messages = new List<string>();

                foreach (var file in streamProvider.FileData)
                {
                    var fi = new FileInfo(file.LocalFileName);
                    StudentService.ImportStudents(fi.FullName);
                    messages.Add("File uploaded");
                }
                return messages;
            }
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
            throw new HttpResponseException(response);
        }
    }
}
