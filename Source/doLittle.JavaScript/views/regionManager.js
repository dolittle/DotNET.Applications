doLittle.namespace("doLittle.views", {
    regionManager: doLittle.Singleton(function (documentService, regionDescriptorManager, messengerFactory, operationsFactory, tasksFactory) {
        /// <summary>Represents a manager that knows how to deal with Regions on the page</summary>
        var self = this;

        function createRegionInstance() {
            var instance = new doLittle.views.Region(messengerFactory, operationsFactory, tasksFactory);
            return instance;
        }


        function manageInheritance(element) {
            var parentRegion = documentService.getParentRegionFor(element);
            if (parentRegion) {
                doLittle.views.Region.prototype = parentRegion;
            } else {
                var topLevel = createRegionInstance();
                regionDescriptorManager.describeTopLevel(topLevel);
                doLittle.views.Region.prototype = topLevel;
            }
            return parentRegion;
        }

        function manageHierarchy(parentRegion) {
            var region = createRegionInstance();
            region.parent = parentRegion;
            if (parentRegion) {
                parentRegion.children.push(region);
            }
            return region;
        }

        this.getFor = function (view) {
            /// <summary>Gets the region for the given view and creates one if none exist</summary>
            /// <param name="view" type="View">View to get a region for</param>
            /// <returns>The region for the element</returns>

            var region;
            var element = view.element;
            if (documentService.hasOwnRegion(element)) {
                region = documentService.getRegionFor(element);
                region.view(view);
                return region;
            }

            var parentRegion = manageInheritance(element);
            region = manageHierarchy(parentRegion);
            region.view(view);
            documentService.setRegionOn(element, region);

            return region;
        };

        this.describe = function (view, region) {
            /// <summary>Describes a region for a view</summary>
            /// <param name="view" type="View">View to describe region for</param>
            /// <param name="region" type="Region">Region to describe for</param>
            /// <returns>A promise that can be continued for when the description is done</returns>
            var promise = doLittle.execution.Promise.create();
            var element = view.element;

            regionDescriptorManager.describe(view, region).continueWith(function () {
                promise.signal();
            });
            return promise;
        };

        this.getCurrent = function () {
            /// <summary>Gets the current region</summary>
            return doLittle.views.Region.current;
        };

        this.evict = function (region) {
            /// <summary>Evict a region from the page</summary>
            /// <param name="region" type="doLittle.views.Region">Region to evict</param>

            if (region.parentRegion) {
                region.parentRegion.children.remove(region);
            }
            region.parentRegion = null;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.regionManager = doLittle.views.regionManage;