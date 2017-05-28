describe("when beginning to resolve and resolvers can not resolve", function () {
    var resolver = {
        canResolve: sinon.stub().returns(false)
    };
    var exception;
    var readyCallback;

    var configure = null;
    var dependencyResolvers;

    beforeEach(function () {
        configure = doLittle.configure;
        doLittle.configure = {
            ready: function (callback) {
                readyCallback = callback;
            }
        };
        dependencyResolvers = doLittle.dependencyResolvers;
        doLittle.dependencyResolvers = {
            getAll: function () {
                return [resolver];
            }
        };

        try {
            doLittle.dependencyResolver.beginResolve("Something").onFail(function (e) {
                exception = e;
            });

            readyCallback();
        } catch (e) {
            exception = e;
        }
    });

    afterEach(function () {
        doLittle.dependencyResolvers = dependencyResolvers;
        doLittle.configure = configure;
    });


    it("should throw unresolved dependencies exception", function () {
        expect(exception instanceof doLittle.UnresolvedDependencies).toBeTruthy();
    });

});