describe("when extending two different types", function () {
    var firstTypeDefinition = doLittle.Type.extend(function () { });
    var secondTypeDefinition = doLittle.Type.extend(function () { });

    it("should have different type identifiers", function () {
        expect(firstTypeDefinition._typeId).not.toBe(secondTypeDefinition._typeId);
    });
});