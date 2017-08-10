leala.models.login = function () {

    var self = this;

    self.email = ko.observable().extend({ required: true, maxLength: 50, email: true });
    self.password = ko.observable().extend({ required: true });

    self.isValid = ko.pureComputed(function () {
        var errors = ko.validation.group(self, { deep: true })();
        return errors.length == 0;
    });
}