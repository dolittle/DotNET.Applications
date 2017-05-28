doLittle.namespace("doLittle.values", {
    valueConsumers: doLittle.Singleton(function () {

        this.getFor = function (instance, propertyName) {
            var consumer = doLittle.values.DefaultValueConsumer.create({
                target: instance,
                property: propertyName
            });
            return consumer;
        };

    })
});
doLittle.WellKnownTypesDependencyResolver.types.valueConsumers = doLittle.values.valueConsumers;