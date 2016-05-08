using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentsManagement.Controllers;
using StudentsManagement.Models;

namespace StudentsManagement.Tests.Controllers
{
    [TestClass]
    public class StudentControllerTest
    {
        [TestMethod]
        public void AddStudent(Student student)
        {
            
        }

        [TestMethod]
        public void GetStudent_ShouldNotFindStudent()
        {
            var controller = new StudentController();
            var result = controller.Get(999);
            Assert.IsInstanceOfType(result, typeof (NotFoundResult));
        }
    }
}
