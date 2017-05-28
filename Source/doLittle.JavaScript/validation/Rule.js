doLittle.namespace("doLittle.validation", {
    Rule: doLittle.Type.extend(function (options) {
        options = options || {};
        this.message = options.message || "";
        this.options = {};
        doLittle.extend(this.options, options);

        this.validate = function (value) {
            return true;
        };
    })
});