describe("when asking query type if has read model and it is not set", function () {
    
    var queryType = doLittle.read.Query.extend(function () {
    });

    var queryableFactory = {};
    var region = {};

    var instance = queryType.create({
        queryableFactory: queryableFactory,
        region: region
    });
    var result = instance.hasReadModel();

    it("should return false", function () {
        expect(result).toBe(false);
    });

});