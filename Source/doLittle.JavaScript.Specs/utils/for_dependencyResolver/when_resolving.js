describe("when resolving", function () {
    var firstResolver = {
        canResolve: sinon.stub().returns(false)
    };
    var secondResolver = {
        canResolve: sinon.stub().returns(false)
    };

    var dependencyResolvers;

    beforeEach(function () {
        dependencyResolvers = doLittle.dependencyResolvers;

        doLittle.dependencyResolvers = {
            getAll: function () {
                return [firstResolver, secondResolver];
            }
        };
        try {
            doLittle.dependencyResolver.resolve("Something");
        } catch (e) {
        }
    });

    afterEach(function () {
        doLittle.dependencyResolvers = dependencyResolvers;
    });

    it("should ask first resolver", function () {
        expect(firstResolver.canResolve.called).toBe(true);
    });
    it("should ask second resolver", function () {
        expect(secondResolver.canResolve.called).toBe(true);
    });
});