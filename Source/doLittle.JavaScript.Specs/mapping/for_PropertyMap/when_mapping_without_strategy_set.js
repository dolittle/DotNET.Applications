describe("when mapping without strategy set", function () {
    var propertyMap = doLittle.mapping.PropertyMap.create({
        sourceProperty: "Source",
        typeConverters: {}
    });

    var missingPropertyStrategy;
    var exception = null;

    beforeEach(function () {
        missingPropertyStrategy = doLittle.mapping.MissingPropertyStrategy;
        doLittle.mapping.MissingPropertyStrategy = doLittle.Type.extend(function () { });

        
        try {
            propertyMap.map({}, {});
        } catch (e) {

            exception = e;
        }
    });

    afterEach(function () {
        doLittle.mapping.MissingPropertyStrategy = missingPropertyStrategy;
    });

    it("should throw missing property strategy", function () {
        expect(exception._type).toBe(doLittle.mapping.MissingPropertyStrategy);
    });
});