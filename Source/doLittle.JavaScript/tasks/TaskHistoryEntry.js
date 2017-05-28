doLittle.namespace("doLittle.tasks", {
    TaskHistoryEntry: doLittle.Type.extend(function () {
        var self = this;

        this.type = "";
        this.content = "";

        this.begin = ko.observable();
        this.end = ko.observable();
        this.total = ko.computed(function () {
            if (!doLittle.isNullOrUndefined(self.end()) &&
                !doLittle.isNullOrUndefined(self.begin())) {
                return self.end() - self.begin();
            }
            return 0;
        });
        this.result = ko.observable();
        this.error = ko.observable();

        this.isFinished = ko.computed(function () {
            return !doLittle.isNullOrUndefined(self.end());
        });
        this.hasFailed = ko.computed(function () {
            return !doLittle.isNullOrUndefined(self.error());
        });

        this.isSuccess = ko.computed(function () {
            return self.isFinished() && !self.hasFailed();
        });
    })
});