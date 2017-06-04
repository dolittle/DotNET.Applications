doLittle.namespace("doLittle",{
    systemClock: doLittle.Singleton(function () {
        this.nowInMilliseconds = function () {
            return window.performance.now();
        };
    })
});