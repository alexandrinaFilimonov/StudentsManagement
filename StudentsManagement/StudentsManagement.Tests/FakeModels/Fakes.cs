using StudentsManagement.Models;

namespace StudentsManagement.Tests.FakeModels
{
    public class Fakes
    {
        public static Subject GetSubject()
        {
            return new Subject { Credits = 5, Name = "Cloud", Semester = 2, StudyYear = 3 }
        }

        public static Student GetStudent()
        {
            return new Student
            {
                Cnp = "7657456475674",
                FathersInitial = "A",
                FirstName = "Alex",
                LastName = "Popescu",
                StudentId = "765714655845"
            };
        }
    }
}
