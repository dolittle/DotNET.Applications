doLittle.namespace("doLittle.values", {
    DefaultValueConsumer: doLittle.values.ValueConsumer.extend(function (target, property) {
        this.consume = function(value) {
            target[property] = value;
        };
    })
});