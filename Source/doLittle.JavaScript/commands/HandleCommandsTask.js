doLittle.namespace("doLittle.commands", {
    HandleCommandsTask: doLittle.tasks.ExecutionTask.extend(function (commands, server) {
        /// <summary>Represents a task that can handle an array of command</summary>
        var self = this;

        this.names = [];
        commands.forEach(function (command) {
            self.names.push(command.name);
        });

        this.execute = function () {
            var promise = doLittle.execution.Promise.create();

            var commandRequests = [];

            commands.forEach(function (command) {
                command.isBusy(true);
                var commandRequest= doLittle.commands.CommandRequest.createFrom(command);
                commandRequests.push(commandRequest);
            });

            var parameters = {
                commands: commandRequests
            };

            var url = "/doLittle/CommandCoordinator/HandleMany";

            server.post(url, parameters).continueWith(function (results) {
                var commandResults = [];

                results.forEach(function (result) {
                    var commandResult = doLittle.commands.CommandResult.createFrom(result);
                    commandResults.push(commandResult);
                });
                promise.signal(commandResults);
            });

            return promise;
        };
    })
});