using System;
using StudentsManagement.College;
using StudentsManagement.DataLayer;
using StudentsManagement.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using StudentsManagement.Helpers;

namespace StudentsManagement.Controllers
{
    public class StudentController : ApiController
    {
        private readonly IDataLayer<Student> StudentService;
        private readonly ICollegeRules CollegeRules;

        public StudentController(IDataLayer<Student> studentService, ICollegeRules collegeRules)
        {
            StudentService = studentService;
            CollegeRules = collegeRules;
        }

        // GET: api/Student
        [Route("api/Student")]
        public IEnumerable<Student> Get()
        {
            var students = this.StudentService.GetAll();
            return students;
        }

        // GET: api/Student/5
        [HttpGet]
        [Route("api/Student/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var student = this.StudentService.Get(id);
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(student);
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
                //var uploadPath = HttpContext.Current.Server.MapPath("~\\App_Data\\App_LocalResources\\");
                var uploadPath = "D:\\master an 2\\css\\c\\";
                var streamProvider = new StreamProvider(uploadPath);
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                var messages = new List<string>();
                foreach (var file in streamProvider.FileData)
                {
                    var fi = new FileInfo(file.LocalFileName);
                    StudentService.Import(fi.FullName);
                    messages.Add("File uploaded");
                }
                return messages;
            }
            var response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
            throw new HttpResponseException(response);
        }

        [HttpGet]
        [Route("api/Student/Download")]
        public HttpResponseMessage DownloadDocument()
        {
            try
            {
                //var filePath = System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\App_LocalResources\\students.csv");
                var filePath = "D:\\master an 2\\css\\c\\";
                var stream = new FileStream(filePath, FileMode.Open);
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(stream)
                };
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition.FileName = "Students.csv";
                return result;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
            }
        }
    }
}
