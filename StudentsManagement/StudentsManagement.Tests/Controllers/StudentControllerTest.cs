using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentsManagement.Controllers;
using StudentsManagement.DataLayer;
using StudentsManagement.Models;

namespace StudentsManagement.Tests.Controllers
{
    [TestClass]
    public class StudentControllerTest
    {
        private Mock<IDataLayer<Subject>> SubjectServiceMock;
        
        [TestInitialize]
        public void STestInitialize()
        {
            SubjectServiceMock = new Mock<IDataLayer<Subject>>();
            var newsubject = new Subject() { Credits = 5, Name = "Cloud", Semester = 2, StudyYear = 3 };
            SubjectServiceMock.Setup(x => x.GetAll()).Returns(new List<Subject> { newsubject });
        }

        [TestMethod]
        public void GetSubjects_ReturnsAListOfSubjects()
        {
            var controller = new SubjectController(SubjectServiceMock.Object);
            var subjects = controller.Get();
            Assert.IsInstanceOfType(subjects, typeof(IEnumerable<Subject>), "Method does not return a list of subjects");
        }

        [TestMethod]
        public void AddSubbject(Subject subject)
        { }
    }
}
