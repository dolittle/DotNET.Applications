describe("when converting from with type without supporting converter for type", function () {

    var convertedValue = 42;
    var typeConverter = {
        supportedType: Date,
        convertFrom: sinon.stub().returns(convertedValue)
    };
    var typeConverterType = { create: function () { return typeConverter; } }
    var typeConverterBefore = null;
    var converted = null;
    var input = "42";
    beforeEach(function () {
       
        typeConverterBefore = doLittle.values.TypeConverter;
        doLittle.values.TypeConverter = {
            getExtenders: function () {
                return [typeConverterType]
            }
        };

        var typeConverters = doLittle.values.typeConverters.createWithoutScope();
        converted = typeConverters.convertFrom(input, Number);
    });

    afterEach(function () {
        doLittle.views.TypeConverter = typeConverterBefore;
    });

    it("should return the same value as input", function () {
        expect(converted).toBe(input);
    });
});