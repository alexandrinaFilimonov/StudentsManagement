using System.IO;
using StudentsManagement.Models;

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
            var entityToCsv = string.Format("{0},{1},{2},{3},{4},{5}", newItemId, student.FirstName, student.FathersInitial, student.LastName,
                student.StudentId, student.Cnp);
            return entityToCsv;
        }

        public void ImportStudents(string sourceFilePath)
        {
            var existingStudentIds = GetExistingValuesOfAProperty(propertyIndex: 4);
            var newItemId = GetNewItemId();
            using (var reader = new StreamReader((sourceFilePath)))
            {
                using (var writer = new StreamWriter(FilePath, true))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        if (!string.IsNullOrEmpty(line))
                        {
                            var values = line.Split(',');
                            if (!existingStudentIds.Contains(values[3]))
                            {
                                var newLine = string.Format("{0},{1}", newItemId, line);
                                writer.WriteLine(newLine);
                                newItemId++;
                            }
                        }
                    }
                }
            }
            File.Delete(sourceFilePath);
        }
    }
}