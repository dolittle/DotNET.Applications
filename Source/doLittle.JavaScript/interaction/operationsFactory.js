doLittle.namespace("doLittle.interaction", {
    operationsFactory: doLittle.Singleton(function () {
        this.create = function () {
            var operations = doLittle.interaction.Operations.create();
            return operations;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.operationsFactory = doLittle.interaction.operationsFactory;