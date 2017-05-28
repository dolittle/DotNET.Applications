doLittle.namespace("doLittle.tasks", {
    FileLoadTask: doLittle.tasks.LoadTask.extend(function (files, fileManager) {
        /// <summary>Represents a task for loading view related files asynchronously</summary>
        this.files = files;

        var self = this;

        this.files = [];
        files.forEach(function (file) {
            self.files.push(file.path.fullPath);
        });

        this.execute = function () {
            var promise = doLittle.execution.Promise.create();

            fileManager.load(files).continueWith(function (instances) {
                promise.signal(instances);
            });
            return promise;
        };
    })
});