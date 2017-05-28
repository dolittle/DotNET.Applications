describe("when asking for a string holding a number with leading zeros", function () {

    var result = doLittle.isNumber("0001");

    it("should return false", function () {
        expect(result).toBe(false);
    });
});