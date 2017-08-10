leala.models.register = function () {
    var self = this;

    self.firstName = ko.observable().extend({ required: true, maxLength: 50 });
    self.lastName = ko.observable().extend({ required: true, maxLength: 50 });
    self.email = ko.observable().extend({ required: true, maxLength: 50, email: true });
    self.password = ko.observable().extend({ required: true, maxLength: 50, passwordComplexity: true });
    self.confirmPassword = ko.observable().extend({ required: true, maxLength: 50, areSame: self.password });

    self.isValid = ko.pureComputed(function () {
        var errors = ko.validation.group(self, { deep: true })();
        return errors.length == 0;
    });
};