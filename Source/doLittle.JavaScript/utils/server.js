doLittle.namespace("doLittle", {
    server: doLittle.Singleton(function () {
        var self = this;

        this.target = "";

        function deserialize(data) {
            if (doLittle.isArray(data)) {
                var items = [];
                data.forEach(function (item) {
                    items.push(deserialize(item));
                });
                return items;
            } else {
                for (var property in data) {
                    if (doLittle.isArray(data[property])) {
                        data[property] = deserialize(data[property]);
                    } else {
                        var value = data[property];

                        if (doLittle.isNumber(value)) {
                            data[property] = parseFloat(value);
                        } else {
                            data[property] = data[property];
                        }
                    }
                }
                return data;
            }
        }


        this.post = function (url, parameters) {
            var promise = doLittle.execution.Promise.create();

            if (!doLittle.Uri.isAbsolute(url)) {
                url = self.target + url;
            }

            var actualParameters = {};

            for (var property in parameters) {
                actualParameters[property] = JSON.stringify(parameters[property]);
            }

            $.ajax({
                url: url,
                type: "POST",
                dataType: 'json',
                data: JSON.stringify(actualParameters),
                contentType: 'application/json; charset=utf-8',
                complete: function (result) {
                    var data = $.parseJSON(result.responseText);
                    deserialize(data);
                    promise.signal(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    promise.fail(jqXHR);
                }
            });

            return promise;
        };

        this.get = function (url, parameters) {
            var promise = doLittle.execution.Promise.create();

            if (!doLittle.Uri.isAbsolute(url)) {
                url = self.target + url;
            }

            if (doLittle.isObject(parameters)) {
                for (var parameterName in parameters) {
                    if (doLittle.isArray(parameters[parameterName])) {
                        parameters[parameterName] = JSON.stringify(parameters[parameterName]);
                    }
                }
            }

            $.ajax({
                url: url,
                type: "GET",
                dataType: 'json',
                data: parameters,
                contentType: 'application/json; charset=utf-8',
                complete: function (result, textStatus) {
                    var data = $.parseJSON(result.responseText);
                    deserialize(data);
                    promise.signal(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    promise.fail(jqXHR);
                }
            });

            return promise;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.server = doLittle.server;