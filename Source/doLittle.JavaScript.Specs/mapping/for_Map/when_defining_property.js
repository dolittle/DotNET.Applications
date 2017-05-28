describe("when defining property", function () {
    var propertyMapType = null;
    var propertyMapInstance = { something: 42 };
    var propertyMap = null;
    beforeEach(function () {
        propertyMapType = doLittle.mapping.PropertyMap;
        doLittle.mapping.PropertyMap = {
            create: sinon.stub().returns(propertyMapInstance)
        };

        var map = doLittle.mapping.Map.create();
        propertyMap = map.property("SomeProperty");
    });


    afterEach(function () {
        doLittle.mapping.PropertyMap = propertyMapType;
    });

    it("should create a new property map", function () {
        expect(doLittle.mapping.PropertyMap.create.calledWith({ sourceProperty: "SomeProperty"})).toBe(true)
    });

    it("should return the created property map", function () {
        expect(propertyMap).toBe(propertyMapInstance);
    });
    
});