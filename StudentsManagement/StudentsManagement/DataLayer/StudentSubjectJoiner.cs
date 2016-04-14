using StudentsManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentsManagement.DataLayer
{
    public class StudentSubjectJoiner : IJoiner<StudentToSubject, Subject>
    {
        private SubjectService subjectService;
        private StudentToSubjectService studentToSubjectService;

        public StudentSubjectJoiner(SubjectService subjectService, StudentToSubjectService studentToSubjectService)
        {
            this.studentToSubjectService = studentToSubjectService;
            this.subjectService = subjectService;
        }

        public List<Tuple<StudentToSubject,Subject>> Join(int fromForeignKey)
        {
            var subjectList = new List<Tuple<StudentToSubject, Subject>>();
            var studentToSubjectList = studentToSubjectService.GetByFieldValue(0, fromForeignKey.ToString());
            foreach(var studentToSubject in studentToSubjectList){
                var subject = subjectService.Get(studentToSubject.SubjectId);
                subjectList.Add(new Tuple<StudentToSubject, Subject>(studentToSubject, subject));
            }

            return subjectList;
        }
    }
}