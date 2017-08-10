leala.viewModels.registerViewModel = function () {
    var self = this;

    self.model = new leala.models.register();

    self.submit = function () {
        if (self.model.isValid()) {
            return true;
        }
    }
};


var viewModel = new leala.viewModels.registerViewModel();
ko.applyBindings(viewModel);