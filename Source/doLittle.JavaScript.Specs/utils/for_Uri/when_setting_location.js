describe("when setting location", function() {
	var before = "http://www.vg.no";
	var after = "http://www.db.no";
	
	var uri = doLittle.Uri.create(before);
	uri.setLocation(after);
	
	it("should update location", function() {
		expect(uri.fullPath).toBe(after);
	});
});