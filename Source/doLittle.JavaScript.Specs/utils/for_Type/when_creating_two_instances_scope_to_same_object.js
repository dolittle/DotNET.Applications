describe("when creating two instances scoped to same object", function () {
    var type = null;
    var firstInstance = null;
    var secondInstance = null;
    beforeEach(function () {
        doLittle.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };

        type = doLittle.Type.extend(function () {
        }).scopeTo("something");
        firstInstance = type.create();
        secondInstance = type.create();
    });

    afterEach(function () {
        doLittle.dependencyResolver = {};
    });

    it("should return same instances", function () {
        expect(firstInstance).toBe(secondInstance);
    });
});