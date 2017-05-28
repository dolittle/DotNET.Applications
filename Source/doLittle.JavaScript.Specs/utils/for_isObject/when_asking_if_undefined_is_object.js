describe("when asking if undefined is object", function () {

    var result = doLittle.isObject(undefined);

    it("should not be considered an object", function () {
        expect(result).toBe(false);
    });
});