doLittle.dependencyResolvers.DOMRootDependencyResolver = {
    canResolve: function (namespace, name) {
        return name === "DOMRoot";
    },

    resolve: function (namespace, name) {
        if (document.body != null && typeof document.body !== "undefined") {
            return document.body;
        }

        var promise = doLittle.execution.Promise.create();
        doLittle.dependencyResolvers.DOMRootDependencyResolver.promises.push(promise);
        return promise;
    }
};

doLittle.dependencyResolvers.DOMRootDependencyResolver.promises = [];
doLittle.dependencyResolvers.DOMRootDependencyResolver.documentIsReady = function () {
    doLittle.dependencyResolvers.DOMRootDependencyResolver.promises.forEach(function (promise) {
        promise.signal(document.body);
    });
};