var submitSubject = function (event) {
    var subject = {}
    subject.Name = $(this).find('#inputName').val();
    subject.StudyYear = $(this).find('#inputStudyYear').val();
    subject.Semester = $(this).find('#inputSemester').val();
    subject.Credits = $(this).find('#inputCredits').val();
    SubjectService.Post(subject);
    event.preventDefault();
}

$(document).ready(function () {
    $('#subject-form').submit(submitSubject);
});
