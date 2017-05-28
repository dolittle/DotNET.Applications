doLittle.namespace("doLittle.types", {
    PropertyInfo: doLittle.Type.extend(function (name, type) {
        this.name = name;
        this.type = type;
    })
});