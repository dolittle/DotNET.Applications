describe("when getting value from non observable for view twice without value having changed", function () {
    var typeConverters = {
        convertTo: sinon.stub()
    };
    var stringFormatter = {
        hasFormat: sinon.stub().returns(false)
    };

    var pipeline = doLittle.values.valuePipeline.createWithoutScope({
        typeConverters: typeConverters,
        stringFormatter: stringFormatter
    });

    var element = {};
    var value = 5;

    var result = pipeline.getValueForView(element, value);
    var secondResult = pipeline.getValueForView(element, value);

    it("should not try to convert it", function () {
        expect(typeConverters.convertTo.called).toBe(false);
    });

    it("should return the same value second time", function () {
        expect(secondResult).toBe(result);
    });
});
