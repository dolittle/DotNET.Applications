describe("when resolving for supported type", sinon.test(function () {

    var resolver,
        resolvedTypes,
        propertyToResolve,
        namespace,
        getExtendersIn = function (namespace) { return [Test.extender]; };

    beforeEach(function () {
        doLittle.namespace("Test", { extender: doLittle.Type.extend(function () { }) });
        namespace = Test.extender._namespace;

        doLittle.commands.Command = { getExtendersIn: getExtendersIn };

        resolver = new doLittle.KnownArtifactTypesDependencyResolver();
        propertyToResolve = "commandTypes";
        namespace = {};
        
        resolvedTypes = resolver.resolve(namespace, propertyToResolve);
    });

    it("should resolve types", function () {
        expect(resolvedTypes).toBeDefined();
    })

    it("should have the resolved extender", function () {
        expect(resolvedTypes.extender).toBe(Test.extender);
    })

}));