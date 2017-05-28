doLittle.namespace("doLittle.validation", {
    regex: doLittle.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return doLittle.isUndefined(value) || doLittle.isNull(value);
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new doLittle.validation.OptionsNotDefined();
            }
            if (notSet(options.expression)) {
                throw new doLittle.validation.MissingExpression();
            }
            if (!doLittle.isString(options.expression)) {
                throw new doLittle.validation.NotAString("Expression " + options.expression + " is not a string.");
            }
        }

        function throwIfValueIsNotString(value) {
            if (!doLittle.isString(value)) {
                throw new doLittle.validation.NotAString("Value " + value + " is not a string.");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueIsNotString(value);
            return (value.match(self.options.expression) == null) ? false : true;
        };
    })
});


