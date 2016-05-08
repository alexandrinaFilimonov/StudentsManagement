using StudentsManagement.Models;
using System.Collections.Generic;

namespace StudentsManagement.DataLayer
{
    public class StudentSubjectJoiner : IJoiner<StudentToSubject, Subject>
    {
        private readonly IDataLayer<Subject> subjectService;
        private readonly IDataLayer<StudentToSubject> studentToSubjectService;

        public StudentSubjectJoiner(IDataLayer<Subject> subjectService, IDataLayer<StudentToSubject> studentToSubjectService)
        {
            this.studentToSubjectService = studentToSubjectService;
            this.subjectService = subjectService;
        }

        public List<StudentSubjectJoin> Join(int fromForeignKey)
        {
            var subjectList = new List<StudentSubjectJoin>();
            var studentToSubjectList = studentToSubjectService.GetByFieldValue(0, fromForeignKey.ToString());
            foreach(var studentToSubject in studentToSubjectList){
                var subject = subjectService.Get(studentToSubject.SubjectId);
                subjectList.Add(new StudentSubjectJoin(studentToSubject, subject));
            }

            return subjectList;
        }
    }
}