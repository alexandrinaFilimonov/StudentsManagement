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
        studentTable.on('click', '.table-row', showDetails);
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

onSuccessImport = function (messages) {
    for (i = 0; i < messages.length; i++) {
        alert(messages[i]);
    }
    StudentService.GetAll(showAll);
}

onImportError = function () {
    alert("Error on uploading file");
}

$(document).ready(function () {
    StudentService.GetAll(showAll);

    $('#financed').click(function () {
        StudentService.GetByParameter("Budget", showBudgeted);
    });

    $('#promotion').click(function () {
        StudentService.GetByParameter("Promotion", showPromotion);
    });

    $("#uploadBtn").click(function (evt) {
        var files = $("#file1").get(0).files;
        if (files.length > 0) {
            var data = new FormData();
            for (i = 0; i < files.length; i++) {
                data.append("file" + i, files[i]);
            }

            StudentService.UploadFile(data, onSuccessImport, onImportError);
        }
    });

    $('#downloadBtn').click(function() {
        window.open("http://localhost/StudentsManagement/api/Student/Download", "_blank");
    });
});