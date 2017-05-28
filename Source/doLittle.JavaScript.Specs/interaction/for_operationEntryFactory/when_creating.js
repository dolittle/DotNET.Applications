describe("when creating", function () {
    var existingOperationEntryType = null;

    var operationEntry = {
        something: 42
    };

    var operation = {
        someOperation: 44
    };

    var operationState = {
        someState: 45
    };

    var instance = null;

    var operationEntryType = null;

    beforeEach(function () {
        existingOperationEntryType = doLittle.interaction.OperationEntry;

        operationEntryType = {
            create: sinon.mock().withArgs({
                operation: operation,
                state: operationState
            }).returns(operationEntry)
        };

        doLittle.interaction.OperationEntry = operationEntryType;

        var factory = doLittle.interaction.operationEntryFactory.create();
        instance = factory.create(operation, operationState);
    });

    afterEach(function () {
        doLittle.interaction.OperationEntry = existingOperationEntryType;
    });
    
    it("should create a new instance", function () {
        expect(operationEntryType.create.called).toBe(true);
    });

    it("should return the created instance", function () {
        expect(instance).toBe(operationEntry);
    });
});