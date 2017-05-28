describe("when asking if it can resolve a type that is not registered", function () {
    doLittle.WellKnownTypesDependencyResolver.types.something = "Hello";

    var resolver = new doLittle.WellKnownTypesDependencyResolver();

    var result = resolver.canResolve(null, "somethingElse");

    it("should return false", function () {
        expect(result).toBe(false);
    });
});