doLittle.namespace("doLittle", {
    isNumber: function (number) {
        if (doLittle.isString(number)) {
            if (number.length > 1 && number[0] === '0') {
                return false;
            }
        }

        return !isNaN(parseFloat(number)) && isFinite(number);
    }
});