doLittle.namespace("doLittle",{
    areEqual: function (source, target) {
        function isReservedMemberName(member) {
            return member.indexOf("_") >= 0 || member === "model" || member === "commons" || member === "targetViewModel" || member === "region";
        }

        if (ko.isObservable(source)) {
            source = source();
        }
        if (ko.isObservable(target)) {
            target = target();
        }

        if (doLittle.isNullOrUndefined(source) && doLittle.isNullOrUndefined(target)) {
            return true;
        }

        if (doLittle.isNullOrUndefined(source)) {
            return false;
        }
        if (doLittle.isNullOrUndefined(target)) {
            return false;
        }

        if (doLittle.isArray(source) && doLittle.isArray(target)) {
            if (source.length !== target.length) {
                return false;
            }

            for (var index = 0; index < source.length; index++) {
                if (doLittle.areEqual(source[index], target[index]) === false) {
                    return false;
                }
            }
        } else {
            for (var member in source) {
                if (isReservedMemberName(member)) {
                    continue;
                }
                if (target.hasOwnProperty(member)) {
                    var sourceValue = source[member];
                    var targetValue = target[member];

                    if (doLittle.isObject(sourceValue) ||
                        doLittle.isArray(sourceValue) ||
                        ko.isObservable(sourceValue)) {

                        if (!doLittle.areEqual(sourceValue, targetValue)) {
                            return false;
                        }
                    } else {
                        if (sourceValue !== targetValue) {
                            return false;
                        }
                    }
                } else {
                    return false;
                }
            }
        }

        return true;
    }
});