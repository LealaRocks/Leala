leala.models.script = function (d) {

    var self = this;
    var savePoint = undefined;

    self.id = ko.observable();
    self.version = ko.observable();
    self.name = ko.observable().extend({ required: true, maxLength: 100 });
    self.tabs = ko.observableArray();
    self.exceptionCategories = ko.observableArray();


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

        self.tabs.removeAll();
        var tabs = new Array();
        for (var i = 0; i < d.tabs.length; i++) {
            var tab = new leala.models.tab(d.tabs[i]);
            tabs.push(tab);
        }
        self.tabs(tabs);

        self.exceptionCategories.removeAll();
        var exceptionCategories = new Array();
        for (var i = 0; i < d.exceptionCategories.length; i++) {
            var category = new leala.models.dataExceptionCategory(d.exceptionCategories[i]);
            exceptionCategories.push(category);
        }
        self.exceptionCategories(exceptionCategories);
    };

    if (d) {
        loadFromData(d);
    };

    self.createSavePoint();
};

