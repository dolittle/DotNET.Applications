describe("when resolving for supported type that has multiple same named extenders", sinon.test(function () {

    var resolver,
        resolvedTypes,
        propertyToResolve,
        namespace,
        getExtendersIn = function (namespace) { return [Test.extender, Test.Deeper.extender, Test.Deeper.EvenDeeper.extender]; };

    beforeEach(function () {
        doLittle.namespace("Test", { extender: doLittle.Type.extend(function () { }) });
        doLittle.namespace("Test.Deeper", { extender: doLittle.Type.extend(function () { }) });
        doLittle.namespace("Test.Deeper.EvenDeeper", { extender: doLittle.Type.extend(function () { }) });
        namespace = Test.Deeper.EvenDeeper.extender._namespace;

        doLittle.commands.Command = { getExtendersIn: getExtendersIn };

        resolver = new doLittle.KnownArtifactTypesDependencyResolver();
        propertyToResolve = "commandTypes";
        namespace = {};
        
        resolvedTypes = resolver.resolve(namespace, propertyToResolve);
    });

    it("should resolve types", function () {
        expect(resolvedTypes).toBeDefined();
    })

    it("should have the most specific type", function () {
        expect(resolvedTypes.extender).toBe(Test.Deeper.EvenDeeper.extender);
    })

}));