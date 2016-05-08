using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudentsManagement.Reader;
using StudentsManagement.DataLayer;
using System.Collections.Generic;
using StudentsManagement.FileManager;

namespace StudentsManagement.Tests.DataLayer
{
    [TestClass]
    public class DataLayerTest
    {
        private Mock<IIOFactory> IOFactoryMock;
        private Mock<IReader> ReaderMock;
        private Mock<IWriter> WriterMock;

        private DataLayer<FakeModel> TestedDataLayer;

        private string[] file = new string[] { "1,fake1,1", "2,fake2,2", "3,fake3,3" };

        [TestInitialize]
        public void TestInitialize()
        {
            IOFactoryMock = new Mock<IIOFactory>();
            ReaderMock = new Mock<IReader>();
            WriterMock = new Mock<IWriter>();

            IOFactoryMock.Setup(factory => factory.CreateReader(It.IsAny<string>()))
                .Returns(ReaderMock.Object);

            IOFactoryMock.Setup(factory => factory.CreateWriter(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(WriterMock.Object);

            TestedDataLayer = new FakeDataService(IOFactoryMock.Object, new Mock<IFileManager>().Object);
        }

        [TestMethod]
        public void TestGetAll()
        {
            // Arrange
            var index = 0;
            ReaderMock.Setup(reader => reader.ReadLine())
                .Returns(() => file[index++]);
            ReaderMock.SetupGet(reader => reader.EndOfStream)
                .Returns(() => index >= file.Length ? true : false);

            // Act
            var entities = TestedDataLayer.GetAll();

            // Assert
            for(int i = 0; i < file.Length; i++){
                var entity = entities.ElementAt(i);
                var entityIndex = (i + 1);
                Assert.AreEqual(entity.FirstProperty, "fake" + entityIndex);
                Assert.AreEqual(entity.SecondProperty, entityIndex);
            }
        }

        [TestMethod]
        public void TestGetByFieldIndex()
        {
            // Arrange
            var index = 0;
            ReaderMock.Setup(reader => reader.ReadLine())
                .Returns(() => file[index++]);
            ReaderMock.SetupGet(reader => reader.EndOfStream)
                .Returns(() => index >= file.Length ? true : false);

            // Act
            var entities = TestedDataLayer.GetByFieldValue(2,"2");

            // Assert
            Assert.AreEqual(entities.Count(), 1);
            var entity = entities.ElementAt(0);
            Assert.AreEqual(entity.FirstProperty, "fake" + 2);
            Assert.AreEqual(entity.SecondProperty, 2);
        }


        [TestMethod]
        public void TestGet()
        {
            // Arrange
            var index = 0;
            ReaderMock.Setup(reader => reader.ReadLine())
                .Returns(() => file[index++]);
            ReaderMock.SetupGet(reader => reader.EndOfStream)
                .Returns(() => index >= file.Length ? true : false);

            // Act
            var entity = TestedDataLayer.Get(2);

            // Assert
            Assert.IsNotNull(entity);
            Assert.AreEqual(entity.FirstProperty, "fake" + 2);
            Assert.AreEqual(entity.SecondProperty, 2);
        }

        [TestMethod]
        public void TestAdd()
        {
            // Arrange
            var entity = new FakeModel { FirstProperty = "fake4", SecondProperty = 4 };

            var index = 0;
            ReaderMock.Setup(reader => reader.ReadLine())
              .Returns(() => file[index++]);
            ReaderMock.SetupGet(reader => reader.EndOfStream)
                .Returns(() => index >= file.Length ? true : false);

            var expectedEntityToCsv = EntityToCSV(file.Count() + 1, entity.FirstProperty, entity.SecondProperty);

            // Act
            TestedDataLayer.Add(entity);

            // Assert
            WriterMock.Verify(writer => writer.WriteLine(expectedEntityToCsv), Times.Once);
        }

        [TestMethod]
        public void TestUpdate()
        {
            // Arrange
            var entity = new FakeModel { FirstProperty = "fake4", SecondProperty = 4 };

            var index = 0;
            ReaderMock.Setup(reader => reader.ReadLine())
                .Returns(() => file[index++]);
            ReaderMock.SetupGet(reader => reader.EndOfStream)
                .Returns(() => index >= file.Length ? true : false);

            // Act
            var id = 2;
            TestedDataLayer.Update(id, entity);

            // Assert

            for (int i = 1; i <= file.Length; i++)
            {
                string csv;
                if(i != id){
                    csv = EntityToCSV(i, "fake" + i, i);
                }
                else
                {
                    csv = EntityToCSV(id, entity.FirstProperty, entity.SecondProperty);
                }
                WriterMock.Verify(writer => writer.WriteLine(csv), Times.Once);
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            // Arrange
            var index = 0;
            ReaderMock.Setup(reader => reader.ReadLine())
                .Returns(() => file[index++]);
            ReaderMock.SetupGet(reader => reader.EndOfStream)
                .Returns(() => index >= file.Length ? true : false);

            // Act
            var id = 2;
            TestedDataLayer.Delete(id);

            // Assert
            for (int i = 1; i <= file.Length; i++)
            {
                var csv = EntityToCSV(i, "fake" + i, i);
                if (i != id)
                {
                    WriterMock.Verify(writer => writer.WriteLine(csv), Times.Once);
                }
                else
                {
                    WriterMock.Verify(writer => writer.WriteLine(csv), Times.Never);
                }
            }
        }

        private string EntityToCSV(int id,string first, int second)
        {
            var expectedEntityToCsv = string.Format("{0},{1},{2}", id, first, second);
            return expectedEntityToCsv;
        }
    }
}
