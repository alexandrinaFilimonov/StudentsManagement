using System.Collections.Generic;
using StudentsManagement.Models;

namespace StudentsManagement.Tests.FakeModels
{
    public class Fakes
    {
        public static List<Student> Students()
        {
            return new List<Student>()
            {
                GetStudent(),
                GetStudent()
            };
        }

        public static Subject GetSubject()
        {
            return new Subject
            {
                Id = 8,
                Credits = 5, 
                Name = "Cloud Computing", 
                Semester = 2, 
                StudyYear = 3
            };
        }

        public static Student GetStudent()
        {
            return new Student
            {
                Id = 18,
                Cnp = "7657456475674",
                FathersInitial = "A",
                FirstName = "Alex",
                LastName = "Popescu",
                StudentId = "765714655845"
            };
        }


    }
}
