describe("when not providing a comparison value", function () {
    var exception = null;
    try {
        var validator = doLittle.validation.lessThan.create({ options: {} });
        validator.validate("1234");
    } catch (e) {
        exception = e;
        
    }
    it("should throw an exception", function () {
        expect(exception instanceof doLittle.validation.OptionsNotDefined).toBe(true);
    });
});