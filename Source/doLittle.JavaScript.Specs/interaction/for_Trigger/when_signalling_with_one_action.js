describe("when signalling with one action", function () {
    var action = {
        perform: sinon.stub()
    };

    var trigger = doLittle.interaction.Trigger.create();
    trigger.addAction(action);

    trigger.signal();

    it("should perform the action", function () {
        expect(action.perform.called).toBe(true);
    });
});