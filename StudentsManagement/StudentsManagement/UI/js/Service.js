Service = function () {
    self = {};

    self.Get = function (url) {
        return $.ajax({
            url: url
        });
    }

    return self;
}();