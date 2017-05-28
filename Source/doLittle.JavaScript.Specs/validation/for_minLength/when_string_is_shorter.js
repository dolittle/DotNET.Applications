describe("when string is shorter or same", function () {
    var validator = doLittle.validation.minLength.create({ options: { length: 5 } });
    var result = validator.validate("1234");

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});