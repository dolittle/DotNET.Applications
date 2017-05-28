describe("when validating and it should not match", function () {
    var validator = doLittle.validation.regex.create({ options: { expression: "[abc]" } });
    var result = validator.validate("1234")

    it("should not be valid", function () {
        expect(result).toBe(false);
    });
});