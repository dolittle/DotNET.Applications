doLittle.namespace("doLittle.commands", {
    commandSecurityContextFactory: doLittle.Singleton(function () {
        this.create = function () {
            var context = doLittle.commands.CommandSecurityContext.create();
            return context;
        };
    })
});