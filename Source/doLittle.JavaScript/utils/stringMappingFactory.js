doLittle.namespace("doLittle",{
    stringMappingFactory: doLittle.Singleton(function () {

        this.create = function (format, mappedFormat) {
            var mapping = doLittle.StringMapping.create({
                format: format,
                mappedFormat: mappedFormat
            });
            return mapping;
        };
    })
});