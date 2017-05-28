describe("when asking if map exists for source and target with one map for the combination", function () {

    var mapType = null;
    var result = null;

    beforeEach(function () {
        mapType = doLittle.mapping.Map;
        doLittle.mapping.Map = doLittle.Type.extend(function () { });
        var sourceType = doLittle.Type.extend(function () { });
        var targetType = doLittle.Type.extend(function () { });

        var customMap = doLittle.mapping.Map.extend(function () {
            this.sourceType = sourceType;
            this.targetType = targetType;
        });

        var maps = doLittle.mapping.maps.createWithoutScope();

        result = maps.hasMapFor(sourceType, targetType);
    });
    
    afterEach(function () {
        doLittle.mapping.Map = mapType;
    });

    it("should have map", function () {
        expect(result).toBe(true);
    });
});
