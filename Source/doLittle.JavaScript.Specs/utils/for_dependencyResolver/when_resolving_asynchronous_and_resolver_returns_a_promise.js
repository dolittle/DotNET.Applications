describe("when resolving asynchronous and resolver returns a promise", function() {
	var ns = {};
	var innerPromise = doLittle.execution.Promise.create();
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
	                    return innerPromise;
	                }
	            }];
	        }
	    };

	    
	    doLittle.dependencyResolver
            .beginResolve(ns, "something")
            .continueWith(function (arg, nextPromise) {
                result = arg;

            });
	    innerPromise.signal("Hello");

	    readyCallback();
	});

	afterEach(function () {
	    doLittle.dependencyResolvers = dependencyResolvers;
	    doLittle.configure = configure;
	});


	it("should continue with inner promise parameter when inner promise continues", function() {
		expect(result).toBe("Hello");
	});
});