doLittle.namespace("doLittle",{
    isNullOrUndefined: function (value) {
        return doLittle.isUndefined(value) || doLittle.isNull(value);
    }
});