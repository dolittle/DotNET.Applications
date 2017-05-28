doLittle.namespace("doLittle.values", {
    typeExtender: doLittle.Singleton(function () {
        this.extend = function (target, typeAsString) {
            target._typeAsString = typeAsString;
        };
    })
});
ko.extenders.type = doLittle.values.typeExtender.create().extend;
