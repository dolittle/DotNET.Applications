describe("when creating without location", function() {
	var exception;
	try {
		doLittle.Uri.create();
	} catch( e ) {
		exception = e;
	}
	
	it("should throw location not specified", function() {
		expect(exception instanceof doLittle.LocationNotSpecified).toBeTruthy();
	});
});