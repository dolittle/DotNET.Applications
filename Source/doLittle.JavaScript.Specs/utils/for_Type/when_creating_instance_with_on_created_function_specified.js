describe("when creating instance with on created function specified", function () {
    var type = null;
    var onCreatedStub = sinon.stub();

    var instance = null;
    beforeEach(function () {
        doLittle.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };

        type = doLittle.Type.extend(function () {
            this.onCreated = onCreatedStub;
        });

        instance = type.create();
    });

    afterEach(function () {
        doLittle.dependencyResolver = {};
    });

    it("should call the on created function", function () {
        expect(onCreatedStub.called).toBe(true);
    });
});