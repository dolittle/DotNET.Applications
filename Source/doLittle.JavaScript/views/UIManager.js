doLittle.namespace("doLittle.views", {
    UIManager: doLittle.Singleton(function(documentService) {
        var elementVisitorTypes = doLittle.markup.ElementVisitor.getExtenders();
        var elementVisitors = [];
        var postBindingVisitorTypes = doLittle.views.PostBindingVisitor.getExtenders();
        var postBindingVisitors = [];

        elementVisitorTypes.forEach(function (type) {
            elementVisitors.push(type.create());
        });

        postBindingVisitorTypes.forEach(function (type) {
            postBindingVisitors.push(type.create());
        });

        this.handle = function (root) {
            documentService.traverseObjects(function(element) {
                elementVisitors.forEach(function(visitor) {
                    var actions = doLittle.markup.ElementVisitorResultActions.create();
                    visitor.visit(element, actions);
                });
            }, root);
        };

        this.handlePostBinding = function (root) {
            documentService.traverseObjects(function (element) {
                postBindingVisitors.forEach(function (visitor) {
                    visitor.visit(element);
                });
            }, root);
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.UIManager = doLittle.views.UIManager;