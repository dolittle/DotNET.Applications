doLittle.namespace("doLittle.views", {
    viewBindingHandler: doLittle.Type.extend(function (ViewBindingHandlerTemplateEngine, UIManager, viewFactory, viewManager, viewModelManager, documentService, regionManager, pathResolvers) {
        function makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor, bindingContext) {
            return function () {
                var viewUri = ko.utils.unwrapObservable(valueAccessor());

                if (element.viewUri !== viewUri) {
                    element.children.forEach(ko.removeNode);

                    element.viewModel = null;
                    element.view = null;
                    element.templateSource = null;
                    element.innerHTML = "";
                }

                element.viewUri = viewUri;

                var viewModel = ko.observable(element.viewModel);
                var viewModelParameters = allBindingsAccessor().viewModelParameters || {};

                var templateEngine = null;
                var view = null;
                var region = null;

                if (doLittle.isNullOrUndefined(viewUri) || viewUri === "") {
                    templateEngine = new ko.nativeTemplateEngine();
                } else {
                    templateEngine = ViewBindingHandlerTemplateEngine;
                    var actualPath = pathResolvers.resolve(element, viewUri);
                    view = viewFactory.createFrom(actualPath);
                    view.element = element;
                    region = regionManager.getFor(view);
                }

                return {
                    if: true,
                    data: viewModel,
                    element: element,
                    templateEngine: templateEngine,
                    viewUri: viewUri,
                    viewModelParameters: viewModelParameters,
                    view: view,
                    region: region
                };
            };
        }

        this.init = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            return ko.bindingHandlers.template.init(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor, bindingContext), allBindingsAccessor, viewModel, bindingContext);
        };

        this.update = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            return ko.bindingHandlers.template.update(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor, bindingContext), allBindingsAccessor, viewModel, bindingContext);
        };
    })
});
doLittle.views.viewBindingHandler.initialize = function () {
    ko.bindingHandlers.view = doLittle.views.viewBindingHandler.create();
    ko.jsonExpressionRewriting.bindingRewriteValidators.view = false; // Can't rewrite control flow bindings
    ko.virtualElements.allowedBindings.view = true;
};