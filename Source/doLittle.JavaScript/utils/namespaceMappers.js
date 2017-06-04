doLittle.namespace("doLittle",{
    namespaceMappers: {

        mapPathToNamespace: function (path) {
            for (var mapperKey in doLittle.namespaceMappers) {
                var mapper = doLittle.namespaceMappers[mapperKey];
                if (typeof mapper.hasMappingFor === "function" && mapper.hasMappingFor(path)) {
                    var namespacePath = mapper.resolve(path);
                    return namespacePath;
                }
            }

            return null;
        }
    }
});