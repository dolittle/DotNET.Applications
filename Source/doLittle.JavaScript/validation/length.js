doLittle.namespace("doLittle.validation", {
    length: doLittle.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return doLittle.isUndefined(value) || doLittle.isNull(value);
        }

        function throwIfValueIsNotANumber(value) {
            if (!doLittle.isNumber(value)) {
                throw new doLittle.validation.NotANumber("Value " + value + " is not a number");
            }
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new doLittle.validation.OptionsNotDefined();
            }
            if (notSet(options.max)) {
                throw new doLittle.validation.MaxLengthNotSpecified();
            }
            if (notSet(options.min)) {
                throw new doLittle.validation.MinLengthNotSpecified();
            }
            throwIfValueIsNotANumber(options.min);
            throwIfValueIsNotANumber(options.max);
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                value = "";
            }
            if (!doLittle.isString(value)) {
                value = value.toString();
            }
            return self.options.min <= value.length && value.length <= self.options.max;
        };
    })
});