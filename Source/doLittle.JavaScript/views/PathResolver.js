doLittle.namespace("doLittle.views", {
    PathResolver: doLittle.Type.extend(function () {
        this.canResolve = function (element, path) {
            return false;
        };

        this.resolve = function (element, path) {

        };
    })
});