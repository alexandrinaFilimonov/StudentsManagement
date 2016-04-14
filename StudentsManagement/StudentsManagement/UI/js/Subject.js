createTd = function (value) {
    var studentTd = $('<td></td>');
    studentTd.text(value);
    return studentTd;
}

// All
showAll = function (subjects) {
    subjectTable = $('#subject-table');
    subjectTable.empty();

    $(subjects).each(function (index, item) {
        var subjectTr = $('<tr></tr>');
        subjectTr.append(createTd(item.Id));
        subjectTr.append(createTd(item.Name));
        subjectTr.append(createTd(item.StudyYear));
        subjectTr.append(createTd(item.Semester));
        subjectTr.append(createTd(item.Credits));

        subjectTable.append(subjectTr);
    });
}

$(document).ready(function () {
    SubjectService.GetAll(showAll)
});