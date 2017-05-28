describe("when creating two instances with default scope", function () {
    var type = null;
    var firstInstance = null;
    var secondInstance = null;
    beforeEach(function () {
        doLittle.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };

        type = doLittle.Type.extend(function () {
        });
        firstInstance = type.create();
        secondInstance = type.create();
    });

    afterEach(function () {
        doLittle.dependencyResolver = {};
    });

    it("should return different instances", function () {
        expect(firstInstance).not.toBe(secondInstance);
    });
});