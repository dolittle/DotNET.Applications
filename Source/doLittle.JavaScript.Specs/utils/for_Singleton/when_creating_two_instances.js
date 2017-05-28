describe("when creating two instances", function () {
    var type = null;
    var firstInstance = null;
    var secondInstance = null;
    beforeEach(function () {
        doLittle.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };

        type = doLittle.Singleton(function () {
            this.something = "When creating two instances";
        });
        firstInstance = type.create();
        secondInstance = type.create();
    });

    afterEach(function () {
        doLittle.dependencyResolver = {};
    });

    it("should return correct instance for the first", function () {
        expect(firstInstance.something).toBe("When creating two instances");
    });

    it("should return correct instance for the second", function () {
        expect(secondInstance.something).toBe("When creating two instances");
    });

});