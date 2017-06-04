doLittle.namespace("doLittle",{
    WellKnownTypesDependencyResolver: function () {
        var self = this;
        this.types = doLittle.WellKnownTypesDependencyResolver.types;

        this.canResolve = function (namespace, name) {
            return self.types.hasOwnProperty(name);
        };

        this.resolve = function (namespace, name) {
            return self.types[name];
        };
    }
});

doLittle.WellKnownTypesDependencyResolver.types = {
    options: {}
};
