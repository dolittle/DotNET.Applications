doLittle.namespace("doLittle.views", {
    DataViewModelFileAttributeElementVisitor: doLittle.markup.ElementVisitor.extend(function () {
        this.visit = function (element, actions) {

            var dataView = element.attributes.getNamedItem("data-viewmodel-file");
            if (!doLittle.isNullOrUndefined(dataView)) {
                var dataBindString = "";
                var dataBind = element.attributes.getNamedItem("data-bind");
                if (!doLittle.isNullOrUndefined(dataBind)) {
                    dataBindString = dataBind.value + ", ";
                } else {
                    dataBind = document.createAttribute("data-bind");
                }
                dataBind.value = dataBindString + "viewModel: '" + dataView.value + "'";
                element.attributes.setNamedItem(dataBind);
                element.attributes.removeNamedItem("data-viewmodel-file");
            }
        };
    })
});