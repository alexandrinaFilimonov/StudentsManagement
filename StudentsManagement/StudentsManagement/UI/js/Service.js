Service = function () {
    self = {};

    self.Get = function (url) {
        return $.ajax({
            type: "GET",
            url: url
        });
    }

    self.Post = function (url, data) {
        return $.ajax({
            type: "POST",
            url: url,
            data: data
        });
    }

    self.PostUpload = function(url, data) {
        return $.ajax({
            type: "POST",
            url: url,
            data: data,
            contentType: false,
            processData: false
        });
    }

    return self;
}();