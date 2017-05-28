doLittle.namespace("doLittle", {
    dependencyResolver: (function () {
        function resolveImplementation(namespace, name) {
            var resolvers = doLittle.dependencyResolvers.getAll();
            var resolvedSystem = null;
            resolvers.forEach(function (resolver) {
                if (resolvedSystem != null) {
                    return;
                }
                var canResolve = resolver.canResolve(namespace, name);
                if (canResolve) {
                    resolvedSystem = resolver.resolve(namespace, name);
                    return;
                }
            });

            return resolvedSystem;
        }

        function isType(system) {
            if (system != null &&
                system._super !== null) {

                if (typeof system._super !== "undefined" &&
                    system._super === doLittle.Type) {
                    return true;
                }

                if (isType(system._super) === true) {
                    return true;
                }
            }

            return false;
        }

        function handleSystemInstance(system) {
            if (isType(system)) {
                return system.create();
            }
            return system;
        }

        function beginHandleSystemInstance(system) {
            var promise = doLittle.execution.Promise.create();

            if (system != null &&
                system._super !== null &&
                typeof system._super !== "undefined" &&
                system._super === doLittle.Type) {

                system.beginCreate().continueWith(function (result, next) {
                    promise.signal(result);
                });
            } else {
                promise.signal(system);
            }

            return promise;
        }

        return {
            getDependenciesFor: function (func) {
                var dependencies = [];
                var parameters = doLittle.functionParser.parse(func);
                for (var i = 0; i < parameters.length; i++) {
                    dependencies.push(parameters[i].name);
                }
                return dependencies;
            },

            canResolve: function (namespace, name) {
                // Loop through resolvers and check if anyone can resolve it, if so return true - if not false
                var resolvers = doLittle.dependencyResolvers.getAll();
                var canResolve = false;

                resolvers.forEach(function (resolver) {
                    if (canResolve === true) {
                        return;
                    }

                    canResolve = resolver.canResolve(namespace, name);
                });

                return canResolve;
            },

            resolve: function (namespace, name) {
                var resolvedSystem = resolveImplementation(namespace, name);
                if (typeof resolvedSystem === "undefined" || resolvedSystem === null) {
                    console.log("Unable to resolve '" + name + "' in '" + namespace + "'");
                    throw new doLittle.UnresolvedDependencies();
                }

                if (resolvedSystem instanceof doLittle.execution.Promise) {
                    console.log("'" + name + "' was resolved as an asynchronous dependency, consider using beginCreate() or make the dependency available prior to calling create");
                    throw new doLittle.AsynchronousDependenciesDetected();
                }

                return handleSystemInstance(resolvedSystem);
            },

            beginResolve: function (namespace, name) {
                var promise = doLittle.execution.Promise.create();
                doLittle.configure.ready(function () {
                    var resolvedSystem = resolveImplementation(namespace, name);
                    if (typeof resolvedSystem === "undefined" || resolvedSystem === null) {
                        console.log("Unable to resolve '" + name + "' in '" + namespace + "'");
                        promise.fail(new doLittle.UnresolvedDependencies());
                    }

                    if (resolvedSystem instanceof doLittle.execution.Promise) {
                        resolvedSystem.continueWith(function (system, innerPromise) {
                            beginHandleSystemInstance(system)
                            .continueWith(function (actualSystem, next) {

                                promise.signal(handleSystemInstance(actualSystem));
                            }).onFail(function (e) { promise.fail(e); });
                        });
                    } else {
                        promise.signal(handleSystemInstance(resolvedSystem));
                    }
                });

                return promise;
            }
        };
    })()
});
doLittle.WellKnownTypesDependencyResolver.types.dependencyResolver = doLittle.dependencyResolver;