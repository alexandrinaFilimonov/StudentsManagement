StudentService = function () {
    self = {}

    self.url = "http://localhost:39515//api/Student/";

    self.addUrl = "http://localhost:39515/api/Student/Add";

    self.uploadUrl = "http://localhost:39515/api/Student/Upload";

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

    self.UploadFile = function (files, onSuccess, onError) {
        Service.PostUpload(this.uploadUrl, files)
            .done(function (data) {
                debugger;
                onSuccess(data);})
            .error(onError);
    }

    return self;
}();