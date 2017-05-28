doLittle.namespace("doLittle.views", {
    MasterViewModel: doLittle.Type.extend(function (documentService) {
        var self = this;

        function deactivateViewModel(viewModel) {
            if (!doLittle.isNullOrUndefined(viewModel)) {
                if (doLittle.isFunction(viewModel.deactivated)) {
                    viewModel.deactivated();
                }
                
            }
        }


        function activateViewModel(viewModel) {
            if (!doLittle.isNullOrUndefined(viewModel) && doLittle.isFunction(viewModel.activated)) {
                viewModel.activated();
            }
        }


        this.setFor = function (element, viewModel) {
            var existingViewModel = self.getFor(element);
            if (existingViewModel !== viewModel) {
                deactivateViewModel(existingViewModel);
            }

            var name = documentService.getViewModelNameFor(element);
            self[name] = viewModel;

            activateViewModel(viewModel);
        };

        this.getFor = function (element) {
            var name = documentService.getViewModelNameFor(element);
            if (self.hasOwnProperty(name)) {
                return self[name];
            }
            return null;
        };


        this.clearFor = function (element) {
            var name = documentService.getViewModelNameFor(element);
            if (!self.hasOwnProperty(name)) {
                deactivateViewModel(self[name]);
                delete self[name];
                self[name] = null;
            }
        };

        this.apply = function () {
            ko.applyBindings(self);
        };

        this.applyTo = function (element) {
            ko.applyBindings(self, element);
        };
    })
});