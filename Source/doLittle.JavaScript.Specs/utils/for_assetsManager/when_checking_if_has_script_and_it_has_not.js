describe("when checking if has script and it has not", function () {
    var result = false;
    beforeEach(function () {
        doLittle.assetsManager.scripts = ["something.js", "thestuff.js"];
        result = doLittle.assetsManager.hasScript("missing.js");
    });

    afterEach(function () {
        doLittle.assetsManager.scripts = [];
    });

    it("should return that it has it", function () {
        expect(result).toBe(false);
    });
});