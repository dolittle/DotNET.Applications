doLittle.namespace("doLittle.values", {
    valueProviders: doLittle.Singleton(function () {

        this.isKnown = function (name) {
            var found = false;
            var valueProviders = doLittle.values.ValueProvider.getExtenders();
            valueProviders.forEach(function (valueProviderType) {
                if (valueProviderType._name.toLowerCase() === name) {
                    found = true;
                    return;
                }
            });
            return found;
        };

        this.getInstanceOf = function (name) {
            var instance = null;
            var valueProviders = doLittle.values.ValueProvider.getExtenders();
            valueProviders.forEach(function (valueProviderType) {
                if (valueProviderType._name.toLowerCase() === name) {
                    instance = valueProviderType.create();
                    return;
                }
            });

            return instance;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.valueProviders = doLittle.values.valueProviders;