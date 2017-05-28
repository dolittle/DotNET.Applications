describe("when asking if url is absolute and it is relative prefixed with slash", function () {

    var result = doLittle.Uri.isAbsolute("/example/url");

    it("should be considered relative", function () {
        expect(result).toBe(false);
    });


});