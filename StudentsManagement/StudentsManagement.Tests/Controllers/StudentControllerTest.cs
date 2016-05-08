using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentsManagement.Controllers;
using StudentsManagement.DataLayer;
using StudentsManagement.Models;
using StudentsManagement.College;
using System;

namespace StudentsManagement.Tests.Controllers
{
    [TestClass]
    public class StudentControllerTest
    {
        private Mock<IDataLayer<Subject>> SubjectServiceMock;
        private Mock<IDataLayer<Student>> StudentServiceMock;
        private Mock<ICollegeRules> CollegeRulesMock;
        private Random random = new Random();
        
        [TestInitialize]
        public void STestInitialize()
        {
            SubjectServiceMock = new Mock<IDataLayer<Subject>>();
            var newsubject = new Subject() { Credits = 5, Name = "Cloud", Semester = 2, StudyYear = 3 };
            SubjectServiceMock.Setup(x => x.GetAll()).Returns(new List<Subject> { newsubject });

            CollegeRulesMock = new Mock<ICollegeRules>();
            StudentServiceMock = new Mock<IDataLayer<Student>>();
        }

        [TestMethod]
        public void GetSubjects_ReturnsAListOfSubjects()
        {
            var controller = new SubjectController(SubjectServiceMock.Object);
            var subjects = controller.Get();
            Assert.IsInstanceOfType(subjects, typeof(IEnumerable<Subject>), "Method does not return a list of subjects");
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
    }
}
