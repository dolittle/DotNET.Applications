describe("when creating", function() {
	var instance = doLittle.Import.create();
	it("should return an instance", function() {
		expect(instance).not.toBeUndefined();
	});
});