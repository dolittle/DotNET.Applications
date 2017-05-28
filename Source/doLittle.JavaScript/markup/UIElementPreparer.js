doLittle.namespace("doLittle.markup", {
    UIElementPreparer: doLittle.Singleton(function () {
        this.prepare = function (element, instance) {
            var result = instance.prepare(instance._type, element);
            if (result instanceof doLittle.execution.Promise) {
                result.continueWith(function () {

                    if (!doLittle.isNullOrUndefined(instance.template)) {
                        var UIManager = doLittle.views.UIManager.create();

                        UIManager.handle(instance.template);

                        ko.applyBindingsToNode(instance.template, {
                            "with": instance
                        });

                        element.parentElement.replaceChild(instance.template, element);
                    }
                });
            }
        };
    })
});