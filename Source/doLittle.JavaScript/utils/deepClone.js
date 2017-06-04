doLittle.namespace("doLittle",{
    deepClone: function (source, target) {
        function isReservedMemberName(member) {
            return member.indexOf("_") >= 0 || member === "model" || member === "commons" || member === "targetViewModel" || member === "region";
        }

        if (ko.isObservable(source)) {
            source = source();
        }

        if (target == null) {
            if (doLittle.isArray(source)) {
                target = [];
            } else {
                target = {};
            }
        }

        var sourceValue;
        if (doLittle.isArray(source)) {
            for (var index = 0; index < source.length; index++) {
                sourceValue = source[index];
                var clonedValue = doLittle.deepClone(sourceValue);
                target.push(clonedValue);
            }
        } else {
            for (var member in source) {
                if (isReservedMemberName(member)) {
                    continue;
                }

                sourceValue = source[member];

                if (ko.isObservable(sourceValue)) {
                    sourceValue = sourceValue();
                }

                if (doLittle.isFunction(sourceValue)) {
                    continue;
                }

                var targetValue = null;
                if (doLittle.isObject(sourceValue)) {
                    targetValue = {};
                } else if (doLittle.isArray(sourceValue)) {
                    targetValue = [];
                } else {
                    target[member] = sourceValue;
                }

                if (targetValue != null) {
                    target[member] = targetValue;
                    doLittle.deepClone(sourceValue, targetValue);
                }
            }
        }

        return target;
    }
});
