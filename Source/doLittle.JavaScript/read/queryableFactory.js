doLittle.namespace("doLittle.read", {
    queryableFactory: doLittle.Singleton(function () {
        this.create = function (query, region) {
            var queryable = doLittle.read.Queryable.new({
                query: query
            }, region);
            return queryable;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.queryableFactory = doLittle.interaction.queryableFactory;