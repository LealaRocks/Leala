leala.models.dataExceptionCategory = function (d) {

    var self = this;

    self.name = ko.observable().extend({ required: true, maxLength: 100 });
    self.code = ko.observable().extend({ required: true, maxLength: 10 });
    self.exceptions = ko.observableArray();

    if (d) {
        self.name(d.name);
        self.code(d.code);

        self.exceptions.removeAll();

        var exceptions = new Array();
        for (var i = 0; i < d.exceptions.length; i++) {
            var exception = new leala.models.dataException(d.exceptions[i]);
            exceptions.push(exception)
        }
        self.exceptions(exceptions);
    }

    self.displayText = ko.pureComputed(function () {
        if (self.name() && self.code()) {
            return self.code() + ': ' + self.name();
        }
        return null;
    });
};