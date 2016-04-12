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

showFinanced = function () {
    alert('financed');
}

showFeePayer = function () {
    alert('fee-payer');
}

showPromotion = function () {
    alert('promotion');
}

$(document).ready(function () {
    StudentService.GetAll(showAll)

    $('#financed').click(function () {
        StudentService.GetByParameter("Financed", showFinanced);
    });
    $('#fee-payer').click(function () {
        StudentService.GetByParameter("FeePayer", showFeePayer);
    });
    $('#promotion').click(function () {
        StudentService.GetByParameter("Promotion", showPromotion);
    });
});