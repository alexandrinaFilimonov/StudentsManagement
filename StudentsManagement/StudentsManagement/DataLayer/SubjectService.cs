using StudentsManagement.FileManager;
using StudentsManagement.Models;
using StudentsManagement.Reader;
using System;

namespace StudentsManagement.DataLayer
{
    public class SubjectService : DataLayer<Subject>
    {
        public SubjectService(IIOFactory factory, IFileManager fileManager)
            : base(factory, fileManager)
        {

        }

        protected override Subject CreateEntity(string[] fields)
        {
            return new Subject{
                Id = int.Parse(fields[0]),
                Name = fields[1],
                StudyYear = int.Parse(fields[2]),
                Semester = int.Parse(fields[3]),
                Credits = int.Parse(fields[4])
            };
        }

        public override string FilePath
        {
            get
            {
                var filePath = System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\App_LocalResources\\subject.csv");
                return filePath;
            }   
        }

        public override void Import(string fileName)
        {
            throw new NotImplementedException();
        }

        protected override string EntityToCsv(int newItemId, Subject subject)
        {
            return string.Format("{0},{1},{2},{3},{4}", newItemId, subject.Name, subject.StudyYear,
                    subject.Semester, subject.Credits);
        }
    }
}