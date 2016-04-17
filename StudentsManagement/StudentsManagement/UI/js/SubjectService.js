SubjectService = function () {
    self = {}

    self.url = "http://localhost:39515/api/Subject/";

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
        Service.Post(this.url, student).done(function () {
            document.location.replace('Subject.html');
        }
        );
    }

    return self;
}();