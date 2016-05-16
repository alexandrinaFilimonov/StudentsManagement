describe("Student", function(){

  beforeEach( function(){
    //fixture will be reset before each test
    loadFixtures( 'Student.html');
  } );

  //test createStudentTd
  it("should be able to add td with value", function() {
	var td = createStudentTd("some_value");
	expect(td.prop('outerHTML')).toEqual("<td>some_value</td>");
	expect(td.text()).toEqual("some_value");
  });

  it("should be able to add td with empty value", function() {
	var td = createStudentTd("");
	expect(td.prop('outerHTML')).toEqual("<td></td>");
	expect(td.text()).toEqual("");
  });

  it("should be able to add td with undefined value", function() {
	var td = createStudentTd(undefined);
	expect(td.prop('outerHTML')).toEqual("<td></td>");
	expect(td.text()).toEqual("");
  });

  //test addStudentDefault
  it("should be able to add student table", function() {
	var student = studentTest();
	var tr = $('<tr></tr>');
	addStudentDefault(student, tr);
	expect(tr.html()).toEqual("<td>1</td><td>First_Name</td><td>I</td><td>Last_Name</td><td>1</td>");
  });

  it("should not be able to add student table when table is null", function() {
	var student = studentTest();
	var tr = null;	
	var toTest = function() {
		addStudentDefault(student, tr)
	};
	expect(toTest).toThrowError(TypeError);
  });

  it("should not be able to add student table when student is null", function() {
	var student = null;
	var tr = $('<tr></tr>');
	var toTest = function() {
		addStudentDefault(student, tr)
	};
	expect(toTest).toThrowError(TypeError);
  });

  it("should not be able to add student table when table is undefined", function() {
	var student = studentTest();	
	var toTest = function() {
		addStudentDefault(student, undefined)
	};
	expect(toTest).toThrowError(TypeError);
  });

  it("should not be able to add student table when student is undefined", function() {
	var tr = $('<tr></tr>');	
	var toTest = function() {
		addStudentDefault(undefined, tr)
	};
	expect(toTest).toThrowError(TypeError);
  });

  //test showAll
  it("should show all", function() {
	var students = [studentTest(), studentTest()];	
	showAll(students);
	expect($('#student-table').html()).toEqual('<tr style="cursor: pointer;" class="table-row"><td>1</td><td>First_Name</td><td>I</td><td>Last_Name</td><td>1</td></tr><tr style="cursor: pointer;" class="table-row"><td>1</td><td>First_Name</td><td>I</td><td>Last_Name</td><td>1</td></tr>') ;
  });

  it("should show all when array is empty", function() {
	var students = [];	
	showAll(students);
	expect($('#student-table').html()).toEqual("") ;
  });

  it("should show all when array contains null", function() {
	var students = [studentTest(), null];	
	showAll(students);
	expect($('#student-table').html()).toEqual("") ;
  });

    //test showBudgeted
  it("should show budgeted", function() {
	var students = student_array();	
	showBudgeted(students);
	expect($('#student-table').html()).toEqual('<tr style="cursor: pointer;" class="table-row"><td>1</td><td>First_Name</td><td>I</td><td>Last_Name</td><td>1</td><td>Fee payer</td><td></td></tr><tr style="cursor: pointer;" class="table-row"><td>1</td><td>First_Name</td><td>I</td><td>Last_Name</td><td>1</td><td>Fee payer</td><td></td></tr>') ;
  });

  it("should show budgeted when empty array", function() {
	var students = [];	
	showBudgeted(students);
	expect($('#student-table').html()).toEqual('') ;
  });

  it("should show budgeted when array contains null", function() {
	var students = [null];	
	showBudgeted(students);
	expect($('#student-table').html()).toEqual('') ;
  });

  it("should show promotion", function() {
	var students = student_array();	
	showPromotion(students);
	expect($('#student-table').html()).toEqual('<tr style="cursor: pointer;" class="table-row"><td>1</td><td>First_Name</td><td>I</td><td>Last_Name</td><td>1</td><td>Failed</td></tr><tr style="cursor: pointer;" class="table-row"><td>1</td><td>First_Name</td><td>I</td><td>Last_Name</td><td>1</td><td>Failed</td></tr>') ;
  });

  it("should show promotion when empty array", function() {
	var students = [];	
	showPromotion(students);
	expect($('#student-table').html()).toEqual('') ;
  });
	
});

//test data function to create student object
studentTest = function() {
	var student = {};
	student.FirstName = "First_Name";
	student.LastName = "Last_Name";
	student.FathersInitial = "I";
	student.StudentId = 1;
	student.Cnp = "1921211272770";
	student.SubjectId = 1;
	student.Id=1;
	return student;
};

student_array = function() {
	var item1 = {};
	var item2 = {};

	item1.Student = studentTest();
	item2.Student = studentTest();
	
	return [item1, item2];
};
