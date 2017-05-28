doLittle.namespace("doLittle.values", {
    DateFormatter: doLittle.values.Formatter.extend(function () {
        this.supportedType = Date;

        this.format = function (value, format) {
            return value.format(format);
        };
    })
});