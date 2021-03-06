﻿leala.models.client = function (d) {

    var self = this;
    var savePoint = undefined;

    self.id = ko.observable();
    self.name = ko.observable().extend({ required: true, maxLength: 100 });
    self.version = ko.observable();

    self.createSavePoint = function () {
        savePoint = ko.toJSON(self);
    };

    self.undoChanges = function () {
        var d = JSON.parse(savePoint);
        loadFromData(d);
    };

    var loadFromData = function (d) {
        self.id(d.id);
        self.name(d.name);
        self.version(d.version);
    };

    if (d) {
        loadFromData(d);
    };

    self.createSavePoint();
};