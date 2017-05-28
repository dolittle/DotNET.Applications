doLittle.namespace("doLittle.interaction", {
    Trigger: doLittle.Type.extend(function () {
        var self = this;

        this.actions = [];

        this.addAction = function (action) {
            self.actions.push(action);
        };

        this.initialize = function (element) {
        };

        this.signal = function () {
            self.actions.forEach(function (action) {
                action.perform();
            });
        };
    })
});