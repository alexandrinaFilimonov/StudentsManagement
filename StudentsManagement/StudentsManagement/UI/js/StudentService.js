StudentService = function () {
    var url = "http://localhost:39515/api/Student/";
    self = {}

    self.GetAll = function (func) {
        Service.Get(url).done(function (data) {
            func(data)
        });
    };

    self.Get = function (id) {
        var student = {}

        var url = url + id;
        Service.Get(url).done(function (data) {
            student = data;
        });

        return student;
    }

    return self;
}();