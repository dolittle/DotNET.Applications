doLittle.namespace("doLittle.markup", {
    NamespaceDefinition: doLittle.Type.extend(function (prefix) {
        var self = this;
        this.prefix = prefix;

        this.targets = [];

        this.addTarget = function (target) {
            self.targets.push(target);
        };
    })
});