doLittle.namespace("doLittle.views", {
    UriMapperPathResolver: doLittle.views.PathResolver.extend(function () {
        this.canResolve = function (element, path) {
            var closest = $(element).closest("[data-urimapper]");
            if (closest.length === 1) {
                var mapperName = $(closest[0]).data("urimapper");
                if (doLittle.uriMappers[mapperName].hasMappingFor(path) === true) {
                    return true;
                }
            }
            return doLittle.uriMappers.default.hasMappingFor(path);
        };

        this.resolve = function (element, path) {
            var closest = $(element).closest("[data-urimapper]");
            if (closest.length === 1) {
                var mapperName = $(closest[0]).data("urimapper");
                if (doLittle.uriMappers[mapperName].hasMappingFor(path) === true) {
                    return doLittle.uriMappers[mapperName].resolve(path);
                }
            }
            return doLittle.uriMappers.default.resolve(path);
        };
    })
});
if (typeof doLittle.views.pathResolvers !== "undefined") {
    doLittle.views.pathResolvers.UriMapperPathResolver = doLittle.views.UriMapperPathResolver;
}