describe("Student", function(){

  it("should be able to add td with value", function() {
	var td = createStudentTd("some_value");
	expect(td.text()).toEqual("some_value");
  });
	
});
