using StudentsManagement.Models;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace StudentsManagement.DataLayer
{
    public class StudentService : IDataLayer<Student>
    {
        private readonly string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\App_LocalResources\\students.csv");

        public IEnumerable<Student> GetAll()
        {
            var students = new List<Student>();
            using (var reader = new StreamReader(File.OpenRead(filePath)))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var fields = line.Split(',');
                        var student = new Student
                        {
                            Id = int.Parse(fields[0]),
                            LastName = fields[1],
                            FathersInitial = fields[2],
                            FirstName = fields[3],
                            Cnp = fields[4],
                            StudentId = fields[5]
                        };
                        students.Add(student);
                    }
                }
            }
            return students;
        }

        public void Add(Student student)
        {
            var newItemId = GetNewItemId();

            var newLine = string.Format("{0},{1},{2},{3},{4}", newItemId, student.LastName, student.FirstName,
                student.Cnp, student.StudentId);
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(newLine);
            }
        }

        public Student Get(int id)
        {
            using (var reader = new StreamReader(File.OpenRead(filePath)))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var fields = line.Split(',');
                        if (id == int.Parse(fields[0]))
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
                            return student;
                        }
                    }
                }
            }
            return null;
        }

        public void Update(int id, Student model)
        {
        }

        public void Delete(int Id)
        {
            
        }

        private int GetNewItemId()
        {
            int lastId = 1;
            using (var reader = new StreamReader(File.OpenRead(filePath)))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        lastId++;
                    }
                }
                lastId++;
            }
            return lastId;
        }
    }
}