doLittle.namespace("doLittle", {
    dependencyResolvers: (function () {
        return {
            getAll: function () {
                var resolvers = [
                    new doLittle.WellKnownTypesDependencyResolver(),
                    new doLittle.DefaultDependencyResolver(),
                    new doLittle.KnownArtifactTypesDependencyResolver(),
                    new doLittle.KnownArtifactInstancesDependencyResolver(),

                ];
                for (var property in this) {
                    if (property.indexOf("_") !== 0 &&
                        this.hasOwnProperty(property) &&
                        typeof this[property] !== "function") {
                        resolvers.push(this[property]);
                    }
                }
                return resolvers;
            }
        };
    })()
});