using StudentsManagement.Models;
using System;

namespace StudentsManagement.DataLayer
{
    public class StudentToSubjectService : DataLayer<StudentToSubject>
    {
        protected override StudentToSubject CreateEntity(string[] fields)
        {
            return new StudentToSubject
            {
                StudentId = int.Parse(fields[0]),
                SubjectId = int.Parse(fields[1]),
                Grade = int.Parse(fields[2]),
            };
        }

        public override string FilePath
        {
            get
            {
                var filePath = System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\App_LocalResources\\studentsToSubject.csv");
                //var filePath = "D:\\master an 2\\css\\c\\studentsToSubject.csv";
                return filePath;
            }
        }

        public override void Import(string fileName)
        {
            throw new NotImplementedException();
        }

        protected override string EntityToCsv(int newItemId, StudentToSubject studentToSubject)
        {
            return string.Format("{0},{1},{2}", studentToSubject.StudentId, studentToSubject.SubjectId,
                studentToSubject.Grade);
        }
    }
}