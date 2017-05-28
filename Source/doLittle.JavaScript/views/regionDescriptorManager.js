doLittle.namespace("doLittle.views", {
    regionDescriptorManager: doLittle.Singleton(function () {
        /// <summary>Represents a manager that knows how to manage region descriptors</summary>

        this.describe = function (view, region) {
            /// <summary>Describe a specific region related to a view</summary>
            /// <param name="view" type="doLittle.views.View">View related to the region</param>
            /// <param name="region" type="doLittle.views.Region">Region that needs to be described</param>
            var promise = doLittle.execution.Promise.create();
            var localPath = doLittle.Path.getPathWithoutFilename(view.path);
            var namespacePath = doLittle.namespaceMappers.mapPathToNamespace(localPath);
            if (namespacePath != null) {
                var namespace = doLittle.namespace(namespacePath);

                doLittle.views.Region.current = region;
                doLittle.dependencyResolver.beginResolve(namespace, "RegionDescriptor").continueWith(function (descriptor) {
                    descriptor.describe(region);
                    promise.signal();
                }).onFail(function () {
                    promise.signal();
                });
            } else {
                promise.signal();
            }
            return promise;
        };

        this.describeTopLevel = function (region) {

        };
    })
});