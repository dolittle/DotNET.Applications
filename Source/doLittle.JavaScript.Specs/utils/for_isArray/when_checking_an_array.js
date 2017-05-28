describe("when checking an array", function() {
	var result = doLittle.isArray([]);
	it("should return true", function() {
		expect(result).toBe(true);
	});
});