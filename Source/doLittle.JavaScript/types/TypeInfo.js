doLittle.namespace("doLittle.types", {
    TypeInfo: doLittle.Type.extend(function () {
        this.properties = [];
    })
});
doLittle.types.TypeInfo.createFrom = function (instance) {
    var typeInfo = doLittle.types.TypeInfo.create();
    var propertyInfo;
    for (var property in instance) {
        var value = instance[property];
        if (!doLittle.isNullOrUndefined(value)) {

            var type = value.constructor;

            if (!doLittle.isNullOrUndefined(instance[property]._type)) {
                type = instance[property]._type;
            }

            propertyInfo = doLittle.types.PropertyInfo.create({
                name: property,
                type: type
            });
        }
        typeInfo.properties.push(propertyInfo);
    }
    return typeInfo;
};
