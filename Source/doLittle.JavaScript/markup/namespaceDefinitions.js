doLittle.namespace("doLittle.markup", {
    namespaceDefinitions: doLittle.Singleton(function () {

        this.create = function (prefix) {
            var definition = doLittle.markup.NamespaceDefinition.create({
                prefix: prefix,
            });
            return definition;
        };
    })
});