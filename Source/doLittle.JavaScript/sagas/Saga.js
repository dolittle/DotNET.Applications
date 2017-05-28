doLittle.namespace("doLittle.sagas");
doLittle.sagas.Saga = (function () {
    function Saga() {
        var self = this;

        this.executeCommands = function (commands, options) {

            var canExecuteSaga = true;

            commands.forEach(function (command) {
                if (command.onBeforeExecute() === false) {
                    canExecuteSaga = false;
                    return false;
                }
            });

            if (canExecuteSaga === false) {
                return;
            }
            doLittle.commands.commandCoordinator.handleForSaga(self, commands, options);
        };
    }

    return {
        create: function (configuration) {
            var saga = new Saga();
            doLittle.extend(saga, configuration);
            return saga;
        }
    };
})();
