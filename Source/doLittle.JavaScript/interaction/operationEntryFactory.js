doLittle.namespace("doLittle.interaction", {
    operationEntryFactory: doLittle.Singleton(function () {
        /// <summary>Represents a factory that can create OperationEntries</summary>

        this.create = function (operation, state) {
            /// <sumary>Create an instance of a OperationEntry</summary>
            /// <param name="context" type="doLittle.interaction.OperationContext">Context the operation was performed in</param>
            /// <param name="operation" type="doLittle.interaction.Operation">Operation that was performed</param>
            /// <param name="state" type="object">State that operation generated</param>
            /// <returns>An OperationEntry</returns>

            var instance = doLittle.interaction.OperationEntry.create({
                operation: operation,
                state: state
            });
            return instance;
        };
    })
});