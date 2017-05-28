doLittle.namespace("doLittle.views", {
    viewLoader: doLittle.Singleton(function (viewModelManager, taskFactory, fileFactory, regionManager) {
        this.load = function (path,region) {
            var promise = doLittle.execution.Promise.create();

            var files = [];

            var viewFile = fileFactory.create(path, doLittle.io.fileType.html);
            if (path.indexOf("?") > 0) {
                viewFile.path.fullPath = viewFile.path.fullPath + path.substr(path.indexOf("?"));
            }
            files.push(viewFile);

            var viewModelPath = null;
            if (viewModelManager.hasForView(path)) {
                viewModelPath = viewModelManager.getViewModelPathForView(path);
                if (!viewModelManager.isLoaded(viewModelPath)) {
                    var viewModelFile = fileFactory.create(viewModelPath, doLittle.io.fileType.javaScript);
                    files.push(viewModelFile);
                }
            }

            var task = taskFactory.createViewLoad(files);
            region.tasks.execute(task).continueWith(function (view) {
                promise.signal(view);
            });

            return promise;
        };
    })
});