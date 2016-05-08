namespace StudentsManagement.Models
{
    public class StudentSubjectJoin
    {
        public StudentSubjectJoin(StudentToSubject studentToSubject, Models.Subject subject)
        {
            this.StudentId = studentToSubject.StudentId;
            this.Grade = studentToSubject.Grade;
            this.Subject = subject;
        }

        public int StudentId { get; set; }

        public int Grade { get; set; }

        public Subject Subject { get; set; }
    }
}