doLittle.namespace("doLittle.tasks", {
    HttpGetTask: doLittle.tasks.Task.extend(function (server, url, payload) {
        /// <summary>Represents a task that can perform Http Get requests</summary>

        this.execute = function () {
            var promise = doLittle.execution.Promise.create();
            server
                .get(url, payload)
                    .continueWith(function (result) {
                        promise.signal(result);
                    })
                    .onFail(function (error) {
                        promise.fail(error);
                    });
            return promise;
        };
    })
});