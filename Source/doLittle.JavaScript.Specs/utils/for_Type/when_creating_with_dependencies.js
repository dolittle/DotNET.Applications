describe("when creating with dependencies", function() {
	
	var somethingDependency;
	var system = {
		blah : "something"
	};

	var type = null; 

	var instance = null;

	beforeEach(function() {
		doLittle.dependencyResolver = {
			getDependenciesFor: function() {
				return ["something"];
			},
			resolve : function(name) {
				return system;
			}
		};

		type = doLittle.Type.extend(function(something) {
			somethingDependency = something;
		});		

		instance = type.create();
	});

	afterEach(function() {
		doLittle.functionParser = {};
	});

	it("should create with resolved dependencies", function() {
		expect(somethingDependency).toBe(system);
	});
});