describe("when validating with undefined", function () {
    var validator = doLittle.validation.required.create({ options: {} });
    var result = validator.validate();

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});