describe("when the value to be validated is not a string", function () {
    var exception = null;
    try {
        var validator = doLittle.validation.email.create({ options: {} });
        validator.validate({});
    } catch (e) {
        exception = e;
    }

    it("should throw an exception", function () {
        expect(exception instanceof doLittle.validation.NotAString).toBe(true);
    });
});