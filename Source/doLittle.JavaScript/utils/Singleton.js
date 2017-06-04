doLittle.namespace("doLittle",{
    Singleton: function (typeDefinition) {
        return doLittle.Type.extend(typeDefinition).scopeTo(window);
    }
});