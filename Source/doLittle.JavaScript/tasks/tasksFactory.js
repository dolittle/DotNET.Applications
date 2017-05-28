doLittle.namespace("doLittle.tasks", {
    tasksFactory: doLittle.Singleton(function () {
        this.create = function () {
            var tasks = doLittle.tasks.Tasks.create();
            return tasks;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.tasksFactory = doLittle.tasks.tasksFactory;