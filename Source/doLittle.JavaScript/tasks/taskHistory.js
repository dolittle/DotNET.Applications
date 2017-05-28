doLittle.namespace("doLittle.tasks", {
    taskHistory: doLittle.Singleton(function (systemClock) {
        /// <summary>Represents the history of tasks that has been executed since the start of the application</summary>
        var self = this;

        var entriesById = {};

        /// <field param="entries" type="observableArray">Observable array of entries</field>
        this.entries = ko.observableArray();

        this.begin = function (task) {
            var id = doLittle.Guid.create();

            try {
                var entry = doLittle.tasks.TaskHistoryEntry.create();

                entry.type = task._type._name;

                var content = {};

                for (var property in task) {
                    if (property.indexOf("_") !== 0 && task.hasOwnProperty(property) && typeof task[property] !== "function") {
                        content[property] = task[property];
                    }
                }

                entry.content = JSON.stringify(content);

                entry.begin(systemClock.nowInMilliseconds());
                entriesById[id] = entry;
                self.entries.push(entry);
            } catch (ex) {
                // Todo: perfect place for logging something
            }
            return id;
        };

        this.end = function (id, result) {
            if (entriesById.hasOwnProperty(id)) {
                var entry = entriesById[id];
                entry.end(systemClock.nowInMilliseconds());
                entry.result(result);
            }
        };

        this.failed = function (id, error) {
            if (entriesById.hasOwnProperty(id)) {
                var entry = entriesById[id];
                entry.end(systemClock.nowInMilliseconds());
                entry.error(error);
            }
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.taskHistory = doLittle.tasks.taskHistory;