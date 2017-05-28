doLittle.namespace("doLittle.commands", {
    commandEvents: doLittle.Singleton(function () {
        this.succeeded = doLittle.Event.create();
        this.failed = doLittle.Event.create();
    })
});