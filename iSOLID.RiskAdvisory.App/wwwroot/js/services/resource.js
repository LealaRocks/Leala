leala.services.resource = function (apiUrl, aggregate) {

    var apiUrl = apiUrl;
    var self = this;
    var aggregate = aggregate;
    var apiKey = leala.getCookieValue('api-key');

    var requestHeader = { 'api-key': apiKey }

    self.get = function (id) {

        var deferred = $.Deferred();

        if (id) {
            leala.ajax.get(apiUrl + '/' + id, null, requestHeader)
                .done(function (data, status, jqXHR) {
                    var obj = new aggregate(data);
                    deferred.resolve(obj, data, status, jqXHR);
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    deferred.reject(jqXHR, textStatus, errorThrown);
                });
        }
        else {
            leala.ajax.get(apiUrl, null, requestHeader)
                .done(function (data, status, jqXHR) {

                    var aggregates = new Array();

                    if (data) {
                        for (var i = 0; i < data.length; i++) {
                            var obj = new aggregate(data[i]);
                            aggregates.push(obj);
                        }
                    }
                    
                    deferred.resolve(aggregates, data, status, jqXHR);
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    deferred.reject(jqXHR, textStatus, errorThrown);
                });
        }

        return deferred.promise();

    };

    self.save = function (obj) {

        var deferred = $.Deferred();

        var json = ko.toJSON(obj);

        var successHandler = function (data, status, jqXHR) {

            var id = jqXHR.getResponseHeader('aggregate-id');
            var version = jqXHR.getResponseHeader('aggregate-version');

            obj.id(id);
            obj.version(version);
            obj.createSavePoint();

            toastr["success"]('Save Complete');

            deferred.resolve(obj, data, status, jqXHR);

        };
        var errorHandler = function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 409) {
                toastr["error"]('Save failed: A conflicting update has been detected.');
            }
            else {
                toastr["error"]('Save Failed: ' + errorThrown);
            }

            deferred.reject(obj, jqXHR, textStatus, errorThrown);
        };

        if (obj.id()) {
            leala.ajax.put(apiUrl + '/' + obj.id(), json, requestHeader)
                .done(successHandler)
                .fail(errorHandler);
        }
        else {
            leala.ajax.post(apiUrl, json, requestHeader)
                .done(successHandler)
                .fail(errorHandler);
        }

        return deferred.promise();
    };

}