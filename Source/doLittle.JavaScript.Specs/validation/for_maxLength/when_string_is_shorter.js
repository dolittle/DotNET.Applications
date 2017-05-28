describe("when string is shorter or same", function () {
    var validator = doLittle.validation.maxLength.create({ options: { length: 5 } });
    var result = validator.validate("1234");

    it("should be valid", function () {
        expect(result).toBe(true);
    });
});