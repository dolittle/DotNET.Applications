doLittle.namespace("doLittle.navigation", {
    DataNavigationFrameAttributeElementVisitor: doLittle.markup.ElementVisitor.extend(function (documentService) {
        this.visit = function (element, actions) {
            var dataNavigationFrame = element.attributes.getNamedItem("data-navigation-frame");
            if (!doLittle.isNullOrUndefined(dataNavigationFrame)) {
                var dataBindString = "";
                var dataBind = element.attributes.getNamedItem("data-bind");
                if (!doLittle.isNullOrUndefined(dataBind)) {
                    dataBindString = dataBind.value + ", ";
                } else {
                    dataBind = document.createAttribute("data-bind");
                }
                dataBind.value = dataBindString + "navigation: '" + dataNavigationFrame.value + "'";
                element.attributes.setNamedItem(dataBind);

                element.attributes.removeNamedItem("data-navigation-frame");
            }
        };
    })
});
