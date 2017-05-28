describe("when resolving and system has multiple levels of inheritance but top level is a type", function () {

    var topLevelType = doLittle.Type.extend(function() {
    });

    var secondLevelType = topLevelType.extend(function() {
    });

    var dependencyType = secondLevelType.extend(function () {
        this.hello = "world";
    });

	var type = doLittle.Type.extend(function(dependency) {
		this.something = "Hello";
		this.dependency = dependency;
	});

	var result = null;
	var ns = {};

	var dependencyResolvers;

	beforeEach(function () {
	    dependencyResolvers = doLittle.dependencyResolvers;
	    doLittle.dependencyResolvers = {
	        getAll: function () {
	            return [{
	                canResolve: function () {
	                    return true;
	                },
	                resolve: function (namespace, name) {
	                    if (name == "dependency") {
	                        return dependencyType;
	                    }
	                    return type;
	                }
	            }];
	        }
	    };

	    result = doLittle.dependencyResolver.resolve(ns, "something");
	});

	afterEach(function () {
	    doLittle.dependencyResolvers = dependencyResolvers;
	});
	

	it("should create instance of type and resolve dependencies", function() {
	    expect(result.dependency instanceof dependencyType).toBe(true);
	});
});