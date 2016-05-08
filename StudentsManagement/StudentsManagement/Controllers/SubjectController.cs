using StudentsManagement.DataLayer;
using StudentsManagement.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace StudentsManagement.Controllers
{
    public class SubjectController : ApiController
    {
        private readonly IDataLayer<Subject> subjectService;

        public SubjectController(IDataLayer<Subject> subjectService)
        {
            this.subjectService = subjectService;
        }

        // GET: api/Subject
        public IEnumerable<Subject> Get()
        {
            return this.subjectService.GetAll();
        }

        // GET: api/Subject/5
        public Subject Get(int id)
        {
            return this.subjectService.Get(id);
        }

        // POST: api/Subject
        public void Post([FromBody]Subject subject)
        {
            this.subjectService.Add(subject);
        }

        // PUT: api/Subject/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Subject/5
        public void Delete(int id)
        {
        }
    }
}
