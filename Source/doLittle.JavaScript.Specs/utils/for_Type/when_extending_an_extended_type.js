describe("when extending an extended type", function () {
    var initialType = function (anInitialType) { };
    var extendedType = null;
    var aSecondExtendedType = null;
    var anExtensionOfTheSecondExtendedType = null;

    beforeEach(function () {
        doLittle.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };
        initialType = doLittle.Type.extend(initialType);
        extendedType = initialType.extend(function (foo){});
        aSecondExtendedType = initialType.extend(function (bar) { });
        anExtensionOfTheSecondExtendedType = aSecondExtendedType.extend(function (func){});
    });

    afterEach(function () {
        doLittle.functionParser = {};
    });

    it("should return these types from the types that extend inital type", function () {
        var extenders = initialType.getExtenders();
        expect(extenders).toContain(extendedType);
        expect(extenders).toContain(aSecondExtendedType);
        expect(extenders).toContain(anExtensionOfTheSecondExtendedType);
        expect(extenders.length).toEqual(3);
    });
    
    it("should return these types from the types that extend doLittle type", function () {
        var extenders = doLittle.Type.getExtenders();
        expect(extenders).toContain(extendedType);
        expect(extenders).toContain(aSecondExtendedType);
        expect(extenders).toContain(anExtensionOfTheSecondExtendedType);
    });
    
    it("should return no types from the extended type that is not itself extended", function () {
        var extenders = extendedType.getExtenders();
        expect(extenders.length).toEqual(0);
    });
    
    it("should return only the extended type from the second type that is extended", function () {
        var extenders = aSecondExtendedType.getExtenders();
        expect(extenders.length).toEqual(1);
        expect(extenders).toContain(anExtensionOfTheSecondExtendedType);
    });
});
