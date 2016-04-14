using StudentsManagement.Models;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace StudentsManagement.DataLayer
{
    public class StudentService : Service<Student>
    {
        private StudentSubjectJoiner studentSubjectJoiner;

        protected override string FilePath
        {
            get { return System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\App_LocalResources\\students.csv"); }
        }

        public StudentService(StudentSubjectJoiner studentSubjectJoiner)
        {
            this.studentSubjectJoiner = studentSubjectJoiner;
        }

        protected override Student CreateEntity(string[] fields)
        {
            var student = new Student
            {
                Id = int.Parse(fields[0]),
                LastName = fields[1],
                FathersInitial = fields[2],
                FirstName = fields[3],
                Cnp = fields[4],
                StudentId = fields[5]
            };

            student.SubjectsList = studentSubjectJoiner.Join(student.Id);

            return student;
        }

        protected override string EntityToCsv(int newItemId, Student student)
        {
            return string.Format("{0},{1},{2},{3},{4}", newItemId, student.LastName, student.FirstName,
                student.Cnp, student.StudentId);
        }

        public void Update(int id, Student model)
        {
        }

        public void Delete(int Id)
        {
        }        
    }
}