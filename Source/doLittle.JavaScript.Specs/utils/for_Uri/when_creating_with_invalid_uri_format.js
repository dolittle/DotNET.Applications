describe("when creating with invalid uri format", function() {
	var exception;
	try {
		doLittle.Uri.create("wrong stuff");
	} catch( e ) {
		exception = e;
	}
	
	it("should throw invalid uri format", function() {
		expect(exception instanceof doLittle.InvalidUriFormat).toBeTruthy();
	});
});