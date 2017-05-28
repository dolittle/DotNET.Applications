doLittle.namespace("doLittle.validation", {
    notNull: doLittle.validation.Rule.extend(function () {
        this.validate = function (value) {
            return !(doLittle.isUndefined(value) || doLittle.isNull(value));
        };
    })
});

