doLittle.namespace("doLittle.views", {
    ViewBindingHandlerTemplateSource: doLittle.Type.extend(function (viewFactory) {
        var content = "";


        this.loadFor = function (element, view, region) {
            var promise = doLittle.execution.Promise.create();

            view.load(region).continueWith(function (loadedView) {
                var wrapper = document.createElement("div");
                wrapper.innerHTML = loadedView.content;
                

                content = wrapper.innerHTML;

                if (doLittle.isNullOrUndefined(loadedView.viewModelType)) {
                    promise.signal(loadedView);
                } else {
                    doLittle.views.Region.current = region;
                    view.viewModelType.ensure().continueWith(function () {
                        promise.signal(loadedView);
                    });
                }
            });

            return promise;
        };

        this.data = function (key, value) { };

        this.text = function (value) {
            return content;
        };
    })
});