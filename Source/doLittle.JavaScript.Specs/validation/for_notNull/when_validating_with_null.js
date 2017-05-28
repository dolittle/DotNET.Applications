describe("when validating with null", function () {
    var validator = doLittle.validation.notNull.create({ options: {} });
    var result = validator.validate(null);

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});