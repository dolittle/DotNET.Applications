doLittle.namespace("doLittle",{
    configureType: doLittle.Singleton(function(assetsManager) {
        var self = this;

        var defaultUriMapper = doLittle.StringMapper.create();
        defaultUriMapper.addMapping("{boundedContext}/{module}/{feature}/{view}", "{boundedContext}/{module}/{feature}/{view}.html");
        defaultUriMapper.addMapping("{boundedContext}/{feature}/{view}", "{boundedContext}/{feature}/{view}.html");
        defaultUriMapper.addMapping("{feature}/{view}", "{feature}/{view}.html");
        defaultUriMapper.addMapping("{view}", "{view}.html");
        doLittle.uriMappers.default = defaultUriMapper;

        var doLittleVisualizerUriMapper = doLittle.StringMapper.create();
        doLittleVisualizerUriMapper.addMapping("Visualizer/{module}/{view}", "/doLittle/Visualizer/{module}/{view}.html");
        doLittleVisualizerUriMapper.addMapping("Visualizer/{view}", "/doLittle/Visualizer/{view}.html");
        doLittle.uriMappers.doLittleVisualizer = doLittleVisualizerUriMapper;

        this.isReady = false;
        this.readyCallbacks = [];

        this.initializeLandingPage = true;
        this.applyMasterViewModel = true;

        function onReady() {
            doLittle.views.Region.current = document.body.region;
            self.isReady = true;
            for (var callbackIndex = 0; callbackIndex < self.readyCallbacks.length; callbackIndex++) {
                self.readyCallbacks[callbackIndex]();
            }
        }

        function hookUpNavigaionAndApplyViewModel() {
            doLittle.navigation.navigationManager.hookup();

            if (self.applyMasterViewModel === true) {
                doLittle.views.viewModelManager.create().masterViewModel.apply();
            }
        }

        function onStartup() {
            var configurators = doLittle.configurator.getExtenders();
            configurators.forEach(function (configuratorType) {
                var configurator = configuratorType.create();
                configurator.config(self);
            });


            doLittle.dependencyResolvers.DOMRootDependencyResolver.documentIsReady();
            doLittle.views.viewModelBindingHandler.initialize();
            doLittle.views.viewBindingHandler.initialize();
            doLittle.navigation.navigationBindingHandler.initialize();

            if (typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
                doLittle.WellKnownTypesDependencyResolver.types.history = History;
            }

            assetsManager.initialize().continueWith(function () {
                if (self.initializeLandingPage === true) {
                    doLittle.views.viewManager.create().initializeLandingPage().continueWith(hookUpNavigaionAndApplyViewModel);
                } else {
                    hookUpNavigaionAndApplyViewModel();
                }
                onReady();
            });
        }

        function reset() {
            self.isReady = false;
            self.readyCallbacks = [];
        }

        this.ready = function(callback) {
            if (self.isReady === true) {
                callback();
            } else {
                self.readyCallbacks.push(callback);
            }
        };

        $(function () {
            onStartup();
        });
    })
});
doLittle.configure = doLittle.configureType.create();
