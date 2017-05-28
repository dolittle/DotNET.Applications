describe("when value to be validated is null", function () {
    var validator = doLittle.validation.email.create({ options: {} });
    var result = validator.validate(null);

    it("should be false", function () {
        expect(result).toBe(false);
    });
});
