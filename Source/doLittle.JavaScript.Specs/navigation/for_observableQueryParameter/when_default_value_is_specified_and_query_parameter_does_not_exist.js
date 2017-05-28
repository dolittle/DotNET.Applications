describe("when default value is specified but query parameter does not exist", function () {
    doLittle.navigation.navigationManager = {
        getCurrentLocation: sinon.stub().returns(doLittle.Uri.create("http://www.somewhere.com"))
    };
    var observable = ko.observableQueryParameter("something", "hello");
    it("should hold the value from the query parameter", function () {
        expect(observable()).toBe("hello");
    });
});