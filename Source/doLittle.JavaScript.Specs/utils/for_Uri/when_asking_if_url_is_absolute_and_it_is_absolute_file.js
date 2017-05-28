describe("when asking if url is absolute and it is absolute file", function () {

    var result = doLittle.Uri.isAbsolute("file://example.com");

    it("should be considered absolute", function () {
        expect(result).toBe(true);
    });


});