using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentsManagement.Controllers;
using StudentsManagement.DataLayer;
using StudentsManagement.Models;
using StudentsManagement.College;
using System;
using StudentsManagement.Tests.FakeModels;

namespace StudentsManagement.Tests.Controllers
{
    [TestClass]
    public class StudentControllerTest
    {
        private Mock<IDataLayer<Subject>> SubjectServiceMock;
        private Mock<IDataLayer<StudentToSubject>> StudentToSubjectServiceMock;
        private Mock<IJoiner<StudentToSubject, Subject>> StudentSubjectJoinerMock;
        private Mock<IDataLayer<Student>> StudentServiceMock;
        private Mock<ICollegeRules> CollegeRulesMock;
        private Random random = new Random();
        
        [TestInitialize]
        public void TestInitialize()
        {
            SubjectServiceMock = new Mock<IDataLayer<Subject>>();
            StudentToSubjectServiceMock = new Mock<IDataLayer<StudentToSubject>>();
            StudentSubjectJoinerMock = new Mock<IJoiner<StudentToSubject, Subject>>();
            StudentServiceMock = new Mock<IDataLayer<Student>>();

            StudentServiceMock.Setup(x => x.GetAll()).Returns(new List<Student>() { Fakes.GetStudent() });
            StudentServiceMock.Setup(x => x.Add(Fakes.GetStudent())).Verifiable();
            StudentServiceMock.Setup(x => x.Update(12, Fakes.GetStudent())).Verifiable();
            StudentServiceMock.Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

        }

        [TestMethod]
        public void AddStudent_ServiceMethodIsCalled()
        {
            var controller = new StudentController(StudentServiceMock.Object);
            controller.Add(Fakes.GetStudent());

            StudentServiceMock.Verify(x => x.Add(It.IsAny<Student>()), Times.Once);
        }

        [TestMethod]
        public void UpdateStudent_ServiceMethodIsCalled()
        {
            var controller = new StudentController(StudentServiceMock.Object);
            controller.Update(12, Fakes.GetStudent());

            StudentServiceMock.Verify(x => x.Update(It.IsAny<int>(), It.IsAny<Student>()), Times.Once);
        }

        [TestMethod]
        public void DeleteStudent_ServiceMethodIsCalled()
        {
            var controller = new StudentController(StudentServiceMock.Object);
            controller.Delete(12);

            StudentServiceMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void GetStudentById_ReturnsStudent()
        {
            //Arrange
            StudentServiceMock.Setup(x => x.Get(It.IsAny<int>())).Returns(Fakes.GetStudent);
            var controller = new StudentController(StudentServiceMock.Object)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            //Act
            var result = controller.Get(20);

            //Assert
            Student student;
            //Assert.IsInstanceOfType(student, typeof(Student), "Returned object does not have the correct type");
            Assert.IsTrue(result.TryGetContentValue<Student>(out student));
        }

        [TestMethod]
        public void GetStudentById_ReturnsNotFoundMessage()
        {
            //Arrange
            StudentServiceMock.Setup(x => x.Get(15)).Returns(Fakes.Students().FirstOrDefault(x => x.Id == 15));

            var controller = new StudentController(StudentServiceMock.Object)
        {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            //Act
            var result = controller.Get(15);

            //Assert
            Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
        }

        [TestMethod]
        public void StudentBudgetTest()
        {
            // Arrange
            var controller = new StudentController(StudentServiceMock.Object, CollegeRulesMock.Object);
            
            var students = new List<Student>();
            var studentBdgetList = new List<StudentBudgetStatus>();
            StudentServiceMock.Setup(service => service.GetAll())
                .Returns(students);

            CollegeRulesMock.Setup(rules => rules.GetBugetStudents(It.Is<IEnumerable<Student>>(list => list.Equals(students))))
                .Returns(studentBdgetList);

            // Act
            var actualBudget = controller.Budget();

            // Assert
            Assert.AreEqual(studentBdgetList, actualBudget);
            StudentServiceMock.VerifyAll();
            CollegeRulesMock.VerifyAll();
        }

        [TestMethod]
        public void StudentPromotionTest()
        {
            // Arrange
            var controller = new StudentController(StudentServiceMock.Object, CollegeRulesMock.Object);

            var students = new List<Student>();
            var studentPromotionDetails = new List<StudentPromotionDetails>();
            StudentServiceMock.Setup(service => service.GetAll())
                .Returns(students);

            CollegeRulesMock.Setup(rules => rules.GetPromotionDetails(It.Is<IEnumerable<Student>>(list => list.Equals(students))))
                .Returns(studentPromotionDetails);

            // Act
            var actualPromotionDetails = controller.Promotion();

            // Assert
            Assert.AreEqual(studentPromotionDetails, actualPromotionDetails);
            StudentServiceMock.VerifyAll();
            CollegeRulesMock.VerifyAll();
        }

        [TestMethod]
        public void StudentCollegeDetailsTest()
        {
            // Arrange
            var controller = new StudentController(StudentServiceMock.Object, CollegeRulesMock.Object);

            var student = new Student { Id = random.Next(100) };
            var query = new DetailsQuery { StudentId = student.Id, AcademicYear = 1, Semester = 2};
            var studentStatusDetails = new StudentCollegeDetails();
            StudentServiceMock.Setup(service => service.Get(student.Id))
                .Returns(student);

            CollegeRulesMock.Setup(rules => rules.GetStudentCollgeStatus(student, query.AcademicYear, query.Semester))
                .Returns(studentStatusDetails);

            // Act
            var actualStatusDetails = controller.Details(query);

            // Assert
            Assert.AreEqual(studentStatusDetails, actualStatusDetails);
            StudentServiceMock.VerifyAll();
            CollegeRulesMock.VerifyAll();
        }

        [TestMethod]
        public void AddSubbject(Subject subject)
        { }
        public void GetStudents_ReturnsAListOfStudents()
        {
            //Arrange
            var controller = new StudentController(StudentServiceMock.Object);

            //Act
            var students = controller.Get();

            //Assert
            Assert.IsInstanceOfType(students, typeof(IEnumerable<Student>), "Method does not return a list of students");
            Assert.AreNotEqual(0, students.Count(), "List is empty");
        }
    }
}
