describe("when validating and it should match", function () {
    var validator = doLittle.validation.regex.create({ options: { expression: "[abc]" } });
    var result = validator.validate("abcd")

    it("should be valid", function () {
        expect(result).toBe(true);
    });
});