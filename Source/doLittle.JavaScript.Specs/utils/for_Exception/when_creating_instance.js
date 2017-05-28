describe("when creating instance", function() {
	var exceptionName = "SomeException";
	
	doLittle.Exception.define(exceptionName);

	var instance = new SomeException();
	
	it("should have name set to exception name", function() {
		expect(instance.name).toBe(exceptionName);
	});
});