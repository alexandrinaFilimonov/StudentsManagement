createStudentTd = function (value) {
    var studentTd = $('<td></td>');
    studentTd.text(value);
    return studentTd;
}

// All
showAll = function (students) {
    studentTable = $('#student-table');
    studentTable.empty();
    
    $(students).each(function (index, item) {
        var studentTr = $('<tr></tr>');
        studentTr.append(createStudentTd(item.Id));
        studentTr.append(createStudentTd(item.FirstName));
        studentTr.append(createStudentTd(item.FatherInital));
        studentTr.append(createStudentTd(item.LastName));

        studentTr.addClass("table-row");
        studentTr.css("cursor", "pointer");
        studentTable.on('click', '.table-row', showDetails)
        studentTable.append(studentTr);
    });
}

// Details
showDetails = function(){
    document.location.replace('StudentDetails.html');
}

// Financed
showBudgeted = function (studentsBudgetDetails) {
    studentTable = $('#student-table');
    studentTable.empty();

    $(studentsBudgetDetails).each(function (index, item) {
        var studentTr = $('<tr></tr>');
        studentTr.append(createStudentTd(item.Student.Id));
        studentTr.append(createStudentTd(item.Student.FirstName));
        studentTr.append(createStudentTd(item.Student.FatherInital));
        studentTr.append(createStudentTd(item.Student.LastName));

        financed = item.Financed === 'true' ? 'Financed' : 'Fee payer';
        studentTr.append(createStudentTd(financed));
        studentTr.append(createStudentTd(item.Credits));
        
        studentTr.addClass("table-row");
        studentTr.css("cursor", "pointer");
        studentTable.on('click', '.table-row', showDetails)
        studentTable.append(studentTr);
    });
}

// Promotion
showPromotion = function (studentsPromotionDetails) {
    studentTable = $('#student-table');
    studentTable.empty();

    $(studentsPromotionDetails).each(function (index, item) {
        var studentTr = $('<tr></tr>');
        studentTr.append(createStudentTd(item.Student.Id));
        studentTr.append(createStudentTd(item.Student.FirstName));
        studentTr.append(createStudentTd(item.Student.FatherInital));
        studentTr.append(createStudentTd(item.Student.LastName));

        promoted = item.Promoted === 'true' ? 'Promoted' : 'Failed';
        studentTr.append(createStudentTd(promoted));

        studentTr.addClass("table-row");
        studentTr.css("cursor", "pointer");
        studentTable.on('click', '.table-row', showDetails)
        studentTable.append(studentTr);
    });
}

$(document).ready(function () {
    StudentService.GetAll(showAll)

    $('#financed').click(function () {
        StudentService.GetByParameter("Budget", showBudgeted);
    });

    $('#promotion').click(function () {
        StudentService.GetByParameter("Promotion", showPromotion);
    });
});