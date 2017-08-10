leala.services.router = function (initalView) {
    var self = this;

    self.page = ko.observable(initalView);


    self.navigateSection = function (m, e) {

        var page = e.target.pathname;

        if (page == self.page())
            return;

        self.page(page);
        history.pushState({ page: page }, e.target.textContent, e.target.href);

        return e.preventDefault();
    }


    window.addEventListener('popstate', function (e) {
        if (e.state.page == self.page())
            return;

        viewModel.page(e.state.page);
    });

};