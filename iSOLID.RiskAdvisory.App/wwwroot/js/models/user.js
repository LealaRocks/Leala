leala.models.user = function (d) {

    var self = this;
    var savePoint = undefined;

    self.id = ko.observable();
    self.version = ko.observable();

    self.firstName = ko.observable().extend({ required: true, maxLength: 50 });
    self.lastName = ko.observable().extend({ required: true, maxLength: 50 });
    self.email = ko.observable().extend({ required: true, maxLength: 50 });
    
    self.fullName = ko.computed(function () {
        return self.firstName() + ' ' + self.lastName();
    });

    self.createSavePoint = function () {
        savePoint = ko.toJSON(self);
    };

    self.undoChanges = function () {
        var d = JSON.parse(savePoint);
        loadFromData(d);
    };

    var loadFromData = function (d) {
        self.id(d.id);
        self.version(d.version);
        self.firstName(d.firstName);
        self.lastName(d.lastName);
        self.email(d.email);
    };

    if (d) {
        loadFromData(d);
    };

    self.createSavePoint();

};

leala.models.userComparer = function (a, b) {
    return a.fullName() == b.fullName() ? 0 : (a.fullName() < b.fullName() ? -1 : 1);
};