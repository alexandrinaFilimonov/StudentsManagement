createStudentTd = function (value) {
    var studentTd = $('<td></td>');
    studentTd.text(value);
    return studentTd;
}

addStudentDefault = function (student, studentTr) {
    studentTr.append(createStudentTd(student.Id));
    studentTr.append(createStudentTd(student.FirstName));
    studentTr.append(createStudentTd(student.FathersInitial));
    studentTr.append(createStudentTd(student.LastName));
}

// All
showAll = function (students) {
    studentTable = $('#student-table');
    studentTable.empty();
    
    $(students).each(function (index, item) {
        var studentTr = $('<tr></tr>');
        addStudentDefault(item, studentTr);

        studentTr.addClass("table-row");
        studentTr.css("cursor", "pointer");
        studentTable.on('click', '.table-row', showDetails)
        studentTable.append(studentTr);
    });
}

// Details
showDetails = function () {
    document.location.replace('StudentDetails.html');
}

// Financed
showBudgeted = function (studentsBudgetDetails) {
    studentTable = $('#student-table');
    studentTable.empty();

    $(studentsBudgetDetails).each(function (index, item) {
        var studentTr = $('<tr></tr>');
        addStudentDefault(item.Student, studentTr);
        
        financed = item.Financed ? 'Financed' : 'Fee payer';
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
        addStudentDefault(item.Student, studentTr);

        promoted = item.Promoted ? 'Promoted' : 'Failed';
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