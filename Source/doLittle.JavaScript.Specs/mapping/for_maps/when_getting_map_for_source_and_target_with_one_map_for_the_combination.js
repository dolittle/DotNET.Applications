describe("when getting map for source and target with one map for the combination", function () {

    var mapType = null;
    var map = null;
    var customMap = null;

    beforeEach(function () {
        mapType = doLittle.mapping.Map;
        doLittle.mapping.Map = doLittle.Type.extend(function () { });
        var sourceType = doLittle.Type.extend(function () { });
        var targetType = doLittle.Type.extend(function () { });

        customMap = doLittle.mapping.Map.extend(function () {
            this.sourceType = sourceType;
            this.targetType = targetType;
        });

        var maps = doLittle.mapping.maps.createWithoutScope();

        map = maps.getMapFor(sourceType, targetType);
    });
    
    afterEach(function () {
        doLittle.mapping.Map = mapType;
    });

    it("should get the", function () {
        expect(map._type).toBe(customMap);
    });
});
