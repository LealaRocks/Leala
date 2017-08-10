leala.models.project = function (d) {

    var self = this;
    var savePoint = undefined;

    self.id = ko.observable();
    self.version = ko.observable();

    self.name = ko.observable().extend({ required: true, maxLength: 100 });
    self.clientId = ko.observable().extend({ required: true });
    self.typeId = ko.observable().extend({ required: true });
    self.status = ko.observable('Draft').extend({ required: true })

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
        self.name(d.name);
        self.clientId(d.clientId);
        self.typeId(d.typeId);
    };

    if (d) {
        loadFromData(d);
    };

    self.createSavePoint();
};