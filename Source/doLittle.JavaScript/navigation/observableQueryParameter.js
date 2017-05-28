doLittle.namespace("doLittle.navigation", {
    observableQueryParameterFactory: doLittle.Singleton(function () {
        var self = this;

        var historyEnabled = typeof History !== "undefined" && typeof History.Adapter !== "undefined";

        this.create = function (parameterName, defaultValue, navigationManager) {

            function getState() {
                var uri = navigationManager.getCurrentLocation();
                if (uri.parameters.hasOwnProperty(parameterName)) {
                    return uri.parameters[parameterName];
                }

                return null;
            }

            var observable = null;

            if (historyEnabled) {
                History.Adapter.bind(window, "statechange", function () {
                    if (observable != null) {
                        observable(getState());
                    }
                });
            } else {
                window.addEventListener("hashchange", function () {
                    if (observable != null) {
                        var state = getState();
                        if (observable() !== state) {
                            observable(state);
                        }
                    }
                }, false);
            }

            var state = getState();
            observable = ko.observable(state || defaultValue);

            function getQueryStringParametersWithValueForParameter(url, parameterValue) {
                var parameters = doLittle.hashString.decode(url);
                parameters[parameterName] = parameterValue;

                var queryString = "";
                var parameterIndex = 0;
                for (var parameter in parameters) {
                    var value = parameters[parameter];
                    if (!doLittle.isNullOrUndefined(value)) {
                        if (parameterIndex > 0) {
                            queryString += "&";
                        }
                        queryString += parameter + "=" + value;
                    }
                    parameterIndex++;
                }

                return queryString;
            }

            function cleanQueryString(queryString) {
                if (queryString.indexOf("#") === 0 || queryString.indexOf("?") === 0) {
                    queryString = queryString.substr(1);
                }
                return queryString;
            }

            observable.subscribe(function (newValue) {
                var queryString;
                if (historyEnabled) {
                    var state = History.getState();
                    state[parameterName] = newValue;
                    queryString = "?" + getQueryStringParametersWithValueForParameter(cleanQueryString(state.url), newValue);
                    History.pushState(state, state.title, queryString);
                } else {
                    queryString = "#" + getQueryStringParametersWithValueForParameter(cleanQueryString(document.location.hash), newValue);
                    document.location.hash = queryString;
                }
            });

            return observable;
        };
    })
});

ko.observableQueryParameter = function (parameterName, defaultValue) {
    var navigationManager = doLittle.navigation.navigationManager;
    var observable = doLittle.navigation.observableQueryParameterFactory.create().create(parameterName, defaultValue, navigationManager);
    return observable;
};
