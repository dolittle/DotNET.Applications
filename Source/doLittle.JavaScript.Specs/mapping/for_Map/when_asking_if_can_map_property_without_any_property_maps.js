describe("when asking if can map property without any property maps", function () {
    var propertyMapType = null;
    var propertyMapInstance = { something: 42 };
    var result = null;
    beforeEach(function () {
        propertyMapType = doLittle.mapping.PropertyMap;
        doLittle.mapping.PropertyMap = {
            create: sinon.stub().returns(propertyMapInstance)
        };

        var map = doLittle.mapping.Map.create();
        result = map.canMapProperty("SomeProperty");
        
    });


    afterEach(function () {
        doLittle.mapping.PropertyMap = propertyMapType;
    });

    it("should not be able to map property", function () {
        expect(result).toBe(false);
    });
});