describe("when resolving asynchronous and resolver returns a system", function() {
    var ns = {};
    var result = null;

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
                return [{
                    canResolve: function () {
                        return true;
                    },
                    resolve: function () {
                        return "The result";
                    }
                }];
            }
        };
        
        doLittle.dependencyResolver
            .beginResolve(ns, "something")
            .continueWith(function (parameter, nextPromise) {
                result = parameter;
            });

        readyCallback();
    });

    afterEach(function () {
        doLittle.dependencyResolvers = dependencyResolvers;
        doLittle.configure = configure;
    });

	it("should continue with system from resolver as parameter", function() {
		expect(result).toBe("The result");
	});
});