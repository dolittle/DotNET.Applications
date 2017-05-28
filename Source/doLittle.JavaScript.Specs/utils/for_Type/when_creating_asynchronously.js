describe("when creating asynchronously", function() {
	var type = doLittle.Type.extend(function() {
	});

	var result = type.beginCreate();

	it("should return a promise", function() {
		expect(result instanceof doLittle.execution.Promise).toBe(true);
	});
});