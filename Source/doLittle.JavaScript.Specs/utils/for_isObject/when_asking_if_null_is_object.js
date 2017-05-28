describe("when asking if null is object", function () {

    var result = doLittle.isObject(null);

    it("should not be considered an object", function () {
        expect(result).toBe(false);
    });
    
});