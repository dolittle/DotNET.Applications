doLittle.namespace("doLittle", {
    functionParser: {
        parse: function(func) {
            var result = [];

            var match = func.toString().match(/function\s*\w*\((.*?)\)/);
            if (match !== null) {
                var functionArguments = match[1].split(/\s*,\s*/);
                functionArguments.forEach(function (item) {
                    if (item.trim().length > 0) {
                        result.push({
                            name: item
                        });
                    }
                });
            }

            return result;
        }
    }
});