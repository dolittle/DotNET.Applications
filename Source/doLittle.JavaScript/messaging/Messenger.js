doLittle.namespace("doLittle.messaging", {
    Messenger: doLittle.Type.extend(function () {
        var subscribers = [];

        this.publish = function (topic, message) {
            if (subscribers.hasOwnProperty(topic)) {
                subscribers[topic].subscribers.forEach(function (item) {
                    item(message);
                });
            }
        };

        this.subscribeTo = function (topic, subscriber) {
            var subscribersByTopic;

            if (subscribers.hasOwnProperty(topic)) {
                subscribersByTopic = subscribers[topic];
            } else {
                subscribersByTopic = { subscribers: [] };
                subscribers[topic] = subscribersByTopic;
            }

            subscribersByTopic.subscribers.push(subscriber);
        };

        return {
            publish: this.publish,
            subscribeTo: this.subscribeTo
        };
    })
});
doLittle.messaging.Messenger.global = doLittle.messaging.Messenger.create();
doLittle.WellKnownTypesDependencyResolver.types.globalMessenger = doLittle.messaging.Messenger.global;
