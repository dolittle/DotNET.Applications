describe("when asking query type if has read model and it is set", function () {
    
    var queryType = doLittle.read.Query.extend(function () {
        this._readModel = "Something";
    });

    var queryableFactory = {};
    var region = {};

    var instance = queryType.create({
        queryableFactory: queryableFactory,
        region: region
    });
    var result = instance.hasReadModel();

    it("should return true", function () {
        expect(result).toBe(true);
    });

});