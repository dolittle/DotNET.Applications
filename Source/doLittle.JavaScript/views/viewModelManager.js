doLittle.namespace("doLittle.views", {
    viewModelManager: doLittle.Singleton(function(assetsManager, documentService, viewModelLoader, regionManager, taskFactory, viewFactory, MasterViewModel) {
        var self = this;
        this.assetsManager = assetsManager;
        this.viewModelLoader = viewModelLoader;
        this.documentService = documentService;

        this.masterViewModel = MasterViewModel;

        this.hasForView = function (viewPath) {
            var scriptFile = doLittle.Path.changeExtension(viewPath, "js");
            scriptFile = doLittle.Path.makeRelative(scriptFile);
            var hasViewModel = self.assetsManager.hasScript(scriptFile);
            return hasViewModel;
        };

        this.getViewModelPathForView = function (viewPath) {
            var scriptFile = doLittle.Path.changeExtension(viewPath, "js");
            return scriptFile;
        };

        this.isLoaded = function (path) {
            var localPath = doLittle.Path.getPathWithoutFilename(path);
            var filename = doLittle.Path.getFilenameWithoutExtension(path);
            var namespacePath = doLittle.namespaceMappers.mapPathToNamespace(localPath);
            if (namespacePath != null) {
                var namespace = doLittle.namespace(namespacePath);

                if (filename in namespace) {
                    return true;
                }
            }
            return false;
        };
    })
});