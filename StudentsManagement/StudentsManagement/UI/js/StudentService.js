StudentService = function () {
    self = {}

    self.url = "http://localhost/StudentsManagement/api/Student/";

    self.addUrl = "http://localhost/StudentsManagement/api/Student/Add";

    self.GetAll = function (func) {
        Service.Get(this.url).done(function (data) {
            func(data);
        });
    };

    self.Get = function (id) {
        var student = {}

        var url = this.url + id;
        Service.Get(url).done(function (data) {
            student = data;
        });

        return student;
    }

    self.Post = function (student) {
        Service.Post(this.addUrl, student).done(function () {
                document.location.replace('Student.html');
            }
        );
    }

    self.GetByParameter = function (parameter, func) {
        var url = this.url + parameter;

        Service.Get(url).done(function(data){
            func(data);
        });
    }

    return self;
}();