describe("when resolving and resolvers can not resolve", function () {
    var resolver = {
        canResolve: sinon.stub().returns(false)
    };
    var exception;

    var dependencyResolvers;
    beforeEach(function () {
        dependencyResolvers = doLittle.dependencyResolvers;

        doLittle.dependencyResolvers = {
            getAll: function () {
                return [resolver];
            }
        };
        try {
            doLittle.dependencyResolver.resolve("Something");
        } catch (e) {
            exception = e;
        }
    });

    afterEach(function () {
        doLittle.dependencyResolvers = dependencyResolvers;
    });

    it("should throw unresolved dependencies exception", function () {
        expect(exception instanceof doLittle.UnresolvedDependencies).toBeTruthy();
    });

});