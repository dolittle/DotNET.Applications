describe("when resolving synchronous but resolver returns a promise", function() {
	var exception;
	var ns = {

	}

	var dependencyResolvers;

	beforeEach(function () {
	    dependencyResolvers = doLittle.dependencyResolvers;
	    doLittle.dependencyResolvers = {
	        getAll: function () {
	            return [{
	                canResolve: function () {
	                    return true;
	                },
	                resolve: function () {
	                    return doLittle.execution.Promise.create();
	                }
	            }
	            ];
	        }
	    };

	    try {
	        doLittle.dependencyResolver.resolve(ns, "something");
	    } catch (e) {
	        exception = e;
	    }
	});

	afterEach(function () {
	    doLittle.dependencyResolvers = dependencyResolvers;
	});
    

	it("should throw an exception", function() {
		expect(exception instanceof doLittle.AsynchronousDependenciesDetected).toBe(true);
	});
});