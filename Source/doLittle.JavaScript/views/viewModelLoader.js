doLittle.namespace("doLittle.views", {
    viewModelLoader: doLittle.Singleton(function (taskFactory, fileFactory, viewModelTypes) {
        this.load = function (path, region, viewModelParameters) {
            var promise = doLittle.execution.Promise.create();
            var file = fileFactory.create(path, doLittle.io.fileType.javaScript);
            var task = taskFactory.createViewModelLoad([file]);
            region.tasks.execute(task).continueWith(function () {
                viewModelTypes.beginCreateInstanceOfViewModel(path, region, viewModelParameters).continueWith(function (instance) {
                    promise.signal(instance);
                });
            });
            return promise;
        };
    })
});
