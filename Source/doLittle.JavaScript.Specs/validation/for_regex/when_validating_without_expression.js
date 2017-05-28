describe("when validating without expression", function () {
    var exception = null;
    try {
        var validator = doLittle.validation.regex.create({ options: { } });
        validator.validate("1234");
    } catch (e) {
        exception = e;
    }

    it("should throw not a string exception", function () {
        expect(exception instanceof doLittle.validation.MissingExpression).toBe(true);
    });
});