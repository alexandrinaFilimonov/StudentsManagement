createStudentTd = function (value) {
    var studentTd = $('<td></td>');
    studentTd.text(value);
    return studentTd;
}

showAll = function (students) {
    studentTable = $('#student-table');

    $(students).each(function (index, item) {
        var studentTr = $('<tr></tr>');
        studentTr.append(createStudentTd(item.Id));
        studentTr.append(createStudentTd(item.FirstName));
        studentTr.append(createStudentTd(item.FatherInital));
        studentTr.append(createStudentTd(item.LastName));

        studentTable.append(studentTr);
    });
}

$(document).ready(function () {
    var students = StudentService.GetAll(showAll)
});