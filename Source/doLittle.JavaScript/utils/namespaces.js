doLittle.namespace("doLittle", {
    namespaces: doLittle.Singleton(function() {
        var self = this;

        this.stripPath = function (path) {
            if (path.startsWith("/")) {
                path = path.substr(1);
            }
            if (path.endsWith("/")) {
                path = path.substr(0, path.length - 1);
            }
            return path;
        };

        this.initialize = function () {
            var scripts = doLittle.assetsManager.getScripts();
            if (typeof scripts === "undefined") {
                return;
            }

            scripts.forEach(function (fullPath) {
                var path = doLittle.Path.getPathWithoutFilename(fullPath);
                path = self.stripPath(path);

                for (var mapperKey in doLittle.namespaceMappers) {
                    var mapper = doLittle.namespaceMappers[mapperKey];
                    if (typeof mapper.hasMappingFor === "function" && mapper.hasMappingFor(path)) {
                        var namespacePath = mapper.resolve(path);
                        var namespace = doLittle.namespace(namespacePath);

                        var root = "/" + path + "/";
                        namespace._path = root;

                        if (typeof namespace._scripts === "undefined") {
                            namespace._scripts = [];
                        }

                        var fileIndex = fullPath.lastIndexOf("/");
                        var file = fullPath.substr(fileIndex + 1);
                        var extensionIndex = file.lastIndexOf(".");
                        var system = file.substr(0, extensionIndex);

                        namespace._scripts.push(system);
                    }
                }
            });
        };
    })
});