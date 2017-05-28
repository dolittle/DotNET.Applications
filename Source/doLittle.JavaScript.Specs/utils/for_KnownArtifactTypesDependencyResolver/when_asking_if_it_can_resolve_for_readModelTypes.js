describe("when asking if it can resolve for readModelTypes", sinon.test(function () {

    var resolver,
        canResolve,
        propertyToResolve,
        namespace;

    beforeEach(function () {
        doLittle.commands = sinon.stub().returns({ Command: function () { } });
        doLittle.read = sinon.stub().returns({
            ReadModelOf: function () { },
            Query: function () { }
        });

        resolver = new doLittle.KnownArtifactTypesDependencyResolver();
        canResolve = false;
        propertyToResolve = "readModelTypes";
        namespace = {};
        
        canResolve = resolver.canResolve(namespace, propertyToResolve);
    });

    it("should return true", function () {
        expect(canResolve).toBe(true);
    })

}));