doLittle.namespace("doLittle.markup", {
    NamingRoot: doLittle.Type.extend(function() {
        var self = this;
        this.target = null;

        this.find = function (name, element) {
            if (doLittle.isNullOrUndefined(element)) {
                if (doLittle.isNullOrUndefined(self.target)) {
                    return null;
                }
                element = self.target;
            }


            if (element.getAttribute("name") === name) {
                return element;
            }

            if (element.hasChildNodes()) {
                var child = element.firstChild;
                while (child) {
                    if (child.nodeType === 1) {
                        var foundElement = self.find(name, child);
                        if (foundElement != null) {
                            return foundElement;
                        }
                    }
                    child = child.nextSibling;
                }
            }

            return null;
        };
    })
});