using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentsManagement.Controllers;
using StudentsManagement.DataLayer;
using StudentsManagement.Models;
using StudentsManagement.Tests.FakeModels;

namespace StudentsManagement.Tests.Controllers
{
    public class SubjectControllerTest
    {
        private Mock<IDataLayer<Subject>> SubjectServiceMock;

        [TestInitialize]
        public void TestInitialize()
        {
            SubjectServiceMock = new Mock<IDataLayer<Subject>>();
            var newsubject = Fakes.GetSubject();
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
