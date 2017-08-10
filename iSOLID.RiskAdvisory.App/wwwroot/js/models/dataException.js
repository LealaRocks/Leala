leala.models.dataException = function (d) {
    var self = this;

    self.name = ko.observable().extend({ required: true, maxLength: 100 });
    self.code = ko.observable().extend({ required: true, maxLength: 10 });

    self.displayText = ko.pureComputed(function () {
        if (self.name() && self.code()) {
            return self.code() + ': ' + self.name();
        }
        return null;
    });
};