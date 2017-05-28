describe("when value to be validated is undefined", function () {
    var validator = doLittle.validation.maxLength.create({ options: { length: 3 } })
    var result = validator.validate(undefined)

    it("should be valid", function () {
        expect(result).toBe(true);
    });
});