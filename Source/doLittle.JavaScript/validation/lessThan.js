doLittle.namespace("doLittle.validation", {
    lessThan: doLittle.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return doLittle.isUndefined(value) || doLittle.isNull(value);
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new doLittle.validation.OptionsNotDefined();
            }
            if (notSet(options.value)) {
                var exception = new doLittle.validation.OptionsValueNotSpecified();
                exception.message = exception.message + " 'value' is not set.";
                throw exception;
            }
        }

        function throwIsValueToCheckIsNotANumber(value) {
            if (!doLittle.isNumber(value)) {
                throw new doLittle.validation.NotANumber("Value " + value + " is not a number");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIsValueToCheckIsNotANumber(value);
            return parseFloat(value) < parseFloat(self.options.value);
        };
    })
});
