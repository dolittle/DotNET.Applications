doLittle.namespace("doLittle.validation", {
    required: doLittle.validation.Rule.extend(function () {
        this.validate = function (value) {
            return !(doLittle.isUndefined(value) || doLittle.isNull(value) || value === "" || value === 0);
        };
    })
});

