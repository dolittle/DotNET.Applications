doLittle.namespace("doLittle.values", {
    dependencyProperty: doLittle.Type.extend(function (propertyName) {
        this.initialize = function (UIElement) { };
        this.dispose = function (UIElement) {};

        this.set = function (UIElement, value) {
            UIElement[propertyName] = value;
        };

        this.get = function (UIElement) {
            return UIElement[propertyName];
        };
    })
});

doLittle.values.DependencyProperty.register = function (owningType, name, dependencyPropertyType) {
};

doLittle.namespace("doLittle.DOM", {
    inputValueDependencyProperty: doLittle.values.dependencyProperty.extend(function() {

        function inputChanged(e) {
            if( doLittle.isFunction(e.target._changed) ) {
                e.target._changed(e.value);
            }
        }

        this.initialize = function (UIElement, changed) {
            UIElement._changed = changed;
            UIElement.addEventListener("change", inputChanged);
        };

        this.dispose = function (UIElement) {
            UIElement.removeEventListener("change", inputChanged);
        };
    })
});


doLittle.values.DependencyProperty.register(HTMLInputElement, "value", doLittle.DOM.inputValueDependencyProperty);
