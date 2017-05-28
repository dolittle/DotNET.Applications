describe("when creating", function () {

    var region = { some:"region"};

    var queryableFactory = {
    };

    var query = doLittle.read.Query.create({
        queryableFactory: queryableFactory,
        region: region
    });

    it("should set the region", function () {
        expect(query.region).toBe(region);
    });
});