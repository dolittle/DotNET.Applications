describe("when creating without message", function () {
    var rule = doLittle.validation.Rule.create({ options: { } });

    it("should set empty message in rule", function () {
        expect(rule.message).toBe("");
    });
});