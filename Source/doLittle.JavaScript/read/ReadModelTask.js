doLittle.namespace("doLittle.read", {
    ReadModelTask: doLittle.tasks.LoadTask.extend(function (readModelOf, propertyFilters, taskFactory) {
        var url = "/doLittle/ReadModel/InstanceMatching?_rm=" + readModelOf._generatedFrom;
        var payload = {
            descriptor: {
                readModel: readModelOf._name,
                generatedFrom: readModelOf._generatedFrom,
                propertyFilters: propertyFilters
            }
        };

        this.readModel = readModelOf._generatedFrom;

        var innerTask = taskFactory.createHttpPost(url, payload);

        this.execute = function () {
            var promise = innerTask.execute();
            return promise;
        };
    })
});