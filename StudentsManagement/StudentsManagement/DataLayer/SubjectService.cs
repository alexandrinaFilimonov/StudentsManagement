﻿using StudentsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.DataLayer
{
    public class SubjectService : Service<Subject>
    {
        private StudentSubjectJoiner studentSubjectJoiner;

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

        protected override string FilePath
        {
            get { return System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\App_LocalResources\\subject.csv"); }
        }

        protected override string EntityToCsv(int newItemId, Subject subject)
        {
            return string.Format("{0},{1},{2},{3},{4}", newItemId, subject.Name, subject.StudyYear,
                    subject.Semester, subject.Credits);
        }
    }
}