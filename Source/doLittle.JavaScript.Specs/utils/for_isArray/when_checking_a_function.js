describe("when checking a function", function() {
	var result = doLittle.isArray(function() {});
	it("should return true", function() {
		expect(result).toBe(false);
	});
});