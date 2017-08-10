ko.validation.init({
    decorateElement: true,
    insertMessages: true,
    errorMessageClass: 'help-block',
    errorElementClass: 'has-error'
});

ko.validation.rules['areSame'] = {
    getValue: function (o) {
        return (typeof o === 'function' ? o() : o);
    },
    validator: function (val, otherField) {
        return val === this.getValue(otherField);
    },
    message: 'The fields must have the same value'
};


ko.validation.rules['passwordComplexity'] = {
    validator: function (val) {
        return /(?=^[^\s]{6,128}$)((?=.*?\d)(?=.*?[A-Z])(?=.*?[a-z])|(?=.*?\d)(?=.*?[^\w\d\s])(?=.*?[a-z])|(?=.*?[^\w\d\s])(?=.*?[A-Z])(?=.*?[a-z])|(?=.*?\d)(?=.*?[A-Z])(?=.*?[^\w\d\s]))^.*/.test('' + val + '');
    },
    message: 'Password must be between 6 and 128 characters long and contain three of the following 4 items: upper case letter, lower case letter, a symbol, a number'
};

ko.validation.rules['isUnique'] = {
    validator: function (newVal, options) {
        if (options.predicate && typeof options.predicate !== "function")
            throw new Error("Invalid option for isUnique validator. The 'predicate' option must be a function.");

        var array = options.array || options;
        var count = 0;
        ko.utils.arrayMap(ko.utils.unwrapObservable(array), function (existingVal) {
            if (equalityDelegate()(existingVal, newVal)) count++;
        });
        return count < 2;

        function equalityDelegate() {
            return options.predicate ? options.predicate : function (v1, v2) { return v1 === v2; };
        }
    },
    message: 'This value is a duplicate',
};

ko.validation.registerExtenders();

window.leala = {

    models: {},
    services: {},
    viewModels: {},

    setContent: function (url, targetDiv, koModel) {

        leala.ajax.get(url).done(function (response) {

            $(targetDiv).html(response);

            if (koModel) {
                ko.applyBindings(koModel, $(targetDiv)[0]);
            }
        });
    },

    ajax: {
        post: function (url, data, headers) {

            var deferred = $.Deferred();

            $.ajax({
                url: url,
                type: 'POST',
                
                data: data,
                success: function (data, textStatus, jqXHR) {
                    
                    deferred.resolve(data, textStatus, jqXHR);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    deferred.reject(jqXHR, textStatus, errorThrown);
                },
                contentType: 'application/json',
                headers: headers
            });

            return deferred.promise();

        },
        get: function (url, data, headers) {

            var deferred = $.Deferred();

            $.ajax({
                url: url,
                type: 'GET',
                data: data,
                success: function (data, textStatus, jqXHR) {
                    deferred.resolve(data, textStatus, jqXHR);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    deferred.reject(jqXHR, textStatus, errorThrown);
                },
                headers: headers
            });

            return deferred.promise();
        },
        delete: function (url, data, headers) {

            var deferred = $.Deferred();

            $.ajax({
                url: url,
                type: 'DELETE',
                dataType: 'json',
                data: data,
                success: function(data, textStatus, jqXHR) {
                    deferred.resolve(data, textStatus, jqXHR);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    deferred.reject(jqXHR, textStatus, errorThrown);
                },
                contentType: 'application/json',
                headers: headers
            });

            return deferred.promise();

        },
        patch: function (url, data, headers) {

            var deferred = $.Deferred();

            $.ajax({
                url: url,
                type: 'PATCH',
                data: data,
                contentType: 'application/json',
                success: function(data, textStatus, jqXHR) {
                    deferred.resolve(data, textStatus, jqXHR);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    deferred.reject(jqXHR, textStatus, errorThrown);
                },
                headers: headers
            });

            return deferred.promise();
        },
        put: function (url, data, headers) {

            var deferred = $.Deferred();

            $.ajax({
                url: url,
                type: 'PUT',
                data: data,
                success: function(data, textStatus, jqXHR) {
                    deferred.resolve(data, textStatus, jqXHR);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    deferred.reject(jqXHR, textStatus, errorThrown);
                },
                contentType: 'application/json',
                headers: headers
            });

            return deferred.promise();
        }
    },

    getCookieValue: function (cname) {

        var name = cname + '=';
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');

        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];

            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }

            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }

        return '';
    }
}