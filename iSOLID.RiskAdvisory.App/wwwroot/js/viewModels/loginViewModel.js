leala.viewModels.loginViewModel = function () {
    var self = this;

    self.model = new leala.models.login();

    self.submit = function () {
        debugger;
        if (self.model.isValid()) {
            return true;
        }
    }
};

var viewModel = new leala.viewModels.loginViewModel();
ko.applyBindings(viewModel);