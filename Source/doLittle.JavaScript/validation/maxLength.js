doLittle.namespace("doLittle.validation", {
    maxLength: doLittle.validation.Rule.extend(function() {
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
            if (notSet(options.length)) {
                throw new doLittle.validation.MaxNotSpecified();
            }
            throwIfValueIsNotANumber(options.length);
        }


        function throwIfValueIsNotAString(string) {
            if (!doLittle.isString(string)) {
                throw new doLittle.validation.NotAString("Value " + string + " is not a string");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                value = "";
            }
            throwIfValueIsNotAString(value);
            return value.length <= self.options.length;
        };
    })
});

