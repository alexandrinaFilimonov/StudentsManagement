var getDetails = function (event) {
    var query = {}
    query.StudentId = $(this).find('#inputQueryId').val();
    query.AcademicYear = $(this).find('#inputAcademycYear').val();
    query.Semester = $(this).find('#inputSemester').val();

    StudentService.Detail(query, showCollegeDetails);
    event.preventDefault();
}

var submitStudent = function (event) {
    var student = {}
    student.Id = $(this).find('#inputId').val();
    student.FirstName = $(this).find('#inputFirstName').val();
    student.LastName = $(this).find('#inputLastName').val();
    student.FathersInitial = $(this).find('#inputFathersInitial').val();
    student.StudentId = $(this).find('#inputStudentId').val();
    student.Cnp = $(this).find('#inputCnp').val();

    //student.SubjectsList = [];
    //var subjectRows = $('#subjects-table tr');
    //$(subjectRows).each(function (index, item) {
    //    var tableData = $(item).find('td');
    //   // subjectMapping.SubjectId = $(tableData[0]).val();
        
    //    student.SubjectsList.push({
    //        'StudentId': student.Id,
    //        'Grade': $(tableData[4]).val(),
    //        'Subject': {}
    //    });
    //});
    
    StudentService.Update(student);
    event.preventDefault();
}

createSubjectTd = function (value) {
    var subjectTd = $('<td></td>');
    subjectTd.text(value);
    return subjectTd;
}

createRemoveButton = function (id) {
    var removeTd = $('<td></td>');
    var deleteButton = $('<a></a>');
    deleteButton.text('remove');
    
    removeTd.append(deleteButton);
    return removeTd;
}

removeSubject = function (event){
    var tableRow = $(event.target).parent().parent();
    tableRow.remove();
}

showCollegeDetails = function (studentDetails) {
    var table = $('#college-table');
    table.empty();
    var studentTr = $('<tr></tr>');

    studentTr.append(createSubjectTd(studentDetails.Average));
    studentTr.append(createSubjectTd(studentDetails.Credits));
    
    table.append(studentTr);

    var resultsTable = $('#results-table');
    resultsTable.empty();
    $(studentDetails.ExaminationResults).each(function (index, item) {
        var resultTr = $('<tr></tr>');

        resultTr.append(createSubjectTd(item.Subject.Name));
        resultTr.append(createSubjectTd(item.Grade));

        resultsTable.append(resultTr);
    });
        
    resultsTable.append(resultTr);
}

var showStudent = function(student){
    var form = $('#student-form');

    form.find('#inputFirstName').val(student.FirstName);
    form.find('#inputLastName').val(student.LastName);
    form.find('#inputFathersInitial').val(student.FathersInitial); 
    form.find('#inputStudentId').val(student.StudentId);
    form.find('#inputCnp').val(student.Cnp);

    var studentTable = $('#subjects-table');
    $(student.SubjectsList).each(function (index, item) {
        var studentTr = $('<tr></tr>');

        studentTr.append(createSubjectTd(item.Subject.Id));
        studentTr.append(createSubjectTd(item.Subject.Name));
        studentTr.append(createSubjectTd(item.Subject.StudyYear));
        studentTr.append(createSubjectTd(item.Subject.Semester));
        studentTr.append(createSubjectTd(item.Grade));
        studentTr.append(createSubjectTd(item.Subject.Credits));

        var removeButton = createRemoveButton(item.Subject.Id);
        removeButton.addClass("remove-button");
        studentTr.append(removeButton);
        
        studentTable.append(studentTr);
    });

    studentTable.on('click', '.remove-button', removeSubject);
}

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

$(document).ready(function () {
    var id = getUrlParameter('id');

    StudentService.Get(id, showStudent);

    $('#inputId').val(id);
    $('#inputQueryId').val(id);
    $('#student-form').submit(submitStudent);
    $('#details-form').submit(getDetails);
});