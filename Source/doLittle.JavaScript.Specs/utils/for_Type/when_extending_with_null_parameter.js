describe("when extending with null parameter", function() {
	var exception;
	
	try {
		doLittle.Type.extend(null);
	} catch(e) {
		exception = e;
	}
	
	it("should throw missing class definition exception", function() {
		expect(exception instanceof doLittle.MissingTypeDefinition).toBeTruthy();
	});
});