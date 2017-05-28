doLittle.namespace("doLittle.tasks", {
    LoadTask: doLittle.tasks.Task.extend(function () {
        /// <summary>Represents a base task that represents anything that is loading things</summary>
        this.execute = function () {
            var promise = doLittle.execution.Promise.create();
            promise.signal();
            return promise;
        };
    })
});