describe("when creating", function() {
	var firstInstance = doLittle.Uri.create("http://www.vg.no");
	var secondInstance = doLittle.Uri.create("http://www.vg.no");
	
	it("should return an instance", function() {
		expect(firstInstance).not.toBeUndefined();
	});
	
	it("should return unique instances", function() {
		expect(firstInstance).not.toBe(secondInstance);
	});
});