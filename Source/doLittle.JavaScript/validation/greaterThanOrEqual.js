doLittle.namespace("doLittle.validation.ruleHandlers");
doLittle.validation.ruleHandlers.greaterThanOrEqual = {
    throwIfOptionsInvalid: function (options) {
        if (this.notSet(options)) {
            throw new doLittle.validation.OptionsNotDefined();
        }
        if (this.notSet(options.value)) {
            var exception = new doLittle.validation.OptionsValueNotSpecified();
            exception.message = exception.message + " 'value' is not set.";
            throw exception;
        }
        this.throwIfValueToCheckIsNotANumber(options.value);
    },

    throwIfValueToCheckIsNotANumber: function (value) {
        if (!doLittle.isNumber(value)) {
            throw new doLittle.validation.NotANumber("Value " + value + " is not a number");
        }
    },

    notSet: function (value) {
        return doLittle.isUndefined(value) || doLittle.isNull(value);
    },

    validate: function (value, options) {
        this.throwIfOptionsInvalid(options);
        if (this.notSet(value)) {
            return false;
        }
        this.throwIfValueToCheckIsNotANumber(value);
        return parseFloat(value) >= parseFloat(options.value);
    }
};
