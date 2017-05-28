describe("when defining without name", function() {
	var exception;
	
	try {
		doLittle.Exception.define();
	} catch( e ) {
		exception = e;
	}
	
	it("should throw missing name exception", function() {
		expect(exception instanceof doLittle.MissingName).toBeTruthy();
	});
});