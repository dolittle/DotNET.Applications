doLittle.namespace("doLittle",{
    assetsManager: {
        scripts: [],
        isInitialized: function() {
            return doLittle.assetsManager.scripts.length > 0;
        },
        initialize: function () {
            var promise = doLittle.execution.Promise.create();
            if (!doLittle.assetsManager.isInitialized()) {
                $.get("/doLittle/AssetsManager", { extension: "js" }, function (result) {
                    doLittle.assetsManager.initializeFromAssets(result);
                    promise.signal();
                }, "json");
            } else {
                promise.signal();
            }
            return promise;
        },
        initializeFromAssets: function (assets) {
            if (!doLittle.assetsManager.isInitialized()) {
                doLittle.assetsManager.scripts = assets;
                doLittle.namespaces.create().initialize();
            }
        },
        getScripts: function () {
            return doLittle.assetsManager.scripts;
        },
        hasScript: function(script) {
            return doLittle.assetsManager.scripts.some(function (scriptInSystem) {
                return scriptInSystem === script;
            });
        },
        getScriptPaths: function () {
            var paths = [];

            doLittle.assetsManager.scripts.forEach(function (fullPath) {
                var path = doLittle.Path.getPathWithoutFilename(fullPath);
                if (paths.indexOf(path) === -1) {
                    paths.push(path);
                }
            });
            return paths;
        }
    }
});