doLittle.namespace("doLittle.values", {
    Formatter: doLittle.Type.extend(function () {
        this.supportedType = null;

        this.format = function (value, format) {
            return value;
        };
    })
});