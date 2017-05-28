describe("when validating with undefined", function () {
    var validator = doLittle.validation.notNull.create({ options: {} });
    var result = validator.validate();

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});