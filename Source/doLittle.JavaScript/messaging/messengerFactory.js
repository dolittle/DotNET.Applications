doLittle.namespace("doLittle.messaging", {
    messengerFactory: doLittle.Singleton(function () {
        this.create = function () {
            var messenger = doLittle.messaging.Messenger.create();
            return messenger;
        };

        this.global = function () {
            return doLittle.messaging.Messenger.global;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.messengerFactory = doLittle.messaging.messengerFactory;