doLittle.namespace("doLittle.validation", {
    range: doLittle.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return doLittle.isUndefined(value) || doLittle.isNull(value);
        }

        function throwIfValueIsNotANumber(value, param) {
            if (!doLittle.isNumber(value)) {
                throw new doLittle.validation.NotANumber(param + " value " + value + " is not a number");
            }
        }


        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new doLittle.validation.OptionsNotDefined();
            }
            if (notSet(options.max)) {
                throw new doLittle.validation.MaxNotSpecified();
            }
            if (notSet(options.min)) {
                throw new doLittle.validation.MinNotSpecified();
            }
            throwIfValueIsNotANumber(options.min, "min");
            throwIfValueIsNotANumber(options.max, "max");
        }


        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueIsNotANumber(value, "value");
            return self.options.min <= value && value <= self.options.max;
        };

    })
});
