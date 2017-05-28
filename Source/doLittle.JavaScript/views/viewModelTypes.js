doLittle.namespace("doLittle.views", {
    viewModelTypes: doLittle.Singleton(function () {
        var self = this;

        function getNamespaceFrom(path) {
            var localPath = doLittle.Path.getPathWithoutFilename(path);
            var namespacePath = doLittle.namespaceMappers.mapPathToNamespace(localPath);
            if (namespacePath != null) {
                var namespace = doLittle.namespace(namespacePath);
                return namespace;
            }
            return null;
        }

        function getTypeNameFrom(path) {
            var localPath = doLittle.Path.getPathWithoutFilename(path);
            var filename = doLittle.Path.getFilenameWithoutExtension(path);
            return filename;
        }


        this.isLoaded = function (path) {
            var namespace = getNamespaceFrom(path);
            if (namespace != null) {
                var typename = getTypeNameFrom(path);
                return typename in namespace;
            }
            return false;
        };

        function getViewModelTypeForPathImplementation(path) {
            var namespace = getNamespaceFrom(path);
            if (namespace != null) {
                var typename = getTypeNameFrom(path);
                if (doLittle.isType(namespace[typename])) {
                    return namespace[typename];
                }
            }

            return null;
        }

        this.getViewModelTypeForPath = function (path) {
            var type = getViewModelTypeForPathImplementation(path);
            if (doLittle.isNullOrUndefined(type)) {
                var deepPath = path.replace(".js", "/index.js");
                type = getViewModelTypeForPathImplementation(deepPath);
                if (doLittle.isNullOrUndefined(type)) {
                    deepPath = path.replace(".js", "/Index.js");
                    getViewModelTypeForPathImplementation(deepPath);
                }
            }

            return type;
        };


        this.beginCreateInstanceOfViewModel = function (path, region, viewModelParameters) {
            var promise = doLittle.execution.Promise.create();

            var type = self.getViewModelTypeForPath(path);
            if (type != null) {
                var previousRegion = doLittle.views.Region.current;
                doLittle.views.Region.current = region;

                viewModelParameters = viewModelParameters || {};
                viewModelParameters.region = region;

                type.beginCreate(viewModelParameters)
                        .continueWith(function (instance) {
                            promise.signal(instance);
                        }).onFail(function (error) {
                            console.log("ViewModel '" + path + "' failed instantiation");
                            throw error;
                        });
            } else {
                console.log("ViewModel '" + path + "' does not exist");
                promise.signal(null);
            }

            return promise;
        };

    })
});
doLittle.WellKnownTypesDependencyResolver.types.viewModelTypes = doLittle.views.viewModelTypes;