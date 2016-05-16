createStudentTd = function (value) {
    var studentTd = $('<td></td>');
    studentTd.text(value);
    return studentTd;
}

addStudentDefault = function (student, studentTr) {
    Preconditions.checkState((studentTr.children('td').length == 0), "Student table row is not empty before adding student details");
    studentTr.append(createStudentTd(student.Id));
    studentTr.append(createStudentTd(student.FirstName));
    studentTr.append(createStudentTd(student.FathersInitial));
    studentTr.append(createStudentTd(student.LastName));
    studentTr.append(createStudentTd(student.StudentId));
    Postconditions.checkState((studentTr.children('td').length == 5), "Student table row does not show all 5 cells");
}

// All
showAll = function (students) {
    Preconditions.checkState($('#student-table').length, "Html with id student-table does not exists");

    studentTable = $('#student-table');
    studentTable.empty();
    
    $(students).each(function (index, item) {

        var studentTr = $('<tr></tr>');
        addStudentDefault(item, studentTr);

        studentTr.addClass("table-row");
        studentTr.css("cursor", "pointer");
        studentTable.append(studentTr);
    });
    studentTable.on('click', 'tr ', showDetails);
    Postconditions.checkState(($('#student-table').children('tr').length == students.length), "Student table doesn't contains total nr. of students.");
}

// Details
showDetails = function (event) {
    var tableRows = $(event.target).parent().find('td');
    var id = $(tableRows[0]).text();
    console.log(id);
    document.location.replace('StudentDetails.html?id=' + id);
}

// Financed
showBudgeted = function (studentsBudgetDetails) {
    Preconditions.checkState($('#student-table').length, "Html with id student-table does not exists");

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
    Postconditions.checkState(
	($('#student-table').children('tr').length == studentsBudgetDetails.length), 
	"Student table doesn't contains total nr. of students.");
}

// Promotion
showPromotion = function (studentsPromotionDetails) {
    Preconditions.checkState($('#student-table').length, "Html with id #student-table does not exists");

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
    Postconditions.checkState(
	($('#student-table').children('tr').length == studentsPromotionDetails.length), 
	"Student table doesn't contains total nr. of students.");
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

function documentReadyAction() {
    Preconditions.checkState($('#student-table').length, "Html with id #student-table does not exists");
    Preconditions.checkState($('#student-table').html() == '', "Html with id #student-table does not exists");
    Preconditions.checkState($('#financed').length, "Html with id #financed does not exists");
    Preconditions.checkState($('#promotion').length, "Html with id #promotion does not exists");
    Preconditions.checkState($('#uploadBtn').length, "Html with id #uploadBtn does not exists");
    Preconditions.checkState($('#downloadBtn').length, "Html with id #downloadBtn does not exists");

    StudentService.GetAll(showAll);

    $('#financed').click(function () {
        StudentService.GetByParameter("Budget", showBudgeted);
    });

    $('#promotion').click(function () {
        StudentService.GetByParameter("Promotion", showPromotion);
    });

    $("#uploadBtn").click(function (evt) {
        var files = $("#file").get(0).files;
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

    var inputs = document.querySelectorAll('.inputfile');
    Array.prototype.forEach.call(inputs, function (input) {
        var label = input.nextElementSibling,
            labelVal = label.innerHTML;

        input.addEventListener('change', function (e) {
            var fileName = '';
            if (this.files && this.files.length > 1)
                fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
            else
                fileName = e.target.value.split('\\').pop();

            if (fileName)
                label.innerHTML = fileName;
        });
    });
    Postconditions.checkState(($._data($('#financed').get(0),'events').click.length > 0), "Button with id #financed must have click event atached");
    Postconditions.checkState(($._data($('#promotion').get(0),'events').click.length > 0), "Button with id #promotion must have click event atached");
    Postconditions.checkState(($._data($('#uploadBtn').get(0),'events').click.length > 0), "Button with id #uploadBtn must have click event atached");
    Postconditions.checkState(($._data($('#downloadBtn').get(0),'events').click.length > 0), "Button with id #downloadBtn must have click event atached");
}

$(document).ready(documentReadyAction());
