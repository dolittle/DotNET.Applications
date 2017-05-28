doLittle.namespace("doLittle.commands", {
    CommandSecurityContext: doLittle.Type.extend(function () {
        this.isAuthorized = ko.observable(false);
    })
});