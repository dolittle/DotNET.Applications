doLittle.namespace("doLittle", {
    dispatcher: doLittle.Singleton(function () {
        this.schedule = function (milliseconds, callback) {
            setTimeout(callback, milliseconds);
        };
    })
});