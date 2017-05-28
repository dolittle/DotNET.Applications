describe("when validating with expression of wrong type", function () {
    var exception = null;
    try {
        var validator = doLittle.validation.regex.create({ options: { expression: {}} });
        validator.validate("1234");
    } catch (e) {
        exception = e;
    }

    it("should throw not a string exception", function () {
        expect(exception instanceof doLittle.validation.NotAString).toBe(true);
    });
});