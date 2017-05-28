describe("when extending", function () {
    var typeDefinition = function (something) { };
    var result = null;

    beforeEach(function () {
        doLittle.dependencyResolver = {
            getDependenciesFor: sinon.stub()
        };
        result = doLittle.Type.extend(typeDefinition);
    });

    afterEach(function () {
        doLittle.functionParser = {};
    });

    it("should get the dependencies for the function", function () {
        expect(doLittle.dependencyResolver.getDependenciesFor.called).toBe(true);
    });

    it("should return the type definition", function () {
        expect(result).toBe(typeDefinition);
    });

    it("should a create function", function () {
        expect(typeof result.create).toBe("function");
    });

    it("should add a type id", function () {
        expect(typeDefinition._typeId).toBeDefined();
    });

    it("should add this type to the list of types that extend doLittle Type", function () {
        var extenders = doLittle.Type.getExtenders();
        expect(extenders).toContain(typeDefinition);
    });
});