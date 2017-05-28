describe("when changing extension", function () {

    var newFile = doLittle.Path.changeExtension("Something/cool/file.html", "js");

    it("should change the extension", function () {
        expect(newFile).toBe("Something/cool/file.js");
    });
});