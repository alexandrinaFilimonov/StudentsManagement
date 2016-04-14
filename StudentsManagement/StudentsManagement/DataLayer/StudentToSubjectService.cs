using StudentsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.DataLayer
{
    public class StudentToSubjectService : Service<StudentToSubject>
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

        protected override string FilePath
        {
            get { return System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\App_LocalResources\\studentsToSubject.csv"); }
        }

        protected override string EntityToCsv(int newItemId, StudentToSubject studentToSubject)
        {
            return string.Format("{0},{1},{2}", studentToSubject.StudentId, studentToSubject.SubjectId,
                studentToSubject.Grade);
        }
    }
}