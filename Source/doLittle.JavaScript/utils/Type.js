doLittle.namespace("doLittle", {
    Type: function () {
    }
});

(function () {
    function throwIfMissingTypeDefinition(typeDefinition) {
        if (typeDefinition == null || typeof typeDefinition === "undefined") {
            throw new doLittle.MissingTypeDefinition();
        }
    }

    function throwIfTypeDefinitionIsObjectLiteral(typeDefinition) {
        if (typeof typeDefinition === "object") {
            throw new doLittle.ObjectLiteralNotAllowed();
        }
    }

    function addStaticProperties(typeDefinition) {
        for (var property in doLittle.Type) {
            if (doLittle.Type.hasOwnProperty(property) && property !== "_extenders") {
                typeDefinition[property] = doLittle.Type[property];
            }
        }
    }

    function setupDependencies(typeDefinition) {
        typeDefinition._dependencies = doLittle.dependencyResolver.getDependenciesFor(typeDefinition);

        var firstParameter = true;
        var createFunctionString = "Function('definition', 'dependencies','return new definition(";
            
        if( typeof typeDefinition._dependencies !== "undefined" ) {
            typeDefinition._dependencies.forEach(function(dependency, index) {
                if (!firstParameter) {
                    createFunctionString += ",";
                }
                firstParameter = false;
                createFunctionString += "dependencies[" + index + "]";
            });
        }
        createFunctionString += ");')";

        typeDefinition.createFunction = eval(createFunctionString);
    }

    function getDependencyInstances(namespace, typeDefinition) {
        var dependencyInstances = [];
        if( typeof typeDefinition._dependencies !== "undefined" ) {
            typeDefinition._dependencies.forEach(function(dependency) {
                var dependencyInstance = doLittle.dependencyResolver.resolve(namespace, dependency);
                dependencyInstances.push(dependencyInstance);
            });
        }
        return dependencyInstances;
    }

    function resolve(namespace, dependency, index, instances, typeDefinition, resolvedCallback) {
        var promise = 
            doLittle.dependencyResolver
                .beginResolve(namespace, dependency)
                .continueWith(function(result, nextPromise) {
                    instances[index] = result;
                    resolvedCallback(result, nextPromise);
                });
        return promise;
    }


    function beginGetDependencyInstances(namespace, typeDefinition, instanceHash) {
        function resolved(result, nextPromise) {
            solvedDependencies++;
            if (solvedDependencies === dependenciesToResolve) {
                promise.signal(dependencyInstances);
            }
        }

        var promise = doLittle.execution.Promise.create();
        var dependencyInstances = [];
        var solvedDependencies = 0;
        if( typeof typeDefinition._dependencies !== "undefined" ) {
            var dependenciesToResolve = typeDefinition._dependencies.length;
            var actualDependencyIndex = 0;
            var dependency = "";
            for( var dependencyIndex=0; dependencyIndex<dependenciesToResolve; dependencyIndex++ ) {
                dependency = typeDefinition._dependencies[dependencyIndex];

                if (instanceHash && instanceHash.hasOwnProperty(dependency)) {
                    dependencyInstances[dependencyIndex] = instanceHash[dependency];
                    solvedDependencies++;
                    if (solvedDependencies === dependenciesToResolve) {
                        promise.signal(dependencyInstances);
                    }
                } else {
                    resolve(namespace, dependency, dependencyIndex, dependencyInstances, typeDefinition, resolved).onFail(promise.fail);
                }
            }

        }
        return promise;
    }

    function expandInstancesHashToDependencies(typeDefinition, instanceHash, dependencyInstances) {
        if (typeof typeDefinition._dependencies === "undefined" || typeDefinition._dependencies == null) {
            return;
        }
        for( var dependency in instanceHash ) {
            for( var dependencyIndex=0; dependencyIndex<typeDefinition._dependencies.length; dependencyIndex++ ) {
                if( typeDefinition._dependencies[dependencyIndex] === dependency ) {
                    dependencyInstances[dependencyIndex] = instanceHash[dependency];
                }
            }
        }
    }

    function expandDependenciesToInstanceHash(typeDefinition, dependencies, instanceHash) {
        for( var dependencyIndex=0; dependencyIndex<dependencies.length; dependencyIndex++ ) {
            instanceHash[typeDefinition._dependencies[dependencyIndex]] = dependencies[dependencyIndex];
        }
    }

    function resolveDependencyInstancesThatHasNotBeenResolved(dependencyInstances, typeDefinition) {
        dependencyInstances.forEach(function(dependencyInstance, index) {
            if( dependencyInstance == null || typeof dependencyInstance === "undefined" ) {
                var dependency = typeDefinition._dependencies[index];
                dependencyInstances[index] = doLittle.dependencyResolver.resolve(typeDefinition._namespace, dependency);
            }
        });
    }

    function resolveDependencyInstances(instanceHash, typeDefinition) {
        var dependencyInstances = [];
        if( typeof instanceHash === "object" ) {
            expandInstancesHashToDependencies(typeDefinition, instanceHash, dependencyInstances);
        } 
        if( typeof typeDefinition._dependencies !== "undefined" && typeDefinition._dependencies.length > 0 ) {
            if( dependencyInstances.length > 0 ) {
                resolveDependencyInstancesThatHasNotBeenResolved(dependencyInstances, typeDefinition);
            } else {
                dependencyInstances = getDependencyInstances(typeDefinition._namespace, typeDefinition);
            }
        }
        return dependencyInstances;
    }

    function addMissingDependenciesAsNullFromTypeDefinition(instanceHash, typeDefinition) {
        if (typeof typeDefinition._dependencies === "undefined") {
            return;
        }
        if (typeof instanceHash === "undefined" || instanceHash == null) {
            return;
        }
        for( var index=0; index<typeDefinition._dependencies.length; index++ ) {
            var dependency = typeDefinition._dependencies[index];
            if (!(dependency in instanceHash)) {
                instanceHash[dependency] = null;
            }
        }
    }

    function handleOnCreate(type, lastDescendant, currentInstance) {
        if (currentInstance == null || typeof currentInstance === "undefined") {
            return;
        }

        if( typeof type !== "undefined" && typeof type.prototype !== "undefined" ) {
            handleOnCreate(type._super, lastDescendant, type.prototype);
        }

        if( currentInstance.hasOwnProperty("onCreated") && typeof currentInstance.onCreated === "function" ) {
            currentInstance.onCreated(lastDescendant);
        }
    }

    doLittle.Type._extenders = [];

    doLittle.Type.scope = {
        getFor : function(namespace, name) {
            return null;
        }
    };

    doLittle.Type.typeOf = function (type) {

        if (typeof this._super === "undefined" ||
            typeof this._super._typeId === "undefined") {
            return false;
        }

        if (this._super._typeId === type._typeId) {
            return true;
        }

        if (typeof type._super !== "undefined") {
            var isType = this._super.typeOf(type);
            if (isType === true) {
                return true;
            }
        }


        return false;
    };

    doLittle.Type.getExtenders = function () {
        return this._extenders;
    };

    doLittle.Type.getExtendersIn = function (namespace) {
        var inNamespace = [];
        
        this._extenders.forEach(function (extender) {
            var current = namespace;
            while (current !== window) {
                if (extender._namespace === current) {
                    inNamespace.push(extender);
                    break;
                }

                if (doLittle.isUndefined(current.parent)) {
                    break;
                }

                current = current.parent;
            }
            
        });
        return inNamespace;
    };

  

    doLittle.Type.extend = function (typeDefinition) {     
        throwIfMissingTypeDefinition(typeDefinition);
        throwIfTypeDefinitionIsObjectLiteral(typeDefinition);

        addStaticProperties(typeDefinition);
        setupDependencies(typeDefinition);
        typeDefinition._super = this;
        typeDefinition._typeId = doLittle.Guid.create();
        typeDefinition._extenders = [];
        doLittle.Type.registerExtender(this, typeDefinition);
        return typeDefinition;
    };

    doLittle.Type.registerExtender = function (typeExtended, typeDefined) {
        var superType = typeExtended;

        while (superType != null) {
            if (superType._extenders.indexOf(typeDefined) === -1) {
                superType._extenders.push(typeDefined);
            }
            superType = superType._super;
        }
    };

    doLittle.Type.scopeTo = function(scope) {
        if( typeof scope === "function" ) {
            this.scope = {
                getFor: scope
            };
        } else {
            if( typeof scope.getFor === "function" ) {
                this.scope = scope;
            } else {
                this.scope = {
                    getFor: function () {
                        return scope;
                    }
                };
            }
        }
        return this;
    };

    doLittle.Type.defaultScope = function() {
        this.scope = {
            getFor: function() {
                return null;
            }
        };
        return this;
    };

    doLittle.Type.requires = function () {
        for (var argumentIndex = 0; argumentIndex < arguments.length; argumentIndex++) {
            this._dependencies.push(arguments[argumentIndex]);
        }

        return this;
    };

    doLittle.Type.create = function (instanceHash, isSuper) {
        var actualType = this;
        if( this._super != null ) {
            actualType.prototype = this._super.create(instanceHash, true);
        }
        addMissingDependenciesAsNullFromTypeDefinition(instanceHash, this);
        var scope = null;
        if( this !== doLittle.Type ) {
            this.instancesPerScope = this.instancesPerScope || {};

            scope = this.scope.getFor(this._namespace, this._name, this._typeId);
            if (scope != null && this.instancesPerScope.hasOwnProperty(scope)) {
                return this.instancesPerScope[scope];
            }
        }

        var instance = null;
        if( typeof this.createFunction !== "undefined" ) {
            var dependencyInstances = resolveDependencyInstances(instanceHash, this);
            instance = this.createFunction(this, dependencyInstances);
        } else {
            instance = new actualType();    
        }

        instance._type = actualType;

        if( isSuper !== true ) {
            handleOnCreate(actualType, instance, instance);
        }

        if( scope != null ) {
            this.instancesPerScope[scope] = instance;
        }

        return instance;
    };

    doLittle.Type.createWithoutScope = function (instanceHash, isSuper) {
        var scope = this.scope;
        this.defaultScope();
        var instance = this.create(instanceHash, isSuper);
        this.scope = scope;
        return instance;
    };

    doLittle.Type.ensure = function () {
        var promise = doLittle.execution.Promise.create();

        var loadedDependencies = 0;
        var dependenciesToResolve = this._dependencies.length;
        var namespace = this._namespace;
        var resolver = doLittle.dependencyResolver;
        if (dependenciesToResolve > 0) {
            this._dependencies.forEach(function (dependency) {

                if (resolver.canResolve(namespace, dependency)) {
                    resolver.beginResolve(namespace, dependency).continueWith(function (resolvedSystem) {
                        loadedDependencies++;
                        if (loadedDependencies === dependenciesToResolve) {
                            promise.signal();
                        }
                    });
                } else {
                    dependenciesToResolve--;
                    if (loadedDependencies === dependenciesToResolve) {
                        promise.signal();
                    }
                }
            });
        } else {
            promise.signal();
        }



        return promise;
    };

    doLittle.Type.beginCreate = function(instanceHash) {
        var self = this;

        var promise = doLittle.execution.Promise.create();
        var superPromise = doLittle.execution.Promise.create();
        superPromise.onFail(function (e) {
            promise.fail(e);
        });

        if( this._super != null ) {
            this._super.beginCreate(instanceHash).continueWith(function (_super, nextPromise) {
                superPromise.signal(_super);
            }).onFail(function (e) {
                promise.fail(e);
            });
        } else {
            superPromise.signal(null);
        }

        superPromise.continueWith(function(_super, nextPromise) {
            self.prototype = _super;

            if( self._dependencies == null || 
                typeof self._dependencies === "undefined" || 
                self._dependencies.length === 0) {
                var instance = self.create(instanceHash);
                promise.signal(instance);
            } else {
                beginGetDependencyInstances(self._namespace, self, instanceHash)
                    .continueWith(function(dependencies, nextPromise) {
                        var dependencyInstances = {};
                        expandDependenciesToInstanceHash(self, dependencies, dependencyInstances);
                        if( typeof instanceHash === "object" ) {
                            for( var property in instanceHash ) {
                                dependencyInstances[property] = instanceHash[property];
                            }
                        }

                        try {
                            var instance = self.create(dependencyInstances);
                            promise.signal(instance);
                        } catch (e) {
                            promise.fail(e);
                        }
                    });

            }
        });

        return promise;
    };
})();