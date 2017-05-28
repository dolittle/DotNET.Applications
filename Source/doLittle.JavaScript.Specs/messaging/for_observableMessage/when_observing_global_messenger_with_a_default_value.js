describe("when observing global messenger with a default value", function () {
    var observable = null;
    beforeEach(function () {
        doLittle.messaging = doLittle.messaging || {};
        doLittle.messaging.Messenger = {
            global: {
                publish: sinon.stub(),
                subscribeTo: function (message, callback) {
                }
            }
        }
        observable = ko.observableMessage("something");
    });


    it("should not publish a message", function () {
        expect(doLittle.messaging.Messenger.global.publish.called).toBe(false);
    });
});