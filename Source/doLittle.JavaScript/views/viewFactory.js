doLittle.namespace("doLittle.views", {
    viewFactory: doLittle.Singleton(function () {
        this.createFrom = function (path) {
            var view = doLittle.views.View.create({
                path: path
            });
            return view;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.viewFactory = doLittle.views.viewFactory;