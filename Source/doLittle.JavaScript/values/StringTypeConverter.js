doLittle.namespace("doLittle.values", {
    StringTypeConverter: doLittle.values.TypeConverter.extend(function () {
        this.supportedType = String;

        this.convertFrom = function (value) {
            return value.toString();
        };
    })
});