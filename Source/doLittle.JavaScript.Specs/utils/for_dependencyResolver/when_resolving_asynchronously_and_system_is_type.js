describe("when resolving asynchronously and system is type", function () {
    var type = doLittle.Type.extend(function (dependency) {
        this.something = "Hello";
        this.dependency = dependency;
    });
    var ns = {
        something: type
    };
    var readyCallback;
    var configure = null;
    var result = null;

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
                    canResolve: function (namespace, name) {
                        return true;
                    },
                    resolve: function (namespace, name) {
                        var promise = doLittle.execution.Promise.create();
                        var system = type;
                        if (name === "dependency") {
                            system = name;
                        }
                        promise.signal(system);
                        return promise;
                    }
                }];
            }
        };
        
        doLittle.dependencyResolver
				.beginResolve(ns, "something")
				.continueWith(function (param, next) {
				    result = param;
				});

        readyCallback();
    });

    afterEach(function () {
        doLittle.dependencyResolvers = dependencyResolvers;
        doLittle.configure = configure;
    });


    // TODO: Fix this, or actually kill it off.. 
    /*
    it("should create instance of type and resolve dependencies", function () {
        expect(result.dependency).toBe("dependency");
    });*/
});