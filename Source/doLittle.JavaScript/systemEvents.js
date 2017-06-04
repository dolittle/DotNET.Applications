doLittle.namespace("doLittle",{
    systemEvents: doLittle.Singleton(function () {
        this.readModels = doLittle.read.readModelSystemEvents.create();
        this.commands = doLittle.commands.commandEvents.create();
    })
});
doLittle.WellKnownTypesDependencyResolver.types.systemEvents = doLittle.systemEvents;