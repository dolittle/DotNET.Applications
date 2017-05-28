describe("when beginning to resolve", function () {
    var ns = {};
    var result;

    var configure = null;
    var dependencyResolvers;

    beforeEach(function () {
        configure = doLittle.configure;
        doLittle.configure = {
            ready: sinon.stub()
        };

        dependencyResolvers = doLittle.dependencyResolvers;

        doLittle.dependencyResolvers = {
            getAll: function () {
                return [{
                    canResolve: function () { return true; },
                    resolve: function () {

                        var promise = doLittle.execution.Promise.create();
                        return promise;
                    }
                }];
            }
        };
        result = doLittle.dependencyResolver.beginResolve(ns, "something");
    });

    afterEach(function () {
        doLittle.dependencyResolvers = dependencyResolvers;
        doLittle.configure = configure;
    });


	it("should return a promise", function() {
		expect(result instanceof doLittle.execution.Promise).toBe(true);
	});
});