describe("when asking if string is object", function () {

    var result = doLittle.isObject({});

    it("should be considered an object", function () {
        expect(result).toBe(true);
    });
    
});