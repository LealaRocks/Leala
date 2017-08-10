leala.models.tab = function (d) {

    var self = this;

    self.name = ko.observable().extend({ required: true, maxLength: 100 });
    self.controls = ko.observableArray();

    if (d) {
        self.name(d.name);
        self.controls.removeAll();
        var controls = new Array();
        for (var i = 0; i < d.controls.length; i++) {
            var control = new leala.models.tab(d.controls[i]);
            controls.push(control);
        }
        self.controls(controls);
    }
}