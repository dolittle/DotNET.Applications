describe("when initialized without event name set", function () {

    var element = $("<button/>");
    var trigger = doLittle.interaction.EventTrigger.create();
    var exception = null;
    try {
        trigger.initialize(element);
    } catch (e) {
        exception = e;
    }

    it("should throw an exception", function () {
        expect(exception).not.toBeNull();
    });
});
