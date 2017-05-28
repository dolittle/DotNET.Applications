describe("when checking an object", function() {
	var result = doLittle.isArray({});
	it("should return true", function() {
		expect(result).toBe(false);
	});
});