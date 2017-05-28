describe("when getting all with one resolver registered", function () {

    var wellKnownTypesDependencyResolver = doLittle.WellKnownTypesDependencyResolver;
    var defaultDependencyResolver = doLittle.DefaultDependencyResolver;
    var knownArtifactTypesDependencyResolver = doLittle.KnownArtifactTypesDependencyResolver;
    var knownArtifactInstancesDependencyResolver = doLittle.KnownArtifactInstancesDependencyResolver;

    doLittle.WellKnownTypesDependencyResolver = function () {
        this.isWellKnown = true;
    };

    doLittle.DefaultDependencyResolver = function () {
        this.isDefault = true;
    };

    doLittle.KnownArtifactTypesDependencyResolver = function () { };
    doLittle.KnownArtifactInstancesDependencyResolver = function () { };

    doLittle.dependencyResolvers.myResolver = {
        identifier: "Hello"
    };

    var resolvers = doLittle.dependencyResolvers.getAll();

    doLittle.WellKnownTypesDependencyResolver = wellKnownTypesDependencyResolver;
    doLittle.DefaultDependencyResolver = defaultDependencyResolver;
    doLittle.KnownArtifactTypesDependencyResolver = knownArtifactTypesDependencyResolver;
    doLittle.KnownArtifactInstancesDependencyResolver = knownArtifactInstancesDependencyResolver;

    it("should not get any functions resolvers", function () {
        var hasFunction = false;

        for (var i = 0; i < resolvers.length; i++) {
            if (typeof resolvers[i] === "function") {
                hasFunction = true;
            }
        }

        expect(hasFunction).toBe(false);
    });

    it("should have the registered resolver at the end", function () {
        expect(resolvers[resolvers.length - 1].identifier).toBe("Hello");
    });

    it("should have the well known type resolver at the second place", function () {
        expect(resolvers[0].isWellKnown).toBe(true);
    });

    it("should have the default resolver at the second place", function () {
        expect(resolvers[1].isDefault).toBe(true);
    });
});