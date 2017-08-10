leala.models.control = function (d) {

    var self = this;

    self.controlType = ko.observable();
    self.selectListItems = ko.observableArray();
    self.decimalPlaces = ko.observableArray();
    self.currencyPrefix = ko.observable();


    if (d) {
        self.controlType(d.controlType);
    }
};