var submitStudent = function (event) {
    var student = {}
    student.FirstName = $(this).find('#inputFirstName').val();
    student.LastName = $(this).find('#inputLastName').val();
    student.FathersInitial = $(this).find('#inputFathersInitial').val();
    student.StudentId = $(this).find('#inputStudentId').val();
    student.Cnp = $(this).find('#inputCnp').val();
    student.SubjectId = $(this).find('#inputSubjectId').val();

    StudentService.Post(student);
    event.preventDefault();
}

$(document).ready(function() {
    $('#student-form').submit(submitStudent);
});
