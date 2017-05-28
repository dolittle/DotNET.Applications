describe("when adding single requirement explicitly", function () {
    doLittle.dependencyResolver.getDependenciesFor = function () {
        return [];
    };

    var type = doLittle.Type.extend(function () {
    }).requires("something");


    it("should only have one dependency", function () {
        expect(type._dependencies.length).toBe(1);
    });

    it("should add the requirement as a dependency", function () {
        expect(type._dependencies[0]).toBe("something");
    });
});