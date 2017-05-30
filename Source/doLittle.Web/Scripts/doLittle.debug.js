
function polyfillForEach() {
    if (typeof Array.prototype.forEach !== "function") {
        Array.prototype.forEach = function (callback, thisArg) {
            if (typeof thisArg === "undefined") {
                thisArg = window;
            }
            for (var i = 0; i < this.length; i++) {
                callback.call(thisArg, this[i], i, this);
            }
        };
    }
}

function polyFillClone() {
    if (typeof Array.prototype.clone !== "function") {
        Array.prototype.clone = function () {
            return this.slice(0);
        };
    }
}

function shallowEquals() {
    if (typeof Array.prototype.shallowEquals !== "function") {
        Array.prototype.shallowEquals = function (other) {
            if (this === other) {
                return true;
            }
            if (this === null || other === null) {
                return false;
            }
            if (this.length !== other.length) {
                return false;
            }

            for (var i = 0; i < this.length; i++) {
                if (this[i] !== other[i]) {
                    return false;
                }
            }
            return true;
        };
    }
}

Array.prototype.insert = function (index, item) {
    this.splice(index, 0, item);
};

(function () {
    polyfillForEach();
    polyFillClone();
    shallowEquals();
})();
if (typeof String.prototype.startsWith !== 'function') {
    String.prototype.startsWith = function (str) {
        return str.length > 0 && this.substring(0, str.length) === str;
    };
}

if (typeof String.prototype.endsWith !== 'function') {
    String.prototype.endsWith = function (str) {
        return str.length > 0 && this.substring(this.length - str.length, this.length) === str;
    };
}

String.prototype.replaceAll = function (toReplace, replacement) {
    var result = this.split(toReplace).join(replacement);
    return result;
};

String.prototype.toCamelCase = function () {
    var result = this.charAt(0).toLowerCase() + this.substring(1);
    result = result.replaceAll("-", "");
    return result;
};

String.prototype.toPascalCase = function () {
    var result = this.charAt(0).toUpperCase() + this.substring(1);
    result = result.replaceAll("-", "");
    return result;
};

String.prototype.hashCode = function () {
    var charCode, hash = 0;
    if (this.length === 0) {
        return hash;
    }
    for (var i = 0; i < this.length; i++) {
        charCode = this.charCodeAt(i);
        hash = ((hash << 5) - hash) + charCode;
        hash = hash & hash;
    }
    return hash;
};
NodeList.prototype.forEach = Array.prototype.forEach;
NodeList.prototype.length = Array.prototype.length;
HTMLElement.prototype.knownElementTypes = [
    "a",
    "abbr",
    "acronym",
    "address",
    "applet",
    "area",
    "article",
    "aside",
    "audio",
    "b",
    "base",
    "basefont",
    "bdi",
    "bdo",
    "bgsound",
    "big",
    "blink",
    "blockquote",
    "body",
    "br",
    "button",
    "canvas",
    "caption",
    "center",
    "cite",
    "col",
    "colgroup",
    "content",
    "code",
    "data",
    "datalist",
    "dd",
    "decorator",
    "del",
    "details",
    "dfn",
    "dir",
    "div",
    "dl",
    "dt",
    "em",
    "embed",
    "fieldset",
    "figcaption",
    "figure",
    "font",
    "footer",
    "form",
    "frame",
    "frameset",
    "h1",
    "h2",
    "h3",
    "h4",
    "h5",
    "h6",
    "head",
    "header",
    "hgroup",
    "hr",
    "html",
    "i",
    "iframe",
    "img",
    "input",
    "ins",
    "isindex",
    "kbd",
    "keygen",
    "label",
    "legend",
    "li",
    "link",
    "listing",
    "main",
    "map",
    "mark",
    "marque",
    "menu",
    "menuitem",
    "meta",
    "meter",
    "nav",
    "nobr",
    "noframes",
    "noscript",
    "object",
    "ol",
    "optgroup",
    "option",
    "output",
    "p",
    "param",
    "plaintext",
    "pre",
    "progress",
    "q",
    "rp",
    "rt",
    "ruby",
    "s",
    "samp",
    "script",
    "section",
    "select",
    "shadow",
    "small",
    "source",
    "spacer",
    "span",
    "strike",
    "strong",
    "style",
    "sub",
    "summary",
    "sup",
    "table",
    "tbody",
    "td",
    "template",
    "textarea",
    "tfoot",
    "th",
    "thead",
    "time",
    "title",
    "tr",
    "track",
    "tt",
    "u",
    "ul",
    "var",
    "video",
    "wbr",
    "xmp"
];
HTMLElement.prototype.isKnownType = function () {
    if (!doLittle.isNullOrUndefined("HTMLUnknownElement")) {
        if (this.constructor.toString().indexOf("HTMLUnknownElement") < 0) {
            return true;
        }
        return false;
    }

    var isKnown = this.constructor !== HTMLElement;
    if (isKnown === false) {
        var tagName = this.tagName.toLowerCase();
        isKnown = this.knownElementTypes.some(function (type) {
            if (tagName === type) {
                return true;
            }
        });
    }
    return isKnown;
};
HTMLElement.prototype.getChildElements = function () {
    var children = [];
    this.childNodes.forEach(function (node) {
        if (node.nodeType === 1) {
            children.push(node);
        }
    });
    return children;
};
HTMLCollection.prototype.forEach = Array.prototype.forEach;
HTMLCollection.prototype.length = Array.prototype.length;
HTMLElement.prototype.knownElementTypes = [
    "a",
    "abbr",
    "acronym",
    "address",
    "applet",
    "area",
    "article",
    "aside",
    "audio",
    "b",
    "base",
    "basefont",
    "bdi",
    "bdo",
    "bgsound",
    "big",
    "blink",
    "blockquote",
    "body",
    "br",
    "button",
    "canvas",
    "caption",
    "center",
    "cite",
    "col",
    "colgroup",
    "content",
    "code",
    "data",
    "datalist",
    "dd",
    "decorator",
    "del",
    "details",
    "dfn",
    "dir",
    "div",
    "dl",
    "dt",
    "em",
    "embed",
    "fieldset",
    "figcaption",
    "figure",
    "font",
    "footer",
    "form",
    "frame",
    "frameset",
    "h1",
    "h2",
    "h3",
    "h4",
    "h5",
    "h6",
    "head",
    "header",
    "hgroup",
    "hr",
    "html",
    "i",
    "iframe",
    "img",
    "input",
    "ins",
    "isindex",
    "kbd",
    "keygen",
    "label",
    "legend",
    "li",
    "link",
    "listing",
    "main",
    "map",
    "mark",
    "marque",
    "menu",
    "menuitem",
    "meta",
    "meter",
    "nav",
    "nobr",
    "noframes",
    "noscript",
    "object",
    "ol",
    "optgroup",
    "option",
    "output",
    "p",
    "param",
    "plaintext",
    "pre",
    "progress",
    "q",
    "rp",
    "rt",
    "ruby",
    "s",
    "samp",
    "script",
    "section",
    "select",
    "shadow",
    "small",
    "source",
    "spacer",
    "span",
    "strike",
    "strong",
    "style",
    "sub",
    "summary",
    "sup",
    "table",
    "tbody",
    "td",
    "template",
    "textarea",
    "tfoot",
    "th",
    "thead",
    "time",
    "title",
    "tr",
    "track",
    "tt",
    "u",
    "ul",
    "var",
    "video",
    "wbr",
    "xmp"
];
HTMLElement.prototype.isKnownType = function () {
    if (!doLittle.isNullOrUndefined("HTMLUnknownElement")) {
        if (this.constructor.toString().indexOf("HTMLUnknownElement") < 0) {
            return true;
        }
        return false;
    }

    var isKnown = this.constructor !== HTMLElement;
    if (isKnown === false) {
        var tagName = this.tagName.toLowerCase();
        isKnown = this.knownElementTypes.some(function (type) {
            if (tagName === type) {
                return true;
            }
        });
    }
    return isKnown;
};
HTMLElement.prototype.getChildElements = function () {
    var children = [];
    this.childNodes.forEach(function (node) {
        if (node.nodeType === 1) {
            children.push(node);
        }
    });
    return children;
};
// From the following thread : http://stackoverflow.com/questions/1056728/formatting-a-date-in-javascript
// author: meizz
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    };

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1,
              RegExp.$1.length === 1 ? o[k] :
                ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};
/*
 * classList.js: Cross-browser full element.classList implementation.
 * 2012-11-15
 *
 * By Eli Grey, http://eligrey.com
 * Public Domain.
 * NO WARRANTY EXPRESSED OR IMPLIED. USE AT YOUR OWN RISK.
 */

/*global self, document, DOMException */

/*! @source http://purl.eligrey.com/github/classList.js/blob/master/classList.js*/

if ("document" in self && !(
        "classList" in document.createElement("_") &&
        "classList" in document.createElementNS("http://www.w3.org/2000/svg", "svg")
    )) {

    (function (view) {

        "use strict";

        if (!('Element' in view)) return;

        var
              classListProp = "classList"
            , protoProp = "prototype"
            , elemCtrProto = view.Element[protoProp]
            , objCtr = Object
            , strTrim = String[protoProp].trim || function () {
                return this.replace(/^\s+|\s+$/g, "");
            }
            , arrIndexOf = Array[protoProp].indexOf || function (item) {
                var
                      i = 0
                    , len = this.length
                ;
                for (; i < len; i++) {
                    if (i in this && this[i] === item) {
                        return i;
                    }
                }
                return -1;
            }
            // Vendors: please allow content code to instantiate DOMExceptions
            , DOMEx = function (type, message) {
                this.name = type;
                this.code = DOMException[type];
                this.message = message;
            }
            , checkTokenAndGetIndex = function (classList, token) {
                if (token === "") {
                    throw new DOMEx(
                          "SYNTAX_ERR"
                        , "An invalid or illegal string was specified"
                    );
                }
                if (/\s/.test(token)) {
                    throw new DOMEx(
                          "INVALID_CHARACTER_ERR"
                        , "String contains an invalid character"
                    );
                }
                return arrIndexOf.call(classList, token);
            }
            , ClassList = function (elem) {
                var
                      trimmedClasses = strTrim.call(elem.getAttribute("class") || "")
                    , classes = trimmedClasses ? trimmedClasses.split(/\s+/) : []
                    , i = 0
                    , len = classes.length
                ;
                for (; i < len; i++) {
                    this.push(classes[i]);
                }
                this._updateClassName = function () {
                    elem.setAttribute("class", this.toString());
                };
            }
            , classListProto = ClassList[protoProp] = []
            , classListGetter = function () {
                return new ClassList(this);
            }
        ;
        // Most DOMException implementations don't allow calling DOMException's toString()
        // on non-DOMExceptions. Error's toString() is sufficient here.
        DOMEx[protoProp] = Error[protoProp];
        classListProto.item = function (i) {
            return this[i] || null;
        };
        classListProto.contains = function (token) {
            token += "";
            return checkTokenAndGetIndex(this, token) !== -1;
        };
        classListProto.add = function () {
            var
                  tokens = arguments
                , i = 0
                , l = tokens.length
                , token
                , updated = false
            ;
            do {
                token = tokens[i] + "";
                if (checkTokenAndGetIndex(this, token) === -1) {
                    this.push(token);
                    updated = true;
                }
            }
            while (++i < l);

            if (updated) {
                this._updateClassName();
            }
        };
        classListProto.remove = function () {
            var
                  tokens = arguments
                , i = 0
                , l = tokens.length
                , token
                , updated = false
            ;
            do {
                token = tokens[i] + "";
                var index = checkTokenAndGetIndex(this, token);
                if (index !== -1) {
                    this.splice(index, 1);
                    updated = true;
                }
            }
            while (++i < l);

            if (updated) {
                this._updateClassName();
            }
        };
        classListProto.toggle = function (token, forse) {
            token += "";

            var
                  result = this.contains(token)
                , method = result ?
                    forse !== true && "remove"
                :
                    forse !== false && "add"
            ;

            if (method) {
                this[method](token);
            }

            return !result;
        };
        classListProto.toString = function () {
            return this.join(" ");
        };

        if (objCtr.defineProperty) {
            var classListPropDesc = {
                get: classListGetter
                , enumerable: true
                , configurable: true
            };
            try {
                objCtr.defineProperty(elemCtrProto, classListProp, classListPropDesc);
            } catch (ex) { // IE 8 doesn't support enumerable:true
                if (ex.number === -0x7FF5EC54) {
                    classListPropDesc.enumerable = false;
                    objCtr.defineProperty(elemCtrProto, classListProp, classListPropDesc);
                }
            }
        } else if (objCtr[protoProp].__defineGetter__) {
            elemCtrProto.__defineGetter__(classListProp, classListGetter);
        }

    }(self));
}
// From: http://www.jonathantneal.com/blog/faking-the-future/
this.Element && (function (ElementPrototype, polyfill) {
    function NodeList() { [polyfill] }
    NodeList.prototype.length = Array.prototype.length;

    ElementPrototype.matchesSelector = ElementPrototype.matchesSelector ||
    ElementPrototype.mozMatchesSelector ||
    ElementPrototype.msMatchesSelector ||
    ElementPrototype.oMatchesSelector ||
    ElementPrototype.webkitMatchesSelector ||
    function matchesSelector(selector) {
        var results = this.parentNode.querySelectorAll(selector);
        var resultsIndex = -1;

        while (results[++resultsIndex] && results[resultsIndex] != this) {}

        return !!results[resultsIndex];
    };

    ElementPrototype.ancestorQuerySelectorAll = ElementPrototype.ancestorQuerySelectorAll ||
    ElementPrototype.mozAncestorQuerySelectorAll ||
    ElementPrototype.msAncestorQuerySelectorAll ||
    ElementPrototype.oAncestorQuerySelectorAll ||
    ElementPrototype.webkitAncestorQuerySelectorAll ||
    function ancestorQuerySelectorAll(selector) {
        for (var cite = this, newNodeList = new NodeList(); cite = cite.parentElement;) {
            if (cite.matchesSelector(selector)) Array.prototype.push.call(newNodeList, cite);
        }
 
        return newNodeList;
    };
 
    ElementPrototype.ancestorQuerySelector = ElementPrototype.ancestorQuerySelector ||
    ElementPrototype.mozAncestorQuerySelector ||
    ElementPrototype.msAncestorQuerySelector ||
    ElementPrototype.oAncestorQuerySelector ||
    ElementPrototype.webkitAncestorQuerySelector ||
    function ancestorQuerySelector(selector) {
        return this.ancestorQuerySelectorAll(selector)[0] || null;
    };
})(Element.prototype);
var doLittle = doLittle || {};
(function(global, undefined) {
    doLittle.extend = function extend(destination, source) {
        return $.extend(destination, source);
    };
})(window);
var doLittle = doLittle || {};
doLittle.namespace = function (ns, content) {

    // Todo: this should not be needed, it is a symptom of something using it being wrong!!! Se issue #232 on GitHub (http://github.com/dolittle/doLittle/issues/232)
    ns = ns.replaceAll("..", ".");
    if (ns.endsWith(".")) {
        ns = ns.substr(0, ns.length - 1);
    }
    if (ns.startsWith(".")) {
        ns = ns.substr(1);
    }

    var parent = window;
    var name = "";
    var parts = ns.split('.');
    parts.forEach(function (part) {
        if (name.length > 0) {
            name += ".";
        }
        name += part;
        if (!Object.prototype.hasOwnProperty.call(parent, part)) {
            parent[part] = {};
            parent[part].parent = parent;
            parent[part].name = name;
        }
        parent = parent[part];
    });

    if (typeof content === "object") {
        doLittle.namespace.current = parent;

        var property;

        for (property in content) {
            parent[property] = content[property];
        }

        for (property in parent) {
            if (parent.hasOwnProperty(property)) {
                parent[property]._namespace = parent;
                parent[property]._name = property;
            }
        }
        doLittle.namespace.current = null;
    }

    return parent;
};
doLittle.namespace("doLittle.execution", {
    Promise: function () {
        var self = this;

        this.id = doLittle.Guid.create();

        this.signalled = false;
        this.callback = null;
        this.error = null;
        this.hasFailed = false;
        this.failedCallback = null;

        function onSignal() {
            if (self.callback != null && typeof self.callback !== "undefined") {
                if (typeof self.signalParameter !== "undefined") {
                    self.callback(self.signalParameter, doLittle.execution.Promise.create());
                } else {
                    self.callback(doLittle.execution.Promise.create());
                }
            }
        }

        this.fail = function (error) {
            if (self.failedCallback != null) {
                self.failedCallback(error);
            }
            self.hasFailed = true;
            self.error = error;
        };

        this.onFail = function (callback) {
            if (self.hasFailed) {
                callback(self.error);
            } else {
                self.failedCallback = callback;
            }
            return self;
        };


        this.signal = function (parameter) {
            self.signalled = true;
            self.signalParameter = parameter;
            onSignal();
        };

        this.continueWith = function (callback) {
            this.callback = callback;
            if (self.signalled === true) {
                onSignal();
            }
            return self;
        };
    }
});

doLittle.execution.Promise.create = function() {
	var promise = new doLittle.execution.Promise();
	return promise;
};
doLittle.namespace("doLittle", {
    isObject: function (o) {
        if (o === null || typeof o === "undefined" ) {
            return false;
        }
        return Object.prototype.toString.call(o) === '[object Object]';
    }
});
doLittle.namespace("doLittle", {
    isNumber: function (number) {
        if (doLittle.isString(number)) {
            if (number.length > 1 && number[0] === '0') {
                return false;
            }
        }

        return !isNaN(parseFloat(number)) && isFinite(number);
    }
});
doLittle.namespace("doLittle", {
    isArray : function(o) {
        return Object.prototype.toString.call(o) === '[object Array]';
    }
});
doLittle.namespace("doLittle", {
    isString: function (value) {
        return typeof value === "string";
        }
});
doLittle.namespace("doLittle", {
    isNull: function (value) {
        return value === null;
    }
});
doLittle.namespace("doLittle", {
    isUndefined: function (value) {
        return typeof value === "undefined";
    }
});
doLittle.namespace("doLittle", {
    isNullOrUndefined: function (value) {
        return doLittle.isUndefined(value) || doLittle.isNull(value);
    }
});
doLittle.namespace("doLittle", {
    isFunction: function (value) {
        return typeof value === "function";
    }
});
doLittle.namespace("doLittle", {
    isType: function (o) {
        if (doLittle.isNullOrUndefined(o)) {
            return false;
        }
		return typeof o._typeId !== "undefined";
	}
});
doLittle.namespace("doLittle", {
    functionParser: {
        parse: function(func) {
            var result = [];

            var match = func.toString().match(/function\w*\s*\((.*?)\)/);
            if (match !== null) {
                var functionArguments = match[1].split(/\s*,\s*/);
                functionArguments.forEach(function (item) {
                    if (item.trim().length > 0) {
                        result.push({
                            name: item
                        });
                    }
                });
            }

            return result;
        }
    }
});
doLittle.namespace("doLittle", {
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
doLittle.namespace("doLittle", {
    WellKnownTypesDependencyResolver: function () {
        var self = this;
        this.types = doLittle.WellKnownTypesDependencyResolver.types;

        this.canResolve = function (namespace, name) {
            return self.types.hasOwnProperty(name);
        };

        this.resolve = function (namespace, name) {
            return self.types[name];
        };
    }
});

doLittle.WellKnownTypesDependencyResolver.types = {
    options: {}
};

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
doLittle.namespace("doLittle", {
    dependencyResolvers: (function () {
        return {
            getAll: function () {
                var resolvers = [
                    new doLittle.WellKnownTypesDependencyResolver(),
                    new doLittle.DefaultDependencyResolver(),
                    new doLittle.KnownArtifactTypesDependencyResolver(),
                    new doLittle.KnownArtifactInstancesDependencyResolver(),

                ];
                for (var property in this) {
                    if (property.indexOf("_") !== 0 &&
                        this.hasOwnProperty(property) &&
                        typeof this[property] !== "function") {
                        resolvers.push(this[property]);
                    }
                }
                return resolvers;
            }
        };
    })()
});
doLittle.namespace("doLittle", {
    DefaultDependencyResolver: function () {
        var self = this;

        this.doesNamespaceHave = function (namespace, name) {
            return namespace.hasOwnProperty(name);
        };

        this.doesNamespaceHaveScriptReference = function (namespace, name) {
            if (namespace.hasOwnProperty("_scripts") && doLittle.isArray(namespace._scripts)) {
                for (var i = 0; i < namespace._scripts.length; i++) {
                    var script = namespace._scripts[i];
                    if (script === name) {
                        return true;
                    }
                }
            }
            return false;
        };

        this.getFileName = function (namespace, name) {
            var fileName = "";
            if (typeof namespace._path !== "undefined") {
                fileName += namespace._path;
                if (!fileName.endsWith("/")) {
                    fileName += "/";
                }
            }
            fileName += name;
            if (!fileName.endsWith(".js")) {
                fileName += ".js";
            }
            fileName = fileName.replaceAll("//", "/");
            return fileName;

        };

        this.loadScriptReference = function (namespace, name, promise) {
            var fileName = self.getFileName(namespace, name);
            var file = doLittle.io.fileFactory.create().create(fileName, doLittle.io.fileType.javaScript);

            doLittle.io.fileManager.create().load([file]).continueWith(function (types) {
                var system = types[0];
                if (self.doesNamespaceHave(namespace, name)) {
                    system = namespace[name];
                }
                promise.signal(system);
            });
        };


        this.canResolve = function (namespace, name) {
            var current = namespace;
            while (current != null && current !== window) {
                if (self.doesNamespaceHave(current, name)) {
                    return true;
                }
                if (self.doesNamespaceHaveScriptReference(current, name) ) {
                    return true;
                }
                if (current === current.parent) {
                    break;
                }
                current = current.parent;
            }

            return false;
        };

        this.resolve = function (namespace, name) {
            var current = namespace;
            while (current != null && current !== window) {
                if (self.doesNamespaceHave(current, name)) {
                    return current[name];
                }
                if (self.doesNamespaceHaveScriptReference(current, name) ) {
                    var promise = doLittle.execution.Promise.create();       
                    self.loadScriptReference(current, name, promise);
                    return promise;
                }
                if (current === current.parent) {
                    break;
                }
                current = current.parent;

            }

            return null;
        };
    }
});

doLittle.dependencyResolvers.DOMRootDependencyResolver = {
    canResolve: function (namespace, name) {
        return name === "DOMRoot";
    },

    resolve: function (namespace, name) {
        if (document.body != null && typeof document.body !== "undefined") {
            return document.body;
        }

        var promise = doLittle.execution.Promise.create();
        doLittle.dependencyResolvers.DOMRootDependencyResolver.promises.push(promise);
        return promise;
    }
};

doLittle.dependencyResolvers.DOMRootDependencyResolver.promises = [];
doLittle.dependencyResolvers.DOMRootDependencyResolver.documentIsReady = function () {
    doLittle.dependencyResolvers.DOMRootDependencyResolver.promises.forEach(function (promise) {
        promise.signal(document.body);
    });
};
doLittle.namespace("doLittle", {
    KnownArtifactTypesDependencyResolver: function () {
        var self = this;
        var supportedArtifacts = {
            readModelTypes: doLittle.read.ReadModelOf,
            commandTypes: doLittle.commands.Command,
            queryTypes: doLittle.read.Query
        };

        function isMoreSpecificNamespace(base, compareTo) {
            var isDeeper = false;
            var matchesbase = false;

            var baseParts = base.name.split(".");
            var compareToParts = compareTo.name.split(".");

            if (baseParts.length > compareToParts.length) {
                return false;
            }

            for (var i = 0; i < baseParts.length; i++) {
                if (baseParts[i] !== compareToParts[i]) {
                    return false;
                }
            }
            return true;
        }

        this.canResolve = function (namespace, name) {
            return name in supportedArtifacts;
        };

        this.resolve = function (namespace, name) {
            var type = supportedArtifacts[name];
            var extenders = type.getExtendersIn(namespace);
            var resolvedTypes = {};

            extenders.forEach(function (extender) {
                var name = extender._name;
                if (resolvedTypes[name] && !isMoreSpecificNamespace(resolvedTypes[name]._namespace, extender._namespace)) {
                    return;
                }

                resolvedTypes[name] = extender;
            });

            return resolvedTypes;
        };
    }
});
doLittle.namespace("doLittle", {
    KnownArtifactInstancesDependencyResolver: function () {
        var self = this;
        var supportedArtifacts = {
            readModels: doLittle.read.ReadModelOf,
            commands: doLittle.commands.Command,
            queries: doLittle.read.Query
        };

        function isMoreSpecificNamespace(base, compareTo) {
            var isDeeper = false;
            var matchesbase = false;

            var baseParts = base.name.split(".");
            var compareToParts = compareTo.name.split(".");

            if (baseParts.length > compareToParts.length) {
                return false;
            }

            for (var i = 0; i < baseParts.length; i++) {
                if (baseParts[i] !== compareToParts[i]) {
                    return false;
                }
            }
            return true;
        }

        this.canResolve = function (namespace, name) {
            return name in supportedArtifacts;
        };

        this.resolve = function (namespace, name) {
            var type = supportedArtifacts[name];
            var extenders = type.getExtendersIn(namespace);
            var resolvedTypes = {};

            extenders.forEach(function (extender) {
                var name = extender._name;
                if (resolvedTypes[name] && !isMoreSpecificNamespace(resolvedTypes[name]._namespace, extender._namespace)) {
                    return;
                }

                resolvedTypes[name] = extender;
            });

            var resolvedInstances = {};
            for (var prop in resolvedTypes) {
                resolvedInstances[prop] = resolvedTypes[prop].create();
            }

            return resolvedInstances;
        };
    }
});
doLittle.namespace("doLittle", {
    Guid : {
        create: function() {
            function S4() {
                return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
            }
            return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
        },
        empty: "00000000-0000-0000-0000-000000000000"
    }
});

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
doLittle.namespace("doLittle", {
    Singleton: function (typeDefinition) {
        return doLittle.Type.extend(typeDefinition).scopeTo(window);
    }
});
doLittle.namespace("doLittle.types", {
    TypeInfo: doLittle.Type.extend(function () {
        this.properties = [];
    })
});
doLittle.types.TypeInfo.createFrom = function (instance) {
    var typeInfo = doLittle.types.TypeInfo.create();
    var propertyInfo;
    for (var property in instance) {
        var value = instance[property];
        if (!doLittle.isNullOrUndefined(value)) {

            var type = value.constructor;

            if (!doLittle.isNullOrUndefined(instance[property]._type)) {
                type = instance[property]._type;
            }

            propertyInfo = doLittle.types.PropertyInfo.create({
                name: property,
                type: type
            });
        }
        typeInfo.properties.push(propertyInfo);
    }
    return typeInfo;
};

doLittle.namespace("doLittle.types", {
    PropertyInfo: doLittle.Type.extend(function (name, type) {
        this.name = name;
        this.type = type;
    })
});
doLittle.namespace("doLittle", {
    Path: doLittle.Type.extend(function (fullPath) {
        var self = this;

        // Based on node.js implementation : http://stackoverflow.com/questions/9451100/filename-extension-in-javascript
        var splitDeviceRe =
            /^([a-zA-Z]:|[\\\/]{2}[^\\\/]+[\\\/][^\\\/]+)?([\\\/])?([\s\S]*?)$/;

        // Regex to split the tail part of the above into [*, dir, basename, ext]
        var splitTailRe =
            /^([\s\S]+[\\\/](?!$)|[\\\/])?((?:\.{1,2}$|[\s\S]+?)?(\.[^.\/\\]*)?)$/;

        function removeUnsupportedParts(filename) {
            var queryStringStart = filename.indexOf("?");
            if (queryStringStart > 0) {
                filename = filename.substr(0, queryStringStart);
            }
            return filename;
        }

        function splitPath(filename) {
            // Separate device+slash from tail
            var result = splitDeviceRe.exec(filename),
                device = (result[1] || '') + (result[2] || ''),
                tail = result[3] || '';
            // Split the tail into dir, basename and extension
            var result2 = splitTailRe.exec(tail),
                dir = result2[1] || '',
                basename = result2[2] || '',
                ext = result2[3] || '';
            return [device, dir, basename, ext];
        }

        fullPath = removeUnsupportedParts(fullPath);
        var result = splitPath(fullPath);
        this.device = result[0] || "";
        this.directory = result[1] || "";
        this.filename = result[2] || "";
        this.extension = result[3] || "";
        this.filenameWithoutExtension = this.filename.replaceAll(this.extension, "");
        this.fullPath = fullPath;

        this.hasExtension = function () {
            if (doLittle.isNullOrUndefined(self.extension)) {
                return false;
            }
            if (self.extension === "") {
                return false;
            }
            return true;
        };
    })
});
doLittle.Path.makeRelative = function (fullPath) {
    if (fullPath.indexOf("/") === 0) {
        return fullPath.substr(1);
    }

    return fullPath;
};
doLittle.Path.getPathWithoutFilename = function (fullPath) {
    var lastIndex = fullPath.lastIndexOf("/");
    return fullPath.substr(0, lastIndex);
};
doLittle.Path.getFilename = function (fullPath) {
    var lastIndex = fullPath.lastIndexOf("/");
    return fullPath.substr(lastIndex+1);
};
doLittle.Path.getFilenameWithoutExtension = function (fullPath) {
    var filename = this.getFilename(fullPath);
    var lastIndex = filename.lastIndexOf(".");
    return filename.substr(0,lastIndex);
};
doLittle.Path.hasExtension = function (path) {
    if (path.indexOf("?") > 0) {
        path = path.substr(0, path.indexOf("?"));
    }
    var lastIndex = path.lastIndexOf(".");
    return lastIndex > 0;
};
doLittle.Path.changeExtension = function (fullPath, newExtension) {
    var path = doLittle.Path.create({ fullPath: fullPath });
    var newPath = path.directory + path.filenameWithoutExtension + "." + newExtension;
    return newPath;
};

doLittle.namespace("doLittle");

doLittle.DefinitionMustBeFunction = function (message) {
    this.prototype = Error.prototype;
    this.name = "DefinitionMustBeFunction";
    this.message = message || "Definition must be function";
};

doLittle.MissingName = function (message) {
    this.prototype = Error.prototype;
    this.name = "MissingName";
    this.message = message || "Missing name";
};

doLittle.Exception = (function(global, undefined) {
    function throwIfNameMissing(name) {
        if (!name || typeof name === "undefined") {
            throw new doLittle.MissingName();
        }
    }
    
    function throwIfDefinitionNotAFunction(definition) {
        if (typeof definition !== "function") {
            throw new doLittle.DefinitionMustBeFunction();
        }
    }

    function getExceptionName(name) {
        var lastDot = name.lastIndexOf(".");
        if (lastDot === -1 && lastDot !== name.length) {
            return name;
        }
        return name.substr(lastDot+1);
    }
    
    function defineAndGetTargetScope(name) {
        var lastDot = name.lastIndexOf(".");
        if( lastDot === -1 ) {
            return global;
        }
        
        var ns = name.substr(0,lastDot);
        doLittle.namespace(ns);
        
        var scope = global;
        var parts = ns.split('.');
        parts.forEach(function(part) {
            scope = scope[part];
        });
        
        return scope;
    }
    
    return {
        define: function(name, defaultMessage, definition) {
            throwIfNameMissing(name);
            
            var scope = defineAndGetTargetScope(name);
            var exceptionName = getExceptionName(name);
            
            var exception = function (message) {
                this.name = exceptionName;
                this.message = message || defaultMessage;
            };
            exception.prototype = Error.prototype;
            
            if( definition && typeof definition !== "undefined" ) {
                throwIfDefinitionNotAFunction(definition);
                
                definition.prototype = Error.prototype;
                exception.prototype = new definition();
            }
            
            scope[exceptionName] = exception;
        }
    };
})(window);
doLittle.namespace("doLittle");
doLittle.Exception.define("doLittle.LocationNotSpecified","Location was not specified");
doLittle.Exception.define("doLittle.InvalidUriFormat", "Uri format specified is not valid");
doLittle.Exception.define("doLittle.ObjectLiteralNotAllowed", "Object literal is not allowed");
doLittle.Exception.define("doLittle.MissingTypeDefinition", "Type definition was not specified");
doLittle.Exception.define("doLittle.AsynchronousDependenciesDetected", "You should consider using Type.beginCreate() or dependencyResolver.beginResolve() for systems that has asynchronous dependencies");
doLittle.Exception.define("doLittle.UnresolvedDependencies", "Some dependencies was not possible to resolve");
doLittle.namespace("doLittle");
doLittle.hashString = (function() {
    return {
        decode: function (a) {
            if (a === "") {
                return {};
            }
            a = a.replace("/?", "").split('&');

            var b = {};
            for (var i = 0; i < a.length; ++i) {
                var p = a[i].split('=', 2);
                if (p.length !== 2) {
                    continue;
                }

                var value = decodeURIComponent(p[1].replace(/\+/g, " "));

                if (value !== "" && !isNaN(value)) {
                    value = parseFloat(value);
                }

                b[p[0]] = value;
            }
            return b;
        }
    };
})();

doLittle.namespace("doLittle");
doLittle.Uri = (function(window, undefined) {
    /* parseUri JS v0.1, by Steven Levithan (http://badassery.blogspot.com)
    Splits any well-formed URI into the following parts (all are optional):
    ----------------------
    • source (since the exec() method returns backreference 0 [i.e., the entire match] as key 0, we might as well use it)
    • protocol (scheme)
    • authority (includes both the domain and port)
        • domain (part of the authority; can be an IP address)
        • port (part of the authority)
    • path (includes both the directory path and filename)
        • directoryPath (part of the path; supports directories with periods, and without a trailing backslash)
        • fileName (part of the path)
    • query (does not include the leading question mark)
    • anchor (fragment)
    */
    function parseUri(sourceUri){
        var uriPartNames = ["source","protocol","authority","domain","port","path","directoryPath","fileName","query","anchor"];
        var uriParts = new RegExp("^(?:([^:/?#.]+):)?(?://)?(([^:/?#]*)(?::(\\d*))?)?((/(?:[^?#](?![^?#/]*\\.[^?#/.]+(?:[\\?#]|$)))*/?)?([^?#/]*))?(?:\\?([^#]*))?(?:#(.*))?").exec(sourceUri);
        var uri = {};

        for (var i = 0; i < 10; i++){
            uri[uriPartNames[i]] = (uriParts[i] ? uriParts[i] : "");
        }

        // Always end directoryPath with a trailing backslash if a path was present in the source URI
        // Note that a trailing backslash is NOT automatically inserted within or appended to the "path" key
        if(uri.directoryPath.length > 0){
            uri.directoryPath = uri.directoryPath.replace(/\/?$/, "/");
        }

        return uri;
    }	
    
    
    function Uri(location) {
        var self = this;
        this.setLocation = function (location) {
            self.fullPath = location;
            location = location.replace("#!", "/");

            var result = parseUri(location);

            if (!result.protocol || typeof result.protocol === "undefined") {
                throw new doLittle.InvalidUriFormat("Uri ('" + location + "') was in the wrong format");
            }

            self.scheme = result.protocol;
            self.host = result.domain;
            self.path = result.path;
            self.anchor = result.anchor;

            self.queryString = result.query;
            self.port = parseInt(result.port);
            self.parameters = doLittle.hashString.decode(result.query);
            self.parameters = doLittle.extend(doLittle.hashString.decode(result.anchor), self.parameters);

            self.isSameAsOrigin = (window.location.protocol === result.protocol + ":" &&
                window.location.hostname === self.host);
        };
        
        this.setLocation(location);
    }
    
    function throwIfLocationNotSpecified(location) {
        if (!location || typeof location === "undefined") {
            throw new doLittle.LocationNotSpecified();
        }
    }
    
    
    return {
        create: function(location) {
            throwIfLocationNotSpecified(location);
        
            var uri = new Uri(location);
            return uri;
        },

        isAbsolute: function (url) {
            // Based on http://stackoverflow.com/questions/10687099/how-to-test-if-a-url-string-is-absolute-or-relative
            var expression = new RegExp('^(?:[a-z]+:)?//', 'i');
            return expression.test(url);
        }
    };
})(window);
doLittle.namespace("doLittle", {
    namespaces: doLittle.Singleton(function() {
        var self = this;

        this.stripPath = function (path) {
            if (path.startsWith("/")) {
                path = path.substr(1);
            }
            if (path.endsWith("/")) {
                path = path.substr(0, path.length - 1);
            }
            return path;
        };

        this.initialize = function () {
            var scripts = doLittle.assetsManager.getScripts();
            if (typeof scripts === "undefined") {
                return;
            }

            scripts.forEach(function (fullPath) {
                var path = doLittle.Path.getPathWithoutFilename(fullPath);
                path = self.stripPath(path);

                for (var mapperKey in doLittle.namespaceMappers) {
                    var mapper = doLittle.namespaceMappers[mapperKey];
                    if (typeof mapper.hasMappingFor === "function" && mapper.hasMappingFor(path)) {
                        var namespacePath = mapper.resolve(path);
                        var namespace = doLittle.namespace(namespacePath);

                        var root = "/" + path + "/";
                        namespace._path = root;

                        if (typeof namespace._scripts === "undefined") {
                            namespace._scripts = [];
                        }

                        var fileIndex = fullPath.lastIndexOf("/");
                        var file = fullPath.substr(fileIndex + 1);
                        var extensionIndex = file.lastIndexOf(".");
                        var system = file.substr(0, extensionIndex);

                        namespace._scripts.push(system);
                    }
                }
            });
        };
    })
});
doLittle.namespace("doLittle", {
    namespaceMappers: {

        mapPathToNamespace: function (path) {
            for (var mapperKey in doLittle.namespaceMappers) {
                var mapper = doLittle.namespaceMappers[mapperKey];
                if (typeof mapper.hasMappingFor === "function" && mapper.hasMappingFor(path)) {
                    var namespacePath = mapper.resolve(path);
                    return namespacePath;
                }
            }

            return null;
        }
    }
});
doLittle.namespace("doLittle", {
    StringMapping: doLittle.Type.extend(function (format, mappedFormat) {
        var self = this;

        this.format = format;
        this.mappedFormat = mappedFormat;

        var placeholderExpression = "{[a-zA-Z]+}";
        var placeholderRegex = new RegExp(placeholderExpression, "g");

        var wildcardExpression = "\\*{2}[//||.]";
        var wildcardRegex = new RegExp(wildcardExpression, "g");

        var combinedExpression = "(" + placeholderExpression + ")*(" + wildcardExpression + ")*";
        var combinedRegex = new RegExp(combinedExpression, "g");

        var components = [];

        var resolveExpression = format.replace(combinedRegex, function(match) {
            if (typeof match === "undefined" || match === "") {
                return "";
            }
            components.push(match);
            if (match.indexOf("**") === 0) {
                return "([\\w.//]*)";
            }
            return "([\\w.]*)";
        });

        var mappedFormatWildcardMatch = mappedFormat.match(wildcardRegex);
        var formatRegex = new RegExp(resolveExpression);

        this.matches = function (input) {
            var match = input.match(formatRegex);
            if (match) {
                return true;
            }
            return false;
        };

        this.getValues = function (input) {
            var output = {};
            var match = input.match(formatRegex);
            components.forEach(function (c, i) {
                var component = c.substr(1, c.length - 2);
                var value = match[i + 2];
                if (c.indexOf("**") !== 0) {
                    output[component] = value;
                }
            });

            return output;
        };

        this.resolve = function (input) {
            var match = input.match(formatRegex);
            var result = mappedFormat;
            var wildcardOffset = 0;

            components.forEach(function (c, i) {
                var value = match[i + 1];
                if (c.indexOf("**") === 0) {
                    var wildcard = mappedFormatWildcardMatch[wildcardOffset];
                    value = value.replaceAll(c[2], wildcard[2]);
                    result = result.replace(wildcard, value);
                    wildcardOffset++;
                } else {
                    result = result.replace(c, value);
                }
            });

            return result;
        };
    })
});
doLittle.namespace("doLittle", {
    stringMappingFactory: doLittle.Singleton(function () {

        this.create = function (format, mappedFormat) {
            var mapping = doLittle.StringMapping.create({
                format: format,
                mappedFormat: mappedFormat
            });
            return mapping;
        };
    })
});
doLittle.namespace("doLittle", {
    StringMapper: doLittle.Type.extend(function (stringMappingFactory) {
        var self = this;

        this.stringMappingFactory = stringMappingFactory;

        this.mappings = [];

        this.reverseMappings = [];

        function hasMappingFor(mappings, input) {
            var found = false;
            mappings.some(function (m) {
                if (m.matches(input)) {
                    found = true;
                }
                return found;
            });
            return found;
        }

        function getMappingFor(mappings, input) {
            var found;
            mappings.some(function (m) {
                if (m.matches(input)) {
                    found = m;
                    return true;
                }
            });

            if (typeof found !== "undefined") {
                return found;
            }

            throw {
                name: "ArgumentError",
                message: "String mapping for (" + input + ") could not be found"
            };
        }

        function resolve(mappings, input) {
            try {
                if (input === null || typeof input === "undefined") {
                    return "";
                }
                
                var mapping = self.getMappingFor(input);
                if (doLittle.isNullOrUndefined(mapping)) {
                    return "";
                }

                return mapping.resolve(input);
            } catch (e) {
                return "";
            }
        }

        this.hasMappingFor = function (input) {
            return hasMappingFor(self.mappings, input);
        };
        this.getMappingFor = function (input) {
            return getMappingFor(self.mappings, input);
        };
        this.resolve = function (input) {
            return resolve(self.mappings, input);
        };

        this.reverse = {
            hasMappingFor: function (input) {
                return self.hasMappingFor(self.reverseMappings, input);
            },

            getMappingFor: function (input) {
                return self.getMappingFor(self.reverseMappings, input);
            },

            resolve: function (input) {
                return self.resolve(self.reverseMappings, input);
            }
        };

        this.addMapping = function (format, mappedFormat) {
            var mapping = self.stringMappingFactory.create(format, mappedFormat);
            self.mappings.push(mapping);

            var reverseMapping = self.stringMappingFactory.create(mappedFormat, format);
            self.reverseMappings.push(reverseMapping);
        };
    })
});
doLittle.namespace("doLittle", {
    uriMappers: {
    }
});
doLittle.namespace("doLittle", {
    server: doLittle.Singleton(function () {
        var self = this;

        this.target = "";

        function deserialize(data) {
            if (doLittle.isArray(data)) {
                var items = [];
                data.forEach(function (item) {
                    items.push(deserialize(item));
                });
                return items;
            } else {
                for (var property in data) {
                    if (doLittle.isArray(data[property])) {
                        data[property] = deserialize(data[property]);
                    } else {
                        var value = data[property];

                        if (doLittle.isNumber(value)) {
                            data[property] = parseFloat(value);
                        } else {
                            data[property] = data[property];
                        }
                    }
                }
                return data;
            }
        }


        this.post = function (url, parameters) {
            var promise = doLittle.execution.Promise.create();

            if (!doLittle.Uri.isAbsolute(url)) {
                url = self.target + url;
            }

            var actualParameters = {};

            for (var property in parameters) {
                actualParameters[property] = JSON.stringify(parameters[property]);
            }

            $.ajax({
                url: url,
                type: "POST",
                dataType: 'json',
                data: JSON.stringify(actualParameters),
                contentType: 'application/json; charset=utf-8',
                complete: function (result) {
                    var data = $.parseJSON(result.responseText);
                    deserialize(data);
                    promise.signal(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    promise.fail(jqXHR);
                }
            });

            return promise;
        };

        this.get = function (url, parameters) {
            var promise = doLittle.execution.Promise.create();

            if (!doLittle.Uri.isAbsolute(url)) {
                url = self.target + url;
            }

            if (doLittle.isObject(parameters)) {
                for (var parameterName in parameters) {
                    if (doLittle.isArray(parameters[parameterName])) {
                        parameters[parameterName] = JSON.stringify(parameters[parameterName]);
                    }
                }
            }

            $.ajax({
                url: url,
                type: "GET",
                dataType: 'json',
                data: parameters,
                contentType: 'application/json; charset=utf-8',
                complete: function (result, textStatus) {
                    var data = $.parseJSON(result.responseText);
                    deserialize(data);
                    promise.signal(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    promise.fail(jqXHR);
                }
            });

            return promise;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.server = doLittle.server;
doLittle.namespace("doLittle", {
    areEqual: function (source, target) {
        function isReservedMemberName(member) {
            return member.indexOf("_") >= 0 || member === "model" || member === "commons" || member === "targetViewModel" || member === "region";
        }

        if (ko.isObservable(source)) {
            source = source();
        }
        if (ko.isObservable(target)) {
            target = target();
        }

        if (doLittle.isNullOrUndefined(source) && doLittle.isNullOrUndefined(target)) {
            return true;
        }

        if (doLittle.isNullOrUndefined(source)) {
            return false;
        }
        if (doLittle.isNullOrUndefined(target)) {
            return false;
        }

        if (doLittle.isArray(source) && doLittle.isArray(target)) {
            if (source.length !== target.length) {
                return false;
            }

            for (var index = 0; index < source.length; index++) {
                if (doLittle.areEqual(source[index], target[index]) === false) {
                    return false;
                }
            }
        } else {
            for (var member in source) {
                if (isReservedMemberName(member)) {
                    continue;
                }
                if (target.hasOwnProperty(member)) {
                    var sourceValue = source[member];
                    var targetValue = target[member];

                    if (doLittle.isObject(sourceValue) ||
                        doLittle.isArray(sourceValue) ||
                        ko.isObservable(sourceValue)) {

                        if (!doLittle.areEqual(sourceValue, targetValue)) {
                            return false;
                        }
                    } else {
                        if (sourceValue !== targetValue) {
                            return false;
                        }
                    }
                } else {
                    return false;
                }
            }
        }

        return true;
    }
});
doLittle.namespace("doLittle", {
    deepClone: function (source, target) {
        function isReservedMemberName(member) {
            return member.indexOf("_") >= 0 || member === "model" || member === "commons" || member === "targetViewModel" || member === "region";
        }

        if (ko.isObservable(source)) {
            source = source();
        }

        if (target == null) {
            if (doLittle.isArray(source)) {
                target = [];
            } else {
                target = {};
            }
        }

        var sourceValue;
        if (doLittle.isArray(source)) {
            for (var index = 0; index < source.length; index++) {
                sourceValue = source[index];
                var clonedValue = doLittle.deepClone(sourceValue);
                target.push(clonedValue);
            }
        } else {
            for (var member in source) {
                if (isReservedMemberName(member)) {
                    continue;
                }

                sourceValue = source[member];

                if (ko.isObservable(sourceValue)) {
                    sourceValue = sourceValue();
                }

                if (doLittle.isFunction(sourceValue)) {
                    continue;
                }

                var targetValue = null;
                if (doLittle.isObject(sourceValue)) {
                    targetValue = {};
                } else if (doLittle.isArray(sourceValue)) {
                    targetValue = [];
                } else {
                    target[member] = sourceValue;
                }

                if (targetValue != null) {
                    target[member] = targetValue;
                    doLittle.deepClone(sourceValue, targetValue);
                }
            }
        }

        return target;
    }
});

doLittle.namespace("doLittle", {
    systemClock: doLittle.Singleton(function () {
        this.nowInMilliseconds = function () {
            return window.performance.now();
        };
    })
});
/*!
* JavaScript TimeSpan Library
*
* Copyright (c) 2010 Michael Stum, http://www.Stum.de/
* 
* Permission is hereby granted, free of charge, to any person obtaining
* a copy of this software and associated documentation files (the
* "Software"), to deal in the Software without restriction, including
* without limitation the rights to use, copy, modify, merge, publish,
* distribute, sublicense, and/or sell copies of the Software, and to
* permit persons to whom the Software is furnished to do so, subject to
* the following conditions:
* 
* The above copyright notice and this permission notice shall be
* included in all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
* MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
* NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
* LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
* OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
* WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
doLittle.namespace("doLittle", {
    // Constructor function, all parameters are optional
    TimeSpan : function (milliseconds, seconds, minutes, hours, days) {
        var version = "1.2",
            // Millisecond-constants
            msecPerSecond = 1000,
            msecPerMinute = 60000,
            msecPerHour = 3600000,
            msecPerDay = 86400000,
            // Internally we store the TimeSpan as Milliseconds
            msecs = 0,

            // Helper functions
            isNumeric = function (input) {
                return !isNaN(parseFloat(input)) && isFinite(input);
            };

        // Constructor Logic
        if (isNumeric(days)) {
            msecs += (days * msecPerDay);
        }
        if (isNumeric(hours)) {
            msecs += (hours * msecPerHour);
        }
        if (isNumeric(minutes)) {
            msecs += (minutes * msecPerMinute);
        }
        if (isNumeric(seconds)) {
            msecs += (seconds * msecPerSecond);
        }
        if (isNumeric(milliseconds)) {
            msecs += milliseconds;
        }

        // Addition Functions
        this.addMilliseconds = function (milliseconds) {
            if (!isNumeric(milliseconds)) {
                return;
            }
            msecs += milliseconds;
        };
        this.addSeconds = function (seconds) {
            if (!isNumeric(seconds)) {
                return;
            }
            msecs += (seconds * msecPerSecond);
        };
        this.addMinutes = function (minutes) {
            if (!isNumeric(minutes)) {
                return;
            }
            msecs += (minutes * msecPerMinute);
        };
        this.addHours = function (hours) {
            if (!isNumeric(hours)) {
                return;
            }
            msecs += (hours * msecPerHour);
        };
        this.addDays = function (days) {
            if (!isNumeric(days)) {
                return;
            }
            msecs += (days * msecPerDay);
        };

        // Subtraction Functions
        this.subtractMilliseconds = function (milliseconds) {
            if (!isNumeric(milliseconds)) {
                return;
            }
            msecs -= milliseconds;
        };
        this.subtractSeconds = function (seconds) {
            if (!isNumeric(seconds)) {
                return;
            }
            msecs -= (seconds * msecPerSecond);
        };
        this.subtractMinutes = function (minutes) {
            if (!isNumeric(minutes)) {
                return;
            }
            msecs -= (minutes * msecPerMinute);
        };
        this.subtractHours = function (hours) {
            if (!isNumeric(hours)) {
                return;
            }
            msecs -= (hours * msecPerHour);
        };
        this.subtractDays = function (days) {
            if (!isNumeric(days)) {
                return;
            }
            msecs -= (days * msecPerDay);
        };

        // Functions to interact with other TimeSpans
        this.isTimeSpan = true;
        this.add = function (otherTimeSpan) {
            if (!otherTimeSpan.isTimeSpan) {
                return;
            }
            msecs += otherTimeSpan.totalMilliseconds();
        };
        this.subtract = function (otherTimeSpan) {
            if (!otherTimeSpan.isTimeSpan) {
                return;
            }
            msecs -= otherTimeSpan.totalMilliseconds();
        };
        this.equals = function (otherTimeSpan) {
            if (!otherTimeSpan.isTimeSpan) {
                return;
            }
            return msecs === otherTimeSpan.totalMilliseconds();
        };

        // Getters
        this.totalMilliseconds = function (roundDown) {
            var result = msecs;
            if (roundDown === true) {
                result = Math.floor(result);
            }
            return result;
        };
        this.totalSeconds = function (roundDown) {
            var result = msecs / msecPerSecond;
            if (roundDown === true) {
                result = Math.floor(result);
            }
            return result;
        };
        this.totalMinutes = function (roundDown) {
            var result = msecs / msecPerMinute;
            if (roundDown === true) {
                result = Math.floor(result);
            }
            return result;
        };
        this.totalHours = function (roundDown) {
            var result = msecs / msecPerHour;
            if (roundDown === true) {
                result = Math.floor(result);
            }
            return result;
        };
        this.totalDays = function (roundDown) {
            var result = msecs / msecPerDay;
            if (roundDown === true) {
                result = Math.floor(result);
            }
            return result;
        };
        // Return a Fraction of the TimeSpan
        this.milliseconds = function () {
            return msecs % 1000;
        };
        this.seconds = function () {
            return Math.floor(msecs / msecPerSecond) % 60;
        };
        this.minutes = function () {
            return Math.floor(msecs / msecPerMinute) % 60;
        };
        this.hours = function () {
            return Math.floor(msecs / msecPerHour) % 24;
        };
        this.days = function () {
            return Math.floor(msecs / msecPerDay);
        };

        // Misc. Functions
        this.getVersion = function () {
            return version;
        };
    }
});

// "Static Constructors"
doLittle.TimeSpan.zero = function() {
    return new doLittle.TimeSpan(0, 0, 0, 0, 0);
};
doLittle.TimeSpan.fromMilliseconds = function (milliseconds) {
    return new doLittle.TimeSpan(milliseconds, 0, 0, 0, 0);
};
doLittle.TimeSpan.fromSeconds = function (seconds) {
    return new doLittle.TimeSpan(0, seconds, 0, 0, 0);
};
doLittle.TimeSpan.fromMinutes = function (minutes) {
    return new doLittle.TimeSpan(0, 0, minutes, 0, 0);
};
doLittle.TimeSpan.fromHours = function (hours) {
    return new doLittle.TimeSpan(0, 0, 0, hours, 0);
};
doLittle.TimeSpan.fromDays = function (days) {
    return new doLittle.TimeSpan(0, 0, 0, 0, days);
};
doLittle.TimeSpan.fromDates = function (firstDate, secondDate, forcePositive) {
    var differenceMsecs = secondDate.valueOf() - firstDate.valueOf();
    if (forcePositive === true) {
        differenceMsecs = Math.abs(differenceMsecs);
    }
    return new doLittle.TimeSpan(differenceMsecs, 0, 0, 0, 0);
};

doLittle.namespace("doLittle", {
    Event: doLittle.Type.extend(function () {
        var subscribers = [];

        this.subscribe = function (subscriber) {
            subscribers.push(subscriber);
        };

        this.trigger = function (data) {
            subscribers.forEach(function (subscriber) {
                subscriber(data);
            });
        };
    })
});
doLittle.namespace("doLittle", {
    systemEvents: doLittle.Singleton(function () {
        this.readModels = doLittle.read.readModelSystemEvents.create();
        this.commands = doLittle.commands.commandEvents.create();
    })
});
doLittle.WellKnownTypesDependencyResolver.types.systemEvents = doLittle.systemEvents;
doLittle.namespace("doLittle", {
    dispatcher: doLittle.Singleton(function () {
        this.schedule = function (milliseconds, callback) {
            setTimeout(callback, milliseconds);
        };
    })
});
ko.extenders.linked = function (target, options) {
    function setupValueSubscription(value) {
        if (ko.isObservable(value)) {
            var subscription = value.subscribe(function () {
                target.valueHasMutated();
            });
            target._previousLinkedSubscription = subscription;
        }
    }

    target.subscribe(function (newValue) {
        if (target._previousLinkedSubscription) {
            target._previousLinkedSubscription.dispose();
        }
        setupValueSubscription(newValue);

    });

    var currentValue = target();
    setupValueSubscription(currentValue);
};
doLittle.namespace("doLittle.hubs", {
    hubConnection: doLittle.Singleton(function () {
        var self = this;
        var hub = $.hubConnection("/signalr", { useDefaultPath: false });
        /* jshint ignore:start */
        $.signalR.hub = hub;
        /* jshint ignore:end */

        this.isConnected = false;
        this.connected = doLittle.Event.create();

        this.createProxy = function (hubName) {
            var proxy = hub.createHubProxy(hubName);
            return proxy;
        };

        //$.connection.hub.logging = true;
        $.connection.hub.start().done(function () {
            console.log("Hub connection up and running");
            self.isConnected = true;
            self.connected.trigger();
        });
    })
});
doLittle.WellKnownTypesDependencyResolver.types.hubConnection = doLittle.hubs.hubConnection;
doLittle.namespace("doLittle.hubs", {
    Hub: doLittle.Type.extend(function (hubConnection) {
        var self = this;

        var proxy = null;
        this._name = "";

        function makeClientProxyFunction(callback) {
            return function () {
                callback.apply(self, arguments);
            };
        }

        this.client = function (callback) {
            var client = {};
            callback(client);

            for (var property in client) {
                var value = client[property];
                if (!doLittle.isFunction(value)) {
                    continue;
                }

                proxy.on(property, makeClientProxyFunction(value));
            }
        };

        this.server = {};

        var delayedServerInvocations = [];

        hubConnection.connected.subscribe(function () {
            delayedServerInvocations.forEach(function (invocationFunction) {
                invocationFunction();
            });
        });

        function makeInvocationFunction(promise, method, args) {
            return function () {
                var argumentsAsArray = [];
                for (var arg = 0; arg < args.length; arg++) {
                    argumentsAsArray.push(args[arg]);
                }

                var allArguments = [method].concat(argumentsAsArray);
                proxy.invoke.apply(proxy, allArguments).done(function (result) {
                    promise.signal(result);
                });
            };
        }

        this.invokeServerMethod = function (method, args) {
            var promise = doLittle.execution.Promise.create();

            var invocationFunction = makeInvocationFunction(promise, method, args);

            if (hubConnection.isConnected === false) {
                delayedServerInvocations.push(invocationFunction);
            } else {
                invocationFunction();
            }

            return promise;
        };

        this.onCreated = function (lastDescendant) {
            proxy = hubConnection.createProxy(lastDescendant._name);
        };
    })
});

doLittle.dependencyResolvers.hub = {
    canResolve: function (namespace, name) {
        if (typeof hubs !== "undefined") {
            return name in hubs;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return hubs[name].create();
    }
};
doLittle.namespace("doLittle.io", {
    fileType: {
        unknown: 0,
        text: 1,
        javaScript: 2,
        html: 3
    }
});
doLittle.namespace("doLittle.io", {
    File: doLittle.Type.extend(function (path) {
        /// <summary>Represents a file</summary>

        /// <field name="type" type="doLittle.io.fileType">Type of file represented</field>
        this.type = doLittle.io.fileType.unknown;

        /// <field name="path" type="doLittle.Path">The path representing the file</field>
        this.path = doLittle.Path.create({ fullPath: path });
    })
});
doLittle.namespace("doLittle.io", {
    fileFactory: doLittle.Singleton(function () {
        /// <summary>Represents a factory for creating instances of doLittle.io.File</summary>
        this.create = function (path, fileType) {
            /// <summary>Creates a new file</summary>
            /// <param name="path" type="String">Path of file</param>
            /// <param name="fileType" type="doLittle.io.fileType" optional="true">Type of file to use</param>
            /// <returns type="doLittle.io.File">An instance of a file</returns>

            var file = doLittle.io.File.create({ path: path });
            if (!doLittle.isNullOrUndefined(fileType)) {
                file.fileType = fileType;
            }
            return file;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.fileFactory = doLittle.io.fileFactory;
doLittle.namespace("doLittle.io", {
    fileManager: doLittle.Singleton(function () {
        /// <summary>Represents a manager for files, providing capabilities of loading and more</summary>
        var self = this;

        var uri = doLittle.Uri.create(window.location.href);
        if (window.location.protocol === "file:") {
            this.origin = window.location.href;
            this.origin = this.origin.substr(0, this.origin.lastIndexOf("/"));

            if (this.origin.lastIndexOf("/") === this.origin.length - 1) {
                this.origin = this.origin.substr(0, this.origin.length - 1);
            }
        } else {
            var port = uri.port || "";
            if (!doLittle.isUndefined(port) && port !== "" && port !== 80) {
                port = ":" + port;
            }

            this.origin = uri.scheme + "://" + uri.host + port;
        }

        function getActualFilename(filename) {
            var actualFilename = self.origin;

            if (filename.indexOf("/") !== 0) {
                actualFilename += "/";
            }
            actualFilename += filename;

            return actualFilename;
        }

        this.load = function (files) {
            /// <summary>Load files</summary>
            /// <param parameterArray="true" elementType="doLittle.io.File">Files to load</param>
            /// <returns type="doLittle.execution.Promise">A promise that can be continued with the actual files coming in as an array</returns>
            var filesToLoad = [];

            var promise = doLittle.execution.Promise.create();

            files.forEach(function (file) {
                var path = getActualFilename(file.path.fullPath);
                if (file.fileType === doLittle.io.fileType.html) {
                    path = "text!" + path + "!strip";
                    if (!file.path.hasExtension()) {
                        path = "noext!" + path;
                    }
                }

                filesToLoad.push(path);
            });

            require(filesToLoad, function () {
                promise.signal(arguments);
            });

            return promise;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.fileManager = doLittle.io.fileManager;
doLittle.namespace("doLittle.specifications", {
    Specification: doLittle.Type.extend(function () {
        /// <summary>Represents a rule based on the specification pattern</summary>
        var self = this;
        var currentInstance = ko.observable();

        /// <field name="evaluator">
        /// Holds the evaluator to be used to evaluate wether or not the rule is satisfied
        /// </field>
        /// <remarks>
        /// The evaluator can either be a function that gets called with the instance
        /// or an observable. The observable not being a regular function will obviously
        /// not have the instance passed 
        /// </remarks>
        this.evaluator = null;

        /// <field name="isSatisfied">Observable that holds the result of any evaluations being done</field>
        /// <remarks>
        /// Due to its nature of being an observable, it will re-evaluate if the evaluator
        /// is an observable and its state changes.
        /// </remarks>
        this.isSatisfied = ko.computed(function () {
            if (ko.isObservable(self.evaluator)) {
                return self.evaluator();
            }
            var instance = currentInstance();

            if (!doLittle.isNullOrUndefined(instance)) {
                return self.evaluator(instance);
            }
            return false;
        });

        this.evaluate = function (instance) {
            /// <summary>Evaluates the rule</summary>
            /// <param name="instance">Object instance used during evaluation</param>
            /// <returns>True if satisfied, false if not</returns>
            currentInstance(instance);

            return self.isSatisfied();
        };

        this.and = function (rule) {
            /// <summary>Takes this rule and "ands" it together with another rule</summary>
            /// <param name="rule">
            /// This can either be the instance of another specific rule, 
            /// or an evaluator that can be used directly by the default rule implementation
            /// </param>
            /// <returns>A new composed rule</returns>

            if (doLittle.isFunction(rule)) {
                var oldRule = rule;
                rule = doLittle.specifications.Specification.create();
                rule.evaluator = oldRule;
            }

            var and = doLittle.specifications.And.create(this, rule);
            return and;
        };

        this.or = function (rule) {
            /// <summary>Takes this rule and "ors" it together with another rule</summary>
            /// <param name="rule">
            /// This can either be the instance of another specific rule, 
            /// or an evaluator that can be used directly by the default rule implementation
            /// </param>
            /// <returns>A new composed rule</returns>

            if (doLittle.isFunction(rule)) {
                var oldRule = rule;
                rule = doLittle.specifications.Specification.create();
                rule.evaluator = oldRule;
            }

            var or = doLittle.specifications.Or.create(this, rule);
            return or;
        };
    })
});
doLittle.specifications.Specification.when = function (evaluator) {
    /// <summary>Starts a rule chain</summary>
    /// <param name="evaluator">
    /// The evaluator can either be a function that gets called with the instance
    /// or an observable. The observable not being a regular function will obviously
    /// not have the instance passed 
    /// </param>
    /// <returns>A new composed rule</returns>
    var rule = doLittle.specifications.Specification.create();
    rule.evaluator = evaluator;
    return rule;
};
doLittle.namespace("doLittle.specifications", {
    And: doLittle.specifications.Specification.extend(function (leftHandSide, rightHandSide) {
        /// <summary>Represents the "and" composite rule based on the specification pattern</summary>

        this.isSatisfied = ko.computed(function () {
            return leftHandSide.isSatisfied() &&
                rightHandSide.isSatisfied();
        });

        this.evaluate = function (instance) {
            leftHandSide.evaluate(instance);
            rightHandSide.evaluate(instance);
        };
    })
});
doLittle.namespace("doLittle.specifications", {
    Or: doLittle.specifications.Specification.extend(function (leftHandSide, rightHandSide) {
        /// <summary>Represents the "or" composite rule based on the specification pattern</summary>

        this.isSatisfied = ko.computed(function () {
            return leftHandSide.isSatisfied() ||
                rightHandSide.isSatisfied();
        });

        this.evaluate = function (instance) {
            leftHandSide.evaluate(instance);
            rightHandSide.evaluate(instance);
        };
    })
});
doLittle.namespace("doLittle.tasks", {
    Task: doLittle.Type.extend(function () {
        /// <summary>Represents a task that can be done in the system</summary>
        var self = this;

        /// <field name="errors" type="observableArray">Observable array of errors</field>
        this.errors = ko.observableArray();

        /// <field name="isExceuting" type="boolean">True / false for telling wether or not the task is executing or not</field>
        this.isExecuting = ko.observable(false);

        this.execute = function () {
            /// <summary>Executes the task</summary>
            /// <returns>A promise</returns>
            var promise = doLittle.execution.Promise.create();
            promise.signal();
            return promise;
        };

        this.reportError = function (error) {
            /// <summary>Report an error from executing the task</summary>
            /// <param name="error" type="String">Error coming back</param>
            self.errors.push(error);
        };
    })
});
doLittle.namespace("doLittle.tasks", {
    TaskHistoryEntry: doLittle.Type.extend(function () {
        var self = this;

        this.type = "";
        this.content = "";

        this.begin = ko.observable();
        this.end = ko.observable();
        this.total = ko.computed(function () {
            if (!doLittle.isNullOrUndefined(self.end()) &&
                !doLittle.isNullOrUndefined(self.begin())) {
                return self.end() - self.begin();
            }
            return 0;
        });
        this.result = ko.observable();
        this.error = ko.observable();

        this.isFinished = ko.computed(function () {
            return !doLittle.isNullOrUndefined(self.end());
        });
        this.hasFailed = ko.computed(function () {
            return !doLittle.isNullOrUndefined(self.error());
        });

        this.isSuccess = ko.computed(function () {
            return self.isFinished() && !self.hasFailed();
        });
    })
});
doLittle.namespace("doLittle.tasks", {
    taskHistory: doLittle.Singleton(function (systemClock) {
        /// <summary>Represents the history of tasks that has been executed since the start of the application</summary>
        var self = this;

        var entriesById = {};

        /// <field param="entries" type="observableArray">Observable array of entries</field>
        this.entries = ko.observableArray();

        this.begin = function (task) {
            var id = doLittle.Guid.create();

            try {
                var entry = doLittle.tasks.TaskHistoryEntry.create();

                entry.type = task._type._name;

                var content = {};

                for (var property in task) {
                    if (property.indexOf("_") !== 0 && task.hasOwnProperty(property) && typeof task[property] !== "function") {
                        content[property] = task[property];
                    }
                }

                entry.content = JSON.stringify(content);

                entry.begin(systemClock.nowInMilliseconds());
                entriesById[id] = entry;
                self.entries.push(entry);
            } catch (ex) {
                // Todo: perfect place for logging something
            }
            return id;
        };

        this.end = function (id, result) {
            if (entriesById.hasOwnProperty(id)) {
                var entry = entriesById[id];
                entry.end(systemClock.nowInMilliseconds());
                entry.result(result);
            }
        };

        this.failed = function (id, error) {
            if (entriesById.hasOwnProperty(id)) {
                var entry = entriesById[id];
                entry.end(systemClock.nowInMilliseconds());
                entry.error(error);
            }
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.taskHistory = doLittle.tasks.taskHistory;
doLittle.namespace("doLittle.tasks", {
    Tasks: doLittle.Type.extend(function (taskHistory) {
        /// <summary>Represents an aggregation of tasks</summary>
        var self = this;

        /// <field name="unfiltered" type="doLittle.tasks.Task[]">All tasks completely unfiltered</field>
        this.unfiltered = ko.observableArray();

        /// <field name="executeWhen" type="doLittle.specifications.Specification">Gets or sets the rule for execution</field>
        /// <remarks>
        /// If a task gets executed that does not get satisfied by the rule, it will just queue it up
        /// </remarks>
        this.canExecuteWhen = ko.observable();

        /// <field name="all" type="doLittle.tasks.Task[]">All tasks being executed</field>
        this.all = ko.computed(function () {
            var all = self.unfiltered();

            var rule = self.canExecuteWhen();

            if (!doLittle.isNullOrUndefined(rule)) {
                var filtered = [];

                all.forEach(function (task) {
                    rule.evaluate(task);
                    if (rule.isSatisfied() === true) {
                        filtered.push(task);
                    }
                });
                return filtered;
            }

            return all;
        });

        /// <field name="errors" type="observableArrayOfString">All errors that occured during execution of the task</field>
        this.errors = ko.observableArray();

        /// <field name="isBusy" type="Boolean">Returns true if the system is busy working, false if not</field>
        this.isBusy = ko.computed(function () {
            return self.all().length > 0;
        });

        function executeTaskIfNotExecuting(task) {
            if (task.isExecuting() === true) {
                return;
            }
            task.isExecuting(true);
            var taskHistoryId = taskHistory.begin(task);

            task.execute().continueWith(function (result) {
                self.unfiltered.remove(task);
                taskHistory.end(taskHistoryId, result);
                task.promise.signal(result);
            }).onFail(function (error) {
                self.unfiltered.remove(task);
                self.errors.push(task);
                taskHistory.failed(taskHistoryId, error);
                task.promise.fail(error);
            });
        }

        this.all.subscribe(function (changedTasks) {
            changedTasks.forEach(function (task) {
                executeTaskIfNotExecuting(task);
            });
        });

        this.execute = function (task) {
            /// <summary>Adds a task and starts executing it right away</summary>
            /// <param name="task" type="doLittle.tasks.Task">Task to add</summary>
            /// <returns>A promise to work with for chaining further events</returns>

            task.promise = doLittle.execution.Promise.create();
            self.unfiltered.push(task);
            
            var rule = self.canExecuteWhen();
            var canExecute = true;
            if (!doLittle.isNullOrUndefined(rule)) {
                canExecute = rule.evaluate(task);
            }
            
            if (canExecute === true) {
                executeTaskIfNotExecuting(task);
            }
            
            return task.promise;
        };
    })
});
doLittle.namespace("doLittle.tasks", {
    tasksFactory: doLittle.Singleton(function () {
        this.create = function () {
            var tasks = doLittle.tasks.Tasks.create();
            return tasks;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.tasksFactory = doLittle.tasks.tasksFactory;
doLittle.namespace("doLittle.tasks", {
    HttpGetTask: doLittle.tasks.Task.extend(function (server, url, payload) {
        /// <summary>Represents a task that can perform Http Get requests</summary>

        this.execute = function () {
            var promise = doLittle.execution.Promise.create();
            server
                .get(url, payload)
                    .continueWith(function (result) {
                        promise.signal(result);
                    })
                    .onFail(function (error) {
                        promise.fail(error);
                    });
            return promise;
        };
    })
});
doLittle.namespace("doLittle.tasks", {
    HttpPostTask: doLittle.tasks.Task.extend(function (server, url, payload) {
        /// <summary>Represents a task that can perform a Http Post request</summary>

        this.execute = function () {
            var promise = doLittle.execution.Promise.create();

            server
                .post(url, payload)
                    .continueWith(function (result) {
                        promise.signal(result);
                    })
                    .onFail(function (error) {
                        promise.fail(error);
                    });
            return promise;
        };
    })
});
doLittle.namespace("doLittle.tasks", {
    LoadTask: doLittle.tasks.Task.extend(function () {
        /// <summary>Represents a base task that represents anything that is loading things</summary>
        this.execute = function () {
            var promise = doLittle.execution.Promise.create();
            promise.signal();
            return promise;
        };
    })
});
doLittle.namespace("doLittle.tasks", {
    FileLoadTask: doLittle.tasks.LoadTask.extend(function (files, fileManager) {
        /// <summary>Represents a task for loading view related files asynchronously</summary>
        this.files = files;

        var self = this;

        this.files = [];
        files.forEach(function (file) {
            self.files.push(file.path.fullPath);
        });

        this.execute = function () {
            var promise = doLittle.execution.Promise.create();

            fileManager.load(files).continueWith(function (instances) {
                promise.signal(instances);
            });
            return promise;
        };
    })
});
doLittle.namespace("doLittle.tasks", {
    ExecutionTask: doLittle.tasks.Task.extend(function () {
        /// <summary>Represents a base task that represents anything that is executing</summary>
        this.execute = function () {
        };
    })
});
doLittle.namespace("doLittle", {
    taskFactory: doLittle.Singleton(function () {
        this.createHttpPost = function (url, payload) {
            var task = doLittle.tasks.HttpPostTask.create({
                url: url,
                payload: payload
            });
            return task;
        };

        this.createHttpGet = function (url, payload) {
            var task = doLittle.tasks.HttpGetTask.create({
                url: url,
                payload: payload
            });
            return task;
        };

        this.createQuery = function (query, paging) {
            var task = doLittle.read.QueryTask.create({
                query: query,
                paging: paging
            });
            return task;
        };

        this.createReadModel = function (readModelOf, propertyFilters) {
            var task = doLittle.read.ReadModelTask.create({
                readModelOf: readModelOf,
                propertyFilters: propertyFilters
            });
            return task;
        };

        this.createHandleCommand = function (command) {
            var task = doLittle.commands.HandleCommandTask.create({
                command: command
            });
            return task;
        };

        this.createHandleCommands = function (commands) {
            var task = doLittle.commands.HandleCommandsTask.create({
                commands: commands
            });
            return task;
        };

        this.createViewLoad = function (files) {
            var task = doLittle.views.ViewLoadTask.create({
                files: files
            });
            return task;
        };

        this.createViewModelLoad = function (files) {
            var task = doLittle.views.ViewModelLoadTask.create({
                files: files
            });
            return task;
        };

        this.createFileLoad = function (files) {
            var task = doLittle.tasks.FileLoadTask.create({
                files: files
            });
            return task;
        };
    })
});
doLittle.namespace("doLittle.validation");
doLittle.Exception.define("doLittle.validation.OptionsNotDefined", "Option was undefined");
doLittle.Exception.define("doLittle.validation.OptionsValueNotSpecified", "Required value in Options is not specified. ");
doLittle.Exception.define("doLittle.validation.NotANumber", "Value is not a number");
doLittle.Exception.define("doLittle.validation.NotAString", "Value is not a string");
doLittle.Exception.define("doLittle.validation.ValueNotSpecified","Value is not specified");
doLittle.Exception.define("doLittle.validation.MinNotSpecified","Min is not specified");
doLittle.Exception.define("doLittle.validation.MaxNotSpecified","Max is not specified");
doLittle.Exception.define("doLittle.validation.MinLengthNotSpecified","Min length is not specified");
doLittle.Exception.define("doLittle.validation.MaxLengthNotSpecified","Max length is not specified");
doLittle.Exception.define("doLittle.validation.MissingExpression","Expression is not specified");
doLittle.namespace("doLittle.validation");
doLittle.validation.ruleHandlers = (function () {
    return doLittle.validation.ruleHandlers || { };
})();

doLittle.namespace("doLittle.validation", {
    Rule: doLittle.Type.extend(function (options) {
        options = options || {};
        this.message = options.message || "";
        this.options = {};
        doLittle.extend(this.options, options);

        this.validate = function (value) {
            return true;
        };
    })
});
doLittle.namespace("doLittle.validation");
doLittle.validation.Validator = (function () {
    function Validator(options) {
        var self = this;
        this.isValid = ko.observable(true);
        this.message = ko.observable("");
        this.rules = [];
        this.isRequired = false;
        options = options || {};

        this.setOptions = function (options) {
            function setupRule(ruleType) {
                if (ruleType._name === property) {
                    var rule = ruleType.create({ options: options[property] || {} });
                    self.rules.push(rule);
                }

                if (ruleType._name === "required") {
                    self.isRequired = true;
                }
            }
            for (var property in options) {
                var ruleTypes = doLittle.validation.Rule.getExtenders();
                ruleTypes.some(setupRule);
            }
        };

        this.validate = function (value) {
            self.isValid(true);
            self.message("");
            value = ko.utils.unwrapObservable(value);
            self.rules.some(function(rule) {
                if (!rule.validate(value)) {
                    self.isValid(false);
                    self.message(rule.message);
                    return true;
                } else {
                    self.isValid(true);
                    self.message("");
                }
            });
        };

        this.validateSilently = function (value) {
            self.rules.some(function (rule) {
                if (!rule.validate(value)) {
                    self.isValid(false);
                    return true;
                } else {
                    self.isValid(true);
                }
            });
        };


        this.setOptions(options);
    }

    return {
        create: function (options) {
            var validator = new Validator(options);
            return validator;
        },
        applyTo: function (itemOrItems, options) {
            var self = this;

            function applyToItem(item) {
                var validator = self.create(options);
                item.validator = validator;
            }

            if (itemOrItems instanceof Array) {
                itemOrItems.forEach(function (item) {

                    applyToItem(item);
                });
            } else {
                applyToItem(itemOrItems);
            }
        },
        applyToProperties: function (item, options) {
            var items = [];

            for (var property in item) {
                if (item.hasOwnProperty(property)) {
                    items.push(item[property]);
                }
            }
            this.applyTo(items, options);
        }
    };
})();

if (typeof ko !== 'undefined') {
    doLittle.namespace("doLittle.validation", {
        ValidationSummary: function (commands, containerElement) {
            var self = this;
            this.commands = ko.observable(commands);
            this.messages = ko.observableArray([]);
            this.hasMessages = ko.computed(function(){
                return this.messages().length > 0;
            },self);

            function aggregateMessages() {
                var actualMessages = [];
                self.commands().forEach(function (command) {
                    var unwrappedCommand = ko.utils.unwrapObservable(command);

                    unwrappedCommand.validators().forEach(function (validator) {
                        if (!validator.isValid() && validator.message().length) {
                            actualMessages.push(validator.message());
                        }
                    });
                });
                self.messages(actualMessages);
            }

            commands.forEach(function (command) {
                var unwrappedCommand = ko.utils.unwrapObservable(command);

                unwrappedCommand.validators().forEach(function (validator) {
                    validator.message.subscribe(aggregateMessages);
                });
            });
        }
    });

    ko.bindingHandlers.validationSummaryFor = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var target = ko.bindingHandlers.validationSummaryFor.getValueAsArray(valueAccessor);
            var validationSummary = new doLittle.validation.ValidationSummary(target);
            var ul = document.createElement("ul");
            element.appendChild(ul);
            ul.innerHTML = "<li><span data-bind='text: $data'></span></li>";

            ko.utils.domData.set(element, 'validationsummary', validationSummary);

            ko.applyBindingsToNode(element, { visible: validationSummary.hasMessages }, validationSummary);
            ko.applyBindingsToNode(ul, { foreach: validationSummary.messages }, validationSummary);
        },
        update: function (element, valueAccessor) {
            var validationSummary = ko.utils.domData.get(element, 'validationsummary');
            validationSummary.commands(ko.bindingHandlers.validationSummaryFor.getValueAsArray(valueAccessor));
        },
        getValueAsArray: function (valueAccessor) {
            var target = ko.utils.unwrapObservable(valueAccessor());
            if (!(doLittle.isArray(target))) { target = [target]; }
            return target;
        }
    };
}
if (typeof ko !== 'undefined') {
    ko.bindingHandlers.validationMessageFor = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
            var value = valueAccessor();
            var validator = value.validator;
            if (doLittle.isNullOrUndefined(validator)) {
                return;
            }

            validator.isValid.subscribe(function (newValue) {
                if (newValue === true) {
                    $(element).hide();
                } else {
                    $(element).show();
                }
            });
            ko.applyBindingsToNode(element, { text: validator.message }, validator);
        }
    };
}
if (typeof ko !== 'undefined') {
    ko.extenders.validation = function (target, options) {
        doLittle.validation.Validator.applyTo(target, options);
        target.subscribe(function (newValue) {
            target.validator.validate(newValue);
        });

        // Todo : look into aggressive validation
        //target.validator.validate(target());
        return target;
    };
}

doLittle.namespace("doLittle.validation", {
    notNull: doLittle.validation.Rule.extend(function () {
        this.validate = function (value) {
            return !(doLittle.isUndefined(value) || doLittle.isNull(value));
        };
    })
});


doLittle.namespace("doLittle.validation", {
    required: doLittle.validation.Rule.extend(function () {
        this.validate = function (value) {
            return !(doLittle.isUndefined(value) || doLittle.isNull(value) || value === "" || value === 0);
        };
    })
});


doLittle.namespace("doLittle.validation", {
    length: doLittle.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return doLittle.isUndefined(value) || doLittle.isNull(value);
        }

        function throwIfValueIsNotANumber(value) {
            if (!doLittle.isNumber(value)) {
                throw new doLittle.validation.NotANumber("Value " + value + " is not a number");
            }
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new doLittle.validation.OptionsNotDefined();
            }
            if (notSet(options.max)) {
                throw new doLittle.validation.MaxLengthNotSpecified();
            }
            if (notSet(options.min)) {
                throw new doLittle.validation.MinLengthNotSpecified();
            }
            throwIfValueIsNotANumber(options.min);
            throwIfValueIsNotANumber(options.max);
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                value = "";
            }
            if (!doLittle.isString(value)) {
                value = value.toString();
            }
            return self.options.min <= value.length && value.length <= self.options.max;
        };
    })
});
doLittle.namespace("doLittle.validation", {
    minLength: doLittle.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return doLittle.isUndefined(value) || doLittle.isNull(value);
        }

        function throwIfValueIsNotANumber(value) {
            if (!doLittle.isNumber(value)) {
                throw new doLittle.validation.NotANumber("Value " + value + " is not a number");
            }
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new doLittle.validation.OptionsNotDefined();
            }
            if (notSet(options.length)) {
                throw new doLittle.validation.MaxNotSpecified();
            }
            throwIfValueIsNotANumber(options.length);
        }


        function throwIfValueIsNotAString(string) {
            if (!doLittle.isString(string)) {
                throw new doLittle.validation.NotAString("Value " + string + " is not a string");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                value = "";
            }
            throwIfValueIsNotAString(value);
            return value.length >= self.options.length;
        };
    })
});


doLittle.namespace("doLittle.validation", {
    maxLength: doLittle.validation.Rule.extend(function() {
        var self = this;

        function notSet(value) {
            return doLittle.isUndefined(value) || doLittle.isNull(value);
        }

        function throwIfValueIsNotANumber(value) {
            if (!doLittle.isNumber(value)) {
                throw new doLittle.validation.NotANumber("Value " + value + " is not a number");
            }
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new doLittle.validation.OptionsNotDefined();
            }
            if (notSet(options.length)) {
                throw new doLittle.validation.MaxNotSpecified();
            }
            throwIfValueIsNotANumber(options.length);
        }


        function throwIfValueIsNotAString(string) {
            if (!doLittle.isString(string)) {
                throw new doLittle.validation.NotAString("Value " + string + " is not a string");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                value = "";
            }
            throwIfValueIsNotAString(value);
            return value.length <= self.options.length;
        };
    })
});


doLittle.namespace("doLittle.validation", {
    range: doLittle.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return doLittle.isUndefined(value) || doLittle.isNull(value);
        }

        function throwIfValueIsNotANumber(value, param) {
            if (!doLittle.isNumber(value)) {
                throw new doLittle.validation.NotANumber(param + " value " + value + " is not a number");
            }
        }


        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new doLittle.validation.OptionsNotDefined();
            }
            if (notSet(options.max)) {
                throw new doLittle.validation.MaxNotSpecified();
            }
            if (notSet(options.min)) {
                throw new doLittle.validation.MinNotSpecified();
            }
            throwIfValueIsNotANumber(options.min, "min");
            throwIfValueIsNotANumber(options.max, "max");
        }


        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueIsNotANumber(value, "value");
            return self.options.min <= value && value <= self.options.max;
        };

    })
});

doLittle.namespace("doLittle.validation", {
    lessThan: doLittle.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return doLittle.isUndefined(value) || doLittle.isNull(value);
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new doLittle.validation.OptionsNotDefined();
            }
            if (notSet(options.value)) {
                var exception = new doLittle.validation.OptionsValueNotSpecified();
                exception.message = exception.message + " 'value' is not set.";
                throw exception;
            }
        }

        function throwIsValueToCheckIsNotANumber(value) {
            if (!doLittle.isNumber(value)) {
                throw new doLittle.validation.NotANumber("Value " + value + " is not a number");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIsValueToCheckIsNotANumber(value);
            return parseFloat(value) < parseFloat(self.options.value);
        };
    })
});

doLittle.namespace("doLittle.validation.ruleHandlers");
doLittle.validation.ruleHandlers.lessThanOrEqual = {
    throwIfOptionsInvalid: function (options) {
        if (this.notSet(options)) {
            throw new doLittle.validation.OptionsNotDefined();
        }
        if (this.notSet(options.value)) {
            var exception = new doLittle.validation.OptionsValueNotSpecified();
            exception.message = exception.message + " 'value' is not set.";
            throw exception;
        }
    },

    throwIsValueToCheckIsNotANumber: function (value) {
        if (!doLittle.isNumber(value)) {
            throw new doLittle.validation.NotANumber("Value " + value + " is not a number");
        }
    },

    notSet: function (value) {
        return doLittle.isUndefined(value) || doLittle.isNull(value);
    },

    validate: function (value, options) {
        this.throwIfOptionsInvalid(options);
        if (this.notSet(value)) {
            return false;
        }
        this.throwIsValueToCheckIsNotANumber(value);
        return parseFloat(value) <= parseFloat(options.value);
    }
};

doLittle.namespace("doLittle.validation", {
    greaterThan: doLittle.validation.Rule.extend(function() {
        var self = this;

        function notSet(value) {
            return doLittle.isUndefined(value) || doLittle.isNull(value);
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new doLittle.validation.OptionsNotDefined();
            }
            if (notSet(options.value)) {
                var exception = new doLittle.validation.OptionsValueNotSpecified();
                exception.message = exception.message + " 'value' is not set.";
                throw exception;
            }
            throwIfValueToCheckIsNotANumber(options.value);
        }

        function throwIfValueToCheckIsNotANumber(value) {
            if (!doLittle.isNumber(value)) {
                throw new doLittle.validation.NotANumber("Value " + value + " is not a number");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueToCheckIsNotANumber(value);
            return parseFloat(value) > parseFloat(self.options.value);
        };
    })
});

doLittle.namespace("doLittle.validation.ruleHandlers");
doLittle.validation.ruleHandlers.greaterThanOrEqual = {
    throwIfOptionsInvalid: function (options) {
        if (this.notSet(options)) {
            throw new doLittle.validation.OptionsNotDefined();
        }
        if (this.notSet(options.value)) {
            var exception = new doLittle.validation.OptionsValueNotSpecified();
            exception.message = exception.message + " 'value' is not set.";
            throw exception;
        }
        this.throwIfValueToCheckIsNotANumber(options.value);
    },

    throwIfValueToCheckIsNotANumber: function (value) {
        if (!doLittle.isNumber(value)) {
            throw new doLittle.validation.NotANumber("Value " + value + " is not a number");
        }
    },

    notSet: function (value) {
        return doLittle.isUndefined(value) || doLittle.isNull(value);
    },

    validate: function (value, options) {
        this.throwIfOptionsInvalid(options);
        if (this.notSet(value)) {
            return false;
        }
        this.throwIfValueToCheckIsNotANumber(value);
        return parseFloat(value) >= parseFloat(options.value);
    }
};

doLittle.namespace("doLittle.validation", {
    email: doLittle.validation.Rule.extend(function () {
        var regex = /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$/;

        function notSet(value) {
            return doLittle.isNull(value) || doLittle.isUndefined(value);
        }

        this.validate = function (value) {
            if (notSet(value)) {
                return false;
            }

            if (!doLittle.isString(value)) {
                throw new doLittle.validation.NotAString("Value " + value + " is not a string");
            }

            return (value.match(regex) == null) ? false : true;
        };

    })
});

doLittle.namespace("doLittle.validation", {
    regex: doLittle.validation.Rule.extend(function () {
        var self = this;

        function notSet(value) {
            return doLittle.isUndefined(value) || doLittle.isNull(value);
        }

        function throwIfOptionsInvalid(options) {
            if (notSet(options)) {
                throw new doLittle.validation.OptionsNotDefined();
            }
            if (notSet(options.expression)) {
                throw new doLittle.validation.MissingExpression();
            }
            if (!doLittle.isString(options.expression)) {
                throw new doLittle.validation.NotAString("Expression " + options.expression + " is not a string.");
            }
        }

        function throwIfValueIsNotString(value) {
            if (!doLittle.isString(value)) {
                throw new doLittle.validation.NotAString("Value " + value + " is not a string.");
            }
        }

        this.validate = function (value) {
            throwIfOptionsInvalid(self.options);
            if (notSet(value)) {
                return false;
            }
            throwIfValueIsNotString(value);
            return (value.match(self.options.expression) == null) ? false : true;
        };
    })
});



if (typeof ko !== 'undefined') {
    ko.bindingHandlers.command = {
        init: function (element, valueAccessor, allBindingAccessor, viewModel) {
            var value = valueAccessor();
            var command;
            var contextBound = false;
            if(typeof value.canExecute === "undefined") {
                command = value.target;

                command.parameters = command.parameters || {};
                var parameters = value.parameters || {};

                for (var parameter in parameters ) {
                    var parameterValue = parameters[parameter];
                    
                    if( command.parameters.hasOwnProperty(parameter) &&
                        ko.isObservable(command.parameters[parameter]) ) {
                        command.parameters[parameter](parameterValue);
                    } else {
                        command.parameters[parameter] = ko.observable(parameterValue);
                    }
                }
                contextBound = true;
            } else {
                command = value;
            }
            ko.applyBindingsToNode(element, { click: function() {
                command.execute();
            }}, viewModel);
        }
    };
}
doLittle.namespace("doLittle.commands", {
    HandleCommandTask: doLittle.tasks.ExecutionTask.extend(function (command, server, systemEvents) {
        /// <summary>Represents a task that can handle a command</summary>
        this.name = command.name;

        this.execute = function () {
            var promise = doLittle.execution.Promise.create();

            var commandRequest = doLittle.commands.CommandRequest.createFrom(command);
            var parameters = {
                command: commandRequest
            };

            var url = "/doLittle/CommandCoordinator/Handle?_cmd=" + encodeURIComponent(command._commandType);

            server.post(url, parameters).continueWith(function (result) {
                var commandResult = doLittle.commands.CommandResult.createFrom(result);

                if (commandResult.success === true) {
                    systemEvents.commands.succeeded.trigger(result);
                } else {
                    systemEvents.commands.failed.trigger(result);
                }

                promise.signal(commandResult);
            }).onFail(function (response) {
                var commandResult = doLittle.commands.CommandResult.create();
                commandResult.exception = "HTTP 500";
                commandResult.exceptionMessage = response.statusText;
                commandResult.details = response.responseText;
                systemEvents.commands.failed.trigger(commandResult);
                promise.signal(commandResult);
            });

            return promise;
        };
    })
});
doLittle.namespace("doLittle.commands", {
    HandleCommandsTask: doLittle.tasks.ExecutionTask.extend(function (commands, server) {
        /// <summary>Represents a task that can handle an array of command</summary>
        var self = this;

        this.names = [];
        commands.forEach(function (command) {
            self.names.push(command.name);
        });

        this.execute = function () {
            var promise = doLittle.execution.Promise.create();

            var commandRequests = [];

            commands.forEach(function (command) {
                command.isBusy(true);
                var commandRequest= doLittle.commands.CommandRequest.createFrom(command);
                commandRequests.push(commandRequest);
            });

            var parameters = {
                commands: commandRequests
            };

            var url = "/doLittle/CommandCoordinator/HandleMany";

            server.post(url, parameters).continueWith(function (results) {
                var commandResults = [];

                results.forEach(function (result) {
                    var commandResult = doLittle.commands.CommandResult.createFrom(result);
                    commandResults.push(commandResult);
                });
                promise.signal(commandResults);
            });

            return promise;
        };
    })
});
doLittle.namespace("doLittle.commands", {
    commandCoordinator: doLittle.Singleton(function (taskFactory) {
        this.handle = function (command) {
            var promise = doLittle.execution.Promise.create();
            var task = taskFactory.createHandleCommand(command);

            command.region.tasks.execute(task).continueWith(function (commandResult) {
                promise.signal(commandResult);
            });

            return promise;
        };

        this.handleMany = function (commands, region) {
            var promise = doLittle.execution.Promise.create();

            try {
                var task = taskFactory.createHandleCommands(commands);

                region.tasks.execute(task).continueWith(function (commandResults) {
                    commands.forEach(function (command, index) {
                        var commandResult = commandResults[index];
                        if (commandResult != null && !doLittle.isUndefined(commandResult)) {
                            command.handleCommandResult(commandResult);
                        }
                        command.isBusy(false);
                    });

                    promise.signal(commandResults);
                });
            } catch(e) {
                commands.forEach(function(command) {
                    command.isBusy(false);
                });
            }

            return promise;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.commandCoordinator = doLittle.commands.commandCoordinator;
doLittle.namespace("doLittle.commands", {
    commandValidationService: doLittle.Singleton(function () {
        var self = this;

        function shouldSkipProperty(target, property) {
            if (property === "region") {
                return true;
            }
            if (target instanceof HTMLElement) {
                return true;
            }
            if (!target.hasOwnProperty(property)) {
                return true;
            }
            if (ko.isObservable(target[property])) {
                return false;
            }
            if (typeof target[property] === "function") {
                return true;
            }
            if (property === "_type") {
                return true;
            }
            if (property === "_dependencies") {
                return true;
            }
            if (property === "_namespace") {
                return true;
            }
            if ((target[property] == null)) {
                return true;
            }
            if ((typeof target[property].prototype !== "undefined") &&
                (target[property].prototype !== null) &&
                (target[property] instanceof doLittle.Type)) {
                return true;
            }

            return false;
        }

        function extendProperties(target, validators) {
            for (var property in target) {
                if (shouldSkipProperty(target, property)) {
                    continue;
                }
                if (typeof target[property].validator !== "undefined") {
                    continue;
                }

                if (ko.isObservable(target[property])) {
                    target[property].extend({ validation: {} });
                    target[property].validator.propertyName = property;
                } else if (typeof target[property] === "object") {
                    extendProperties(target[property], validators);
                }
            }
        }

        function validatePropertiesFor(target, result, silent) {
            for (var property in target) {
                if (shouldSkipProperty(target, property)) {
                    continue;
                }

                if (typeof target[property].validator !== "undefined") {
                    var valueToValidate = ko.utils.unwrapObservable(target[property]());
                    if (silent === true) {
                        target[property].validator.validateSilently(valueToValidate);
                    } else {
                        target[property].validator.validate(valueToValidate);
                    }

                    if (target[property].validator.isValid() === false) {
                        result.valid = false;
                    }
                } else if (typeof target[property] === "object") {
                    validatePropertiesFor(target[property], result, silent);
                }
            }
        }


        function applyValidationMessageToMembers(command, members, message) {
            function fixMember(member) {
                property = member.toCamelCase();
                if (property in target) {
                    if (typeof target[property] === "object") {
                        target = target[property];
                    }
                }
            }

            for (var memberIndex = 0; memberIndex < members.length; memberIndex++) {
                var path = members[memberIndex].split(".");
                var property = null;
                var target = command;

                path.forEach(fixMember);

                if (property != null && property.length) {
                    var member = target[property];

                    if (typeof member.validator !== "undefined") {
                        member.validator.isValid(false);
                        member.validator.message(message);
                    }
                }
            }
        }

        this.applyValidationResultToProperties = function (command, validationResults) {

            for (var i = 0; i < validationResults.length; i++) {
                var validationResult = validationResults[i];
                var message = validationResult.errorMessage;
                var memberNames = validationResult.memberNames;
                if (memberNames.length > 0) {
                    applyValidationMessageToMembers(command, memberNames, message);
                }
            }
        };

        this.validate = function (command) {
            var result = { valid: true };
            validatePropertiesFor(command, result);
            return result;
        };
        
        this.validateSilently = function (command) {
            var result = { valid: true };
            validatePropertiesFor(command, result, true);
            return result;
        };

        this.clearValidationMessagesFor = function (target) {
            for (var property in target) {
                if (shouldSkipProperty(target, property)) {
                    continue;
                }

                if (!doLittle.isNullOrUndefined(target[property].validator)) {
                    target[property].validator.message("");
                }
            }
        };

        this.extendPropertiesWithoutValidation = function (command) {
            extendProperties(command);
        };


        function collectValidators(source, validators) {
            for (var property in source) {
                var value = source[property];

                if (shouldSkipProperty(source, property)) {
                    continue;
                }

                if (ko.isObservable(value) && typeof value.validator !== "undefined") {
                    validators.push(value.validator);
                } else if (doLittle.isObject(value)) {
                    collectValidators(value, validators);
                }
            }
        }

        this.getValidatorsFor = function (command) {
            var validators = [];
            collectValidators(command, validators);
            return validators;
        };
    })
});
doLittle.namespace("doLittle.commands", {
    Command: doLittle.Type.extend(function (commandCoordinator, commandValidationService, commandSecurityService, mapper, options, region) {
        var self = this;
        var hasChangesObservables = ko.observableArray();

        this.region = region;
        this._commandType = "";
        this.targetCommand = this;
        this.validators = ko.observableArray();
        this.validationMessages = ko.observableArray();
        this.securityContext = ko.observable(null);
        this.populatedFromExternalSource = ko.observable(false);


        this.isBusy = ko.observable(false);
        this.isValid = ko.computed(function () {
            var valid = true;
            self.validators().some(function (validator) {
                if (ko.isObservable(validator.isValid) && validator.isValid() === false) {
                    valid = false;
                    return true;
                }
            });

            if (self.validationMessages().length > 0) {
                return false;
            }

            return valid;
        });
        this.isAuthorized = ko.observable(false);
        this.canExecute = ko.computed(function () {
            return self.isValid() && self.isAuthorized();
        });
        this.isPopulatedExternally = ko.observable(false);
        this.isReady = ko.computed(function () {
            if (self.isPopulatedExternally() === false) {
                return true;
            }
            return self.populatedFromExternalSource();
        });
        this.isReadyToExecute = ko.computed(function () {
            if (self.isPopulatedExternally() === false) {
                return true;
            }

            return self.hasChanges();
        });


        this.hasChanges = ko.computed(function () {
            var hasChange = false;
            hasChangesObservables().some(function (item) {
                if (item() === true) {
                    hasChange = true;
                    return true;
                }
            });

            return hasChange;
        });

        this.failedCallbacks = [];
        this.succeededCallbacks = [];
        this.completedCallbacks = [];

        this.commandCoordinator = commandCoordinator;
        this.commandValidationService = commandValidationService;
        this.commandSecurityService = commandSecurityService;

        this.options = {
            beforeExecute: function () { },
            failed: function () { },
            succeeded: function () { },
            completed: function () { },
            properties: {}
        };

        /**
         * Adds a failed callback
         * @param {function} - Callback to add
         */
        this.failed = function (callback) {
            self.failedCallbacks.push(callback);
            return self;
        };

        /**
         * Adds a succeeded callback
         * @param {function} - Callback to add
         */
        this.succeeded = function (callback) {
            self.succeededCallbacks.push(callback);
            return self;
        };

        /**
         * Adds a completed callback
         * @param {function} - Callback to add
         */
        this.completed = function (callback) {
            self.completedCallbacks.push(callback);
            return self;
        };

        this.setOptions = function (options) {
            doLittle.extend(self.options, options);
            if (typeof options.name !== "undefined" && typeof options.name === "string") {
                self._name = options.name;
            }
        };

        this.copyPropertiesFromOptions = function () {
            for (var property in self.targetCommand.options.properties) {
                var value = self.targetCommand.options.properties[property];
                if (!ko.isObservable(value)) {
                    value = ko.observable(value);
                }

                self.targetCommand[property] = value;
            }
        };

        this.getProperties = function () {
            var properties = [];
            for (var property in self.targetCommand) {
                if (self.targetCommand.hasOwnProperty(property) && 
                    !(self.hasOwnProperty(property))) {
                    properties.push(property);
                }
            }

            return properties;
        };

        this.makePropertiesObservable = function () {
            var properties = self.getProperties();
            properties.forEach(function (property) {
                var value = null;
                var propertyValue = self.targetCommand[property];

                if (!ko.isObservable(propertyValue) &&
                     (typeof propertyValue !== "object" || doLittle.isArray(propertyValue))) {

                    if (typeof propertyValue !== "function") {
                        if (doLittle.isArray(propertyValue)) {
                            value = ko.observableArray(propertyValue);
                        } else {
                            value = ko.observable(propertyValue);
                        }
                        self.targetCommand[property] = value;
                    }
                }
            });
        };

        this.extendPropertiesWithHasChanges = function () {
            var properties = self.getProperties();
            properties.forEach(function(property) {
                var propertyValue = self.targetCommand[property];
                if (ko.isObservable(propertyValue)) {
                    propertyValue.extend({ hasChanges: {} });
                    if (!doLittle.isNullOrUndefined(propertyValue.hasChanges)) {
                        hasChangesObservables.push(propertyValue.hasChanges);
                    }
                }
            });
        };

        this.onBeforeExecute = function () {
            self.options.beforeExecute();
        };

        this.onFailed = function (commandResult) {
            self.options.failed(commandResult);

            self.failedCallbacks.forEach(function (callback) {
                callback(commandResult);
            });
        };

        this.setInitialValuesForProperties = function (properties) {
            properties.forEach(function (propertyName) {
                var property = self.targetCommand[propertyName];
                if (ko.isObservable(property) &&
                    ko.isWriteableObservable(property) &&
                    doLittle.isFunction(property.setInitialValue)) {
                    var value = property();
                    property.setInitialValue(value);
                }
            });
        };

        this.setInitialValuesFromCurrentValues = function () {
            var properties = self.getProperties();
            self.setInitialValuesForProperties(properties);
        };

        this.onSucceeded = function (commandResult) {
            self.options.succeeded(commandResult);

            self.setInitialValuesFromCurrentValues();

            self.succeededCallbacks.forEach(function (callback) {
                callback(commandResult);
            });
        };

        this.onCompleted = function (commandResult) {
            self.options.completed(commandResult);

            self.completedCallbacks.forEach(function (callback) {
                callback(commandResult);
            });
        };

        this.handleCommandResult = function (commandResult) {
            self.isBusy(false);
            if (typeof commandResult.commandValidationMessages !== "undefined") {
                self.validationMessages(commandResult.commandValidationMessages);
            }

            if (commandResult.success === false || commandResult.invalid === true) {
                if (commandResult.invalid && typeof commandResult.validationResults !== "undefined") {
                    self.commandValidationService.applyValidationResultToProperties(self.targetCommand, commandResult.validationResults);
                }
                self.onFailed(commandResult);
            } else {
                self.onSucceeded(commandResult);
            }
            self.onCompleted(commandResult);
        };

        this.getCommandResultFromValidationResult = function (validationResult) {
            var result = doLittle.commands.CommandResult.create();
            result.invalid = true;
            return result;
        };

        this.execute = function () {
            self.isBusy(true);
            try {
                self.onBeforeExecute();
                var validationResult = self.commandValidationService.validate(this);
                if (validationResult.valid === true) {
                        self.commandCoordinator.handle(self.targetCommand).continueWith(function (commandResult) {
                            self.handleCommandResult(commandResult);
                        });
                } else {
                    var commandResult = self.getCommandResultFromValidationResult(validationResult);
                    self.handleCommandResult(commandResult);
                }
            } catch (ex) {
                self.isBusy(false);
            }
        };

        this.populatedExternally = function () {
            self.isPopulatedExternally(true);
        };

        this.populateFromExternalSource = function (values) {
            self.isPopulatedExternally(true);
            self.setPropertyValuesFrom(values);
            self.populatedFromExternalSource(true);
            commandValidationService.clearValidationMessagesFor(self.targetCommand);
        };

        this.setPropertyValuesFrom = function (values) {
            var mappedProperties = mapper.mapToInstance(self.targetCommand._type, values, self.targetCommand);
            self.setInitialValuesForProperties(mappedProperties);
        };

        this.onCreated = function (lastDescendant) {
            self.targetCommand = lastDescendant;
            if (typeof options !== "undefined") {
                this.setOptions(options);
                this.copyPropertiesFromOptions();
            }
            this.makePropertiesObservable();
            this.extendPropertiesWithHasChanges();
            if (typeof lastDescendant._name !== "undefined" && lastDescendant._name !== "") {
                commandValidationService.extendPropertiesWithoutValidation(lastDescendant);
                var validators = commandValidationService.getValidatorsFor(lastDescendant);
                if (doLittle.isArray(validators) && validators.length > 0) {
                    self.validators(validators);
                }
                commandValidationService.validateSilently(this);
            }
            
            commandSecurityService.getContextFor(lastDescendant).continueWith(function (securityContext) {
                lastDescendant.securityContext(securityContext);

                if (ko.isObservable(securityContext.isAuthorized)) {
                    lastDescendant.isAuthorized(securityContext.isAuthorized());
                    securityContext.isAuthorized.subscribe(function (newValue) {
                        lastDescendant.isAuthorized(newValue);
                    });
                }
            });
        };
    })
});
doLittle.namespace("doLittle.commands");
doLittle.commands.CommandRequest = function(command) {
    var self = this;

    var builtInCommand = {};
    if (typeof doLittle.commands.Command !== "undefined") {
        builtInCommand = doLittle.commands.Command.create({
            region: { commands: [] },
            commandCoordinator: {},
            commandValidationService: {},
            commandSecurityService: { getContextFor: function () { return { continueWith: function () { } }; } },
            mapper: {},
            options: {}
        });
    }

    function shouldSkipProperty(target, property) {
        if (!target.hasOwnProperty(property)) {
            return true;
        }
        if (builtInCommand.hasOwnProperty(property)) {
            return true;
        }
        if (ko.isObservable(target[property])) {
            return false;
        }
        if (typeof target[property] === "function") {
            return true;
        }
        if (property === "_type") {
            return true;
        }
        if (property === "_commandType") {
            return true;
        }
        if (property === "_namespace") {
            return true;
        }

        return false;
    }

    function getPropertiesFromCommand(command) {
        var properties = {};

        for (var property in command) {
            if (!shouldSkipProperty(command, property) ) {
                properties[property] = command[property];
            }
        }
        return properties;
    }

    this.type = command._commandType;
    this.correlationId = doLittle.Guid.create();

    var properties = getPropertiesFromCommand(command);
    var commandContent = ko.toJS(properties);
    this.content = ko.toJSON(commandContent);
};


doLittle.commands.CommandRequest.createFrom = function (command) {
    var commandDescriptor = new doLittle.commands.CommandRequest(command);
    return commandDescriptor;
};
doLittle.namespace("doLittle.commands");
doLittle.commands.CommandResult = (function () {
    function CommandResult(existing) {
        var self = this;
        this.isEmpty = function () {
            return self.commandId === doLittle.Guid.empty;
        };

        this.commandName = "";
        this.commandId = doLittle.Guid.empty;
        this.validationResults = [];
        this.success = true;
        this.invalid = false;
        this.passedSecurity = true;
        this.exception = undefined;
        this.exceptionMessage = "";
        this.commandValidationMessages = [];
        this.securityMessages = [];
        this.allValidationMessages = [];
        this.details = "";

        if (typeof existing !== "undefined") {
            doLittle.extend(this, existing);
        }
    }

    return {
        create: function() {
            var commandResult = new CommandResult();
            return commandResult;
        },
        createFrom: function (result) {
            var existing = typeof result === "string" ? JSON.parse(result) : result;
            var commandResult = new CommandResult(existing);
            return commandResult;
        }
    };
})();
doLittle.dependencyResolvers.command = {
    canResolve: function (namespace, name) {
        if (typeof commands !== "undefined") {
            return name in commands;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return commands[name].create();
    }
};
doLittle.namespace("doLittle.commands", {
    CommandSecurityContext: doLittle.Type.extend(function () {
        this.isAuthorized = ko.observable(false);
    })
});
doLittle.namespace("doLittle.commands", {
    commandSecurityContextFactory: doLittle.Singleton(function () {
        this.create = function () {
            var context = doLittle.commands.CommandSecurityContext.create();
            return context;
        };
    })
});
doLittle.namespace("doLittle.commands", {
    commandSecurityService: doLittle.Singleton(function (commandSecurityContextFactory) {
        var self = this;

        this.commandSecurityContextFactory = commandSecurityContextFactory;

        function getTypeNameFor(command) {
            return command._type._name;
        }

        function getSecurityContextNameFor(type) {
            var securityContextName = type + "SecurityContext";
            return securityContextName;
        }

        function hasSecurityContextInNamespaceFor(type, namespace) {
            var securityContextName = getSecurityContextNameFor(type);
            return !doLittle.isNullOrUndefined(securityContextName) &&
                !doLittle.isNullOrUndefined(namespace) &&
                namespace.hasOwnProperty(securityContextName);
        }

        function getSecurityContextInNamespaceFor(type, namespace) {
            var securityContextName = getSecurityContextNameFor(type, namespace);
            return namespace[securityContextName];
        }

        this.getContextFor = function (command) {
            var promise = doLittle.execution.Promise.create();
            var context;

            var type = getTypeNameFor(command);
            if (hasSecurityContextInNamespaceFor(type, command._type._namespace)) {
                var contextType = getSecurityContextInNamespaceFor(type, command._type._namespace);
                context = contextType.create();
                promise.signal(context);
            } else {
                context = self.commandSecurityContextFactory.create();
                if (doLittle.isNullOrUndefined(command._generatedFrom) || command._generatedFrom === "") {
                    promise.signal(context);
                } else {
                    var url = "/doLittle/CommandSecurity/GetForCommand?commandName=" + command._generatedFrom;
                    $.getJSON(url, function (e) {
                        context.isAuthorized(e.isAuthorized);
                        promise.signal(context);
                    });
                }
            }

            return promise;
        };

        this.getContextForType = function (commandType) {
            var promise = doLittle.execution.Promise.create();
            var context;

            if (hasSecurityContextInNamespaceFor(commandType._name, commandType._namespace)) {
                var contextType = getSecurityContextInNamespaceFor(commandType._name, commandType._namespace);
                context = contextType.create();
                promise.signal(context);
            } else {
                context = doLittle.commands.CommandSecurityContext.create();
                context.isAuthorized(true);
                promise.signal(context);
            }

            return promise;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.commandSecurityService = doLittle.commands.commandSecurityService;
ko.extenders.hasChanges = function (target) {
    target._initialValueSet = false;
    target.hasChanges = ko.observable(false);
    function updateHasChanges() {
        if (target._initialValueSet === false) {
            target.hasChanges(false);
        } else {
            if (doLittle.isArray(target._initialValue)) {
                target.hasChanges(!target._initialValue.shallowEquals(target()));
                return;
            }
            target.hasChanges(target._initialValue !== target());
        }
    }

    target.subscribe(function () {
        updateHasChanges();
    });

    target.setInitialValue = function (value) {
        var initialValue;
        if (doLittle.isArray(value)) {
            initialValue = value.clone();
        } else {
            initialValue = value;
        }

        target._initialValue = initialValue;
        target._initialValueSet = true;
        updateHasChanges();
    };
};
doLittle.namespace("doLittle.commands", {
    commandEvents: doLittle.Singleton(function () {
        this.succeeded = doLittle.Event.create();
        this.failed = doLittle.Event.create();
    })
});
doLittle.namespace("doLittle.interaction", {
    Operation: doLittle.Type.extend(function (region, context) {
        /// <summary>Defines an operation that be performed</summary>
        var self = this;
        var canPerformObservables = ko.observableArray();
        var internalCanPerform = ko.observable(true);

        /// <field name="context" type="doLittle.interaction.Operation">Context in which the operation exists in</field>
        this.context = context;

        /// <field name="identifier" type="doLittle.Guid">Unique identifier for the operation instance<field>
        this.identifier = doLittle.Guid.empty;

        /// <field name="region" type="doLittle.views.Region">Region that the operation was created in</field>
        this.region = region;

        /// <field name="canPerform" type="observable">Set to true if the operation can be performed, false if not</field>
        this.canPerform = ko.computed({
            read: function () {
                if (canPerformObservables().length === 0) {
                    return true;
                }

                var canPerform = true;
                canPerformObservables().forEach(function (observable) {
                    if (observable() === false) {
                        canPerform = false;
                        return;
                    }
                });

                return canPerform;
            },
            write: function (value) {
                internalCanPerform(value);
            }
        });

        this.canPerform.when = function (observable) {
            /// <summary>Chainable clauses</summary>
            /// <param name="observable" type="observable">The observable to use for deciding wether or not the operation can perform</param>
            /// <returns>The canPerform that can be further chained</returns>
            canPerformObservables.push(observable);
            return self.canPerform;
        };

        this.canPerform.when(internalCanPerform);

        this.perform = function () {
            /// <summary>Function that gets called when an operation gets performed</summary>
            /// <returns>State change, if any - typically helpful when undoing</returns>
            return {};
        };

        this.undo = function (state) {
            /// <summary>Function that gets called when an operation gets undoed</summary>
            /// <param name="state" type="object">State generated when the operation was performed</param>
        };
    })
});
doLittle.namespace("doLittle.interaction", {
    OperationContext: doLittle.Type.extend(function () {
        /// <summary>Defines the context in which an operation is being performed or undoed within</summary>

    })
});
doLittle.namespace("doLittle.interaction", {
    OperationEntry: doLittle.Type.extend(function (operation, state) {
        /// <summary>Represents an entry for an operation in a specific context with resulting state</summary>

        /// <field name="operation" type="doLittle.interaction.Operation">Operation that was performed</field>
        this.operation = operation;

        /// <field name="state" type="object">State that operation generated</field>
        this.state = state;
    })
});
doLittle.namespace("doLittle.interaction", {
    operationEntryFactory: doLittle.Singleton(function () {
        /// <summary>Represents a factory that can create OperationEntries</summary>

        this.create = function (operation, state) {
            /// <sumary>Create an instance of a OperationEntry</summary>
            /// <param name="context" type="doLittle.interaction.OperationContext">Context the operation was performed in</param>
            /// <param name="operation" type="doLittle.interaction.Operation">Operation that was performed</param>
            /// <param name="state" type="object">State that operation generated</param>
            /// <returns>An OperationEntry</returns>

            var instance = doLittle.interaction.OperationEntry.create({
                operation: operation,
                state: state
            });
            return instance;
        };
    })
});
doLittle.namespace("doLittle.interaction", {
    Operations: doLittle.Type.extend(function (operationEntryFactory) {
        /// <summary>Represents a stack of operations and the ability to perform and put operations on the stack</summary>
        var self = this;

        /// <field name="all" type="observableArray">Holds all operations</field>
        this.all = ko.observableArray();

        /// <field name="stateful" type="observableArray">Holds all operations that are stateful - meaning that they produce state from being performed</field>
        this.stateful = ko.computed(function () {
            var entries = [];

            self.all().forEach(function (entry) {
                if (!doLittle.areEqual(entry.state, {})) {
                    entries.push(entry);
                }
            });

            return entries;
        });

        this.getByIdentifier = function (identifier) {
            /// <summary>Get an operation by its identifier</identifier>
            /// <param name="identifier" type="doLittle.Guid">Identifier of the operation to get<param>
            /// <returns>An instance of the operation if found, null if not found</returns>

            var found = null;
            self.all().forEach(function (operation) {
                if (operation.identifier === identifier) {
                    found = operation;
                    return;
                }
            });

            return found;
        };

        this.perform = function (operation) {
            /// <summary>Perform an operation in a given context</summary>
            /// <param name="context" type="doLittle.interaction.OperationContext">Context in which the operation is being performed in</param>
            /// <param name="operation" type="doLittle.interaction.Operation">Operation to perform</param>

            if (operation.canPerform() === true) {
                var state = operation.perform();
                var entry = operationEntryFactory.create(operation, state);
                self.all.push(entry);
            }
        };

        this.undo = function () {
            /// <summary>Undo the last operation on the stack and remove it as an operation</summary>

            throw "Not implemented";
        };
    })
});
doLittle.namespace("doLittle.interaction", {
    operationsFactory: doLittle.Singleton(function () {
        this.create = function () {
            var operations = doLittle.interaction.Operations.create();
            return operations;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.operationsFactory = doLittle.interaction.operationsFactory;
doLittle.namespace("doLittle.interaction", {
    CommandOperation: doLittle.interaction.Operation.extend(function (commandSecurityService) {
        /// <summary>Represents an operation that result in a command</summary>
        var self = this;

        /// <field name="commandType" type="doLittle.Type">Type of command to create</field>
        this.commandType = ko.observable();

        // <field name="isAuthorizaed" type="observable">Holds a boolean; true if authorized / false if not</field>
        this.isAuthorized = ko.observable(false);

        // <field name="commandCreated" type="doLittle.Event">Event that gets triggered when command is created</field>
        this.commandCreated = doLittle.Event.create();

        this.canPerform.when(this.isAuthorized);

        this.commandType.subscribe(function (type) {
            commandSecurityService.getContextForType(type).continueWith(function (context) {
                if (!doLittle.isNullOrUndefined(context)) {
                    self.isAuthorized(context.isAuthorized());
                }
            });
        });

        this.createCommandOfType = function (commandType) {
            /// <summary>Create an instance of a given command type</summary>
            var instance = commandType.create({
                region: self.region
            });

            self.commandCreated.trigger(instance);

            return instance;
        };
    })
});
doLittle.namespace("doLittle.interaction", {
    Action: doLittle.Type.extend(function () {
        this.perform = function () {
        };
    })
});
doLittle.namespace("doLittle.interaction", {
    Trigger: doLittle.Type.extend(function () {
        var self = this;

        this.actions = [];

        this.addAction = function (action) {
            self.actions.push(action);
        };

        this.initialize = function (element) {
        };

        this.signal = function () {
            self.actions.forEach(function (action) {
                action.perform();
            });
        };
    })
});
doLittle.namespace("doLittle.interaction", {
    EventTrigger: doLittle.interaction.Trigger.extend(function () {
        var self = this;

        this.eventName = null;

        function throwIfEventNameIsNotSet(trigger) {
            if (!trigger.eventName) {
                throw "EventName is not set for trigger";
            }
        }

        this.initialize = function (element) {
            throwIfEventNameIsNotSet(this);

            var actualEventName = "on" + self.eventName;
            if (element[actualEventName] == null || typeof element[actualEventName] === "function") {
                var originalEventHandler = element[actualEventName];
                element[actualEventName] = function (e) {
                    if (originalEventHandler) {
                        originalEventHandler(e);
                    }
                    
                    self.signal();
                };
            }

        };
    })
});
doLittle.namespace("doLittle.interaction", {
    VisualState: doLittle.Type.extend(function () {
        /// <summary>Represents a visual state of a control or element</summary>
        var self = this;

        /// <field name="name" type="String">Name of the visual state</field>
        this.name = "";

        /// <field name="actions" type="Array" elementType="doLittle.interaction.VisualStateTransitionAction">Transition actions that will execute when transitioning</field>
        this.actions = ko.observableArray();

        this.addAction = function (action) {
            /// <summary>Add action to the visual state</summary>
            /// <param name="action" type="doLittle.interaction.VisualStateAction">
            self.actions.push(action);
        };

        this.enter = function (namingRoot, duration) {
            /// <summary>Enter the state with a given duration</summary>
            /// <param name="duration" type="doLittle.TimeSpan">Time to spend entering the state</param>
            self.actions().forEach(function (action) {
                action.onEnter(namingRoot, duration);
            });
        };

        this.exit = function (namingRoot, duration) {
            /// <summary>Exit the state with a given duration</summary>
            /// <param name="duration" type="doLittle.TimeSpan">Time to spend entering the state</param>
            self.actions().forEach(function (action) {
                action.onExit(namingRoot, duration);
            });
        };
    })
});
doLittle.namespace("doLittle.interaction", {
    VisualStateAction: doLittle.Type.extend(function () {

        this.initialize = function (namingRoot) {

        };

        this.onEnter = function (namingRoot, duration) {

        };

        this.onExit = function (namingRoot, duration) {

        };
    })
});
doLittle.namespace("doLittle.interaction", {
    VisualStateGroup: doLittle.Type.extend(function (dispatcher) {
        /// <summary>Represents a group that holds visual states</summary>
        var self = this;

        this.defaultDuration = doLittle.TimeSpan.zero();

        /// <field name="currentState" type="doLittle.interaction.VisualState">Holds the current state, this is an observable</field>
        this.currentState = ko.observable({name: "null state", enter: function () {}, exit: function () {}});

        /// <field name="states" type="Array" elementType="doLittle.interaction.VisualState">Holds an observable array of visual states</field>
        this.states = ko.observableArray();

        /// <field name="transitions" type="Array" elementType="doLittle.interaction.VisualStateTransition">Holds an observable array of visual state transitions</field>
        this.transitions = ko.observableArray();

        this.addState = function (state) {
            /// <summary>Add a state to the group</summary>
            /// <param name="state" type="doLittle.interaction.VisualState">State to add</param>
            if (self.hasState(state.name)) {
                throw "VisualState with name of '" + state.name + "' already exists";
            }
            self.states.push(state);
        };

        this.addTransition = function (transition) {
            /// <summary>Add transition to group</summary>
            /// <param name="transition" type="doLittle.interaction.VisualStateTransition">Transition to add</param>
            self.transitions.push(transition);
        };

        this.hasState = function (stateName) {
            /// <summary>Check if group has state by the name of the state</summary>
            /// <param name="stateName" type="String">Name of the state to check for</param>
            /// <returns type="Boolean">True if the state exists, false if not</returns>
            var hasState = false;
            self.states().forEach(function (state) {
                if (state.name === stateName) {
                    hasState = true;
                    return;
                }
            });

            return hasState;
        };

        this.getStateByName = function (stateName) {
            /// <summary>Gets a state by its name</summary>
            /// <param name="stateName" type="String">Name of the state to get</param>
            /// <returns type="doLittle.interaction.VisualState">State found or null if it does not have a state by the given name</returns>
            var stateFound = null;
            self.states().forEach(function (state) {
                if (state.name === stateName) {
                    stateFound = state;
                    return;
                }
            });
            return stateFound;
        };

        this.goTo = function (namingRoot, stateName) {
            /// <summary>Go to a specific state by the name of the state</summary>
            /// <param name="stateName" type="String">Name of the state to go to</param>
            var currentState = self.currentState();
            if (!doLittle.isNullOrUndefined(currentState) && currentState.name === stateName) {
                return;
            }

            var state = self.getStateByName(stateName);
            if (!doLittle.isNullOrUndefined(state)) {
                var duration = self.defaultDuration;
                if (!doLittle.isNullOrUndefined(currentState)) {
                    currentState.exit(namingRoot, duration);
                }
                state.enter(namingRoot, duration);

                dispatcher.schedule(duration.totalMilliseconds(), function () {
                    self.currentState(state);
                });
            }
        };
    })
});
doLittle.namespace("doLittle.interaction", {
    VisualStateManager: doLittle.Type.extend(function () {
        /// <summary>Represents a state manager for dealing with visual states, typically related to a control or other element on a page</summary>
        var self = this;

        /// <field name="namingRoot" type="doLittle.views.NamingRoot">A root for named objects</field>
        this.namingRoot = null;

        /// <field name="groups" type="Array" elementType="doLittle.interaction.VisualStateGroup">Holds all groups in the state manager</field>
        this.groups = ko.observableArray();

        this.addGroup = function (group) {
            /// <summary>Adds a VisualStateGroup to the manager</summary>
            /// <param name="group" type="doLittle.interaction.VisualStateGroup">Group to add</param>
            self.groups.push(group);
        };

        this.goTo = function (stateName) {
            /// <summary>Go to a specific state by its name</summary>
            /// <param name="stateName" type="String">Name of state to go to</param>
            self.groups().forEach(function (group) {
                if (group.hasState(stateName) === true) {
                    group.goTo(self.namingRoot, stateName);
                }
            });
        };
    })
});
doLittle.namespace("doLittle.interaction", {
    VisualStateTransition: doLittle.Type.extend(function() {
        /// <summary>Represents a description of transition between two named states</summary>

        /// <field name="from" type="String">Name of visual state that we are describing transitioning from</field>
        this.from = "";

        /// <field name="to" type="String">Name of visual state that we are describing transitioning to</field>
        this.to = "";

        /// <field name="duration" type="doLittle.TimeStamp">Duration for the transition</field>
        this.duration = doLittle.TimeStamp.zero();
    })
});
var globalId = 0;
doLittle.namespace("doLittle.interaction.visualStateActions", {
    Opacity: doLittle.interaction.VisualStateAction.extend(function (documentService) {
        var self = this;
        var element = null;
        var id = documentService.getUniqueStyleName("opacity");

        this.target = "";
        this.value = "";


        this.initialize = function (namingRoot) {
            element = namingRoot.find(self.target);
        };

        this.onEnter = function (namingRoot, duration) {
            var value = parseFloat(self.value);
            if (isNaN(value)) {
                value = 0.0;
            }

            var actualDuration = duration.totalMilliseconds() / 1000;

            documentService.addStyle("." + id, {
                "-webkit-transition": "opacity " + actualDuration + "s ease-in-out",
                "-moz-transition": "opacity " + actualDuration + "s ease-in-out",
                "-ms-transition": "opacity " + actualDuration + "s ease-in-out",
                "-o-transition": "opacity " + actualDuration + "s ease-in-out",
                "transition": "opacity " + actualDuration + "s ease-in-out",
                "opacity": value
            });

            element.classList.add(id);
        };

        this.onExit = function (namingRoot, duration) {
            element.classList.remove(id);
        };
    })
});
doLittle.namespace("doLittle.mapping", {
    MissingPropertyStrategy: doLittle.Type.extend(function () {

    })
});
doLittle.namespace("doLittle.mapping", {
    PropertyMap: doLittle.Type.extend(function (sourceProperty, typeConverters) {
        var self = this;

        this.strategy = null;

        function throwIfMissingPropertyStrategy() {
            if (doLittle.isNullOrUndefined(self.strategy)) {
                throw doLittle.mapping.MissingPropertyStrategy.create();
            }
        }

        this.to = function (targetProperty) {
            self.strategy = function (source, target) {
                var value = ko.unwrap(source[sourceProperty]);
                var targetValue = ko.unwrap(target[targetProperty]);

                var typeAsString = null;
                if (!doLittle.isNullOrUndefined(value)) {
                    if (!doLittle.isNullOrUndefined(targetValue)) {
                        if (value.constructor !== targetValue.constructor) {
                            typeAsString = targetValue.constructor.name.toString();
                        }

                        if (!doLittle.isNullOrUndefined(target[targetProperty]._typeAsString)) {
                            typeAsString = target[targetProperty]._typeAsString;
                        }
                    }

                    if (!doLittle.isNullOrUndefined(typeAsString)) {
                        value = typeConverters.convertFrom(value.toString(), typeAsString);
                    }
                }

                if (ko.isObservable(target[targetProperty])) {
                    target[targetProperty](value);
                } else {
                    target[targetProperty] = value;
                }
            };
        };

        this.map = function (source, target) {
            throwIfMissingPropertyStrategy();

            self.strategy(source, target);
        };
    })
});
doLittle.namespace("doLittle.mapping", {
    Map: doLittle.Type.extend(function () {
        var self = this;

        var properties = {};

        this.sourceType = null;
        this.targetType = null;

        this.source = function (type) {
            self.sourceType = type;
        };

        this.target = function (type) {
            self.targetType = type;
        };

        this.property = function (property) {
            var propertyMap = doLittle.mapping.PropertyMap.create({ sourceProperty: property });
            properties[property] = propertyMap;
            return propertyMap;
        };

        this.canMapProperty = function (property) {
            return properties.hasOwnProperty(property);
        };

        this.mapProperty = function (property, source, target) {
            if (self.canMapProperty(property)) {
                properties[property].map(source, target);
            }
        };
    })
});
doLittle.namespace("doLittle.mapping", {
    maps: doLittle.Singleton(function () {
        var self = this;
        var maps = {};

        function getKeyFrom(sourceType, targetType) {
            return sourceType._typeId + " - " + targetType._typeId;
        }

        var extenders = doLittle.mapping.Map.getExtenders();

        extenders.forEach(function (extender) {
            var map = extender.create();
            var key = getKeyFrom(map.sourceType, map.targetType);
            maps[key] = map;
        });

        this.hasMapFor = function (sourceType, targetType) {
            if (doLittle.isNullOrUndefined(sourceType) || doLittle.isNullOrUndefined(targetType)) {
                return false;
            }
            var key = getKeyFrom(sourceType, targetType);
            return maps.hasOwnProperty(key);
        };

        this.getMapFor = function (sourceType, targetType) {
            if (self.hasMapFor(sourceType, targetType)) {
                var key = getKeyFrom(sourceType, targetType);
                return maps[key];
            }
        };
    })
});
doLittle.namespace("doLittle.mapping", {
    mapper: doLittle.Type.extend(function (typeConverters, maps) {
        "use strict";
        var self = this;

        function getTypeAsString(to, property, value, toValue) {
            var typeAsString = null;
            if (!doLittle.isNullOrUndefined(value) &&
                !doLittle.isNullOrUndefined(toValue)) {

                if (value.constructor !== toValue.constructor) {
                    typeAsString = toValue.constructor.toString().match(/function\040+(\w*)/)[1];
                }
            }

            if (!doLittle.isNullOrUndefined(to[property]) &&
                !doLittle.isNullOrUndefined(to[property]._typeAsString)) {
                typeAsString = to[property]._typeAsString;
            }
            return typeAsString;
        }
        


        function copyProperties(mappedProperties, from, to, map) {
            for (var property in from) {
                if (property.indexOf("_") === 0) {
                    continue;
                }

                if (!doLittle.isUndefined(from[property])) {
                    
                    if (doLittle.isObject(from[property]) && doLittle.isObject(to[property])) {
                        copyProperties(mappedProperties, from[property], to[property]);
                    } else {
                        if (!doLittle.isNullOrUndefined(map)) {
                            if (map.canMapProperty(property)) {
                                map.mapProperty(property, from, to);

                                if (mappedProperties.indexOf(property) < 0) {
                                    mappedProperties.push(property);
                                }

                                continue;
                            }
                        }

                        if (!doLittle.isUndefined(to[property])) {
                            var value = ko.unwrap(from[property]);
                            var toValue = ko.unwrap(to[property]);

                            var typeAsString = getTypeAsString(to, property, value, toValue);

                            if (!doLittle.isNullOrUndefined(typeAsString) && !doLittle.isNullOrUndefined(value)) {
                                value = typeConverters.convertFrom(value.toString(), typeAsString);
                            }

                            if (mappedProperties.indexOf(property) < 0) {
                                mappedProperties.push(property);
                            }


                            if (ko.isObservable(to[property])) {
                                if (!ko.isWriteableObservable(to[property])) {
                                    continue;
                                }

                                to[property](value);
                            } else {
                                to[property] = value;
                            }
                        }
                    }
                }
            }
        }

        function mapSingleInstance(type, data, mappedProperties) {
            if (data) {
                if (!doLittle.isNullOrUndefined(data._sourceType)) {
                    type = eval(data._sourceType);
                }
            }

            var instance = type.create();

            if (data) {
                var map = null;
                if (maps.hasMapFor(data._type, type)) {
                    map = maps.getMapFor(data._type, type);
                }

                copyProperties(mappedProperties, data, instance, map);
            }
            return instance;
        }

        function mapMultipleInstances(type, data, mappedProperties) {
            var mappedInstances = [];
            for (var i = 0; i < data.length; i++) {
                var singleData = data[i];
                mappedInstances.push(mapSingleInstance(type, singleData, mappedProperties));
            }
            return mappedInstances;
        }

        this.map = function (type, data) {
            var mappedProperties = [];
            if (doLittle.isArray(data)) {
                return mapMultipleInstances(type, data, mappedProperties);
            } else {
                return mapSingleInstance(type, data, mappedProperties);
            }
        };

        this.mapToInstance = function (targetType, data, target) {
            var mappedProperties = [];

            var map = null;
            if (maps.hasMapFor(data._type, targetType)) {
                map = maps.getMapFor(data._type, targetType);
            }
            copyProperties(mappedProperties, data, target, map);

            return mappedProperties;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.mapper = doLittle.mapping.mapper;
doLittle.namespace("doLittle.read", {
    readModelSystemEvents: doLittle.Singleton(function () {
        this.noInstance = doLittle.Event.create();
    })
});
doLittle.namespace("doLittle.read", {
    PagingInfo: doLittle.Type.extend(function (size, number) {
        this.size = size;
        this.number = number;
    })
});
doLittle.namespace("doLittle.read", {
    Queryable: doLittle.Type.extend(function (query, queryService, region, targetObservable) {
        var self = this;

        this.canExecute = true;

        this.target = targetObservable;
        this.query = query;
        this.queryService = queryService;
        this.pageSize = ko.observable(0);
        this.pageNumber = ko.observable(0);
        this.totalItems = ko.observable(0);
        this.completedCallbacks = [];

        this.pageSize.subscribe(function () {
            if (self.canExecute) {
                self.execute();
            }
        });

        this.pageNumber.subscribe(function () {
            if (self.canExecute) {
                self.execute();
            }
        });

        function observePropertiesFrom(query) {
            for (var propertyName in query) {
                var property = query[propertyName];
                if (ko.isObservable(property) === true && query.hasOwnProperty(propertyName) && propertyName !== "areAllParametersSet") {
                    property.subscribe(self.execute);
                }
            }
        }

        this.completed = function (callback) {
            self.completedCallbacks.push(callback);
            return self;
        };

        this.onCompleted = function (data) {
            self.completedCallbacks.forEach(function (callback) {
                callback(data);
            });
        };

        this.execute = function () {
            if (self.query.areAllParametersSet() !== true) {
                // TODO: Diagnostics - warning
                return self.target;
            }
            self.query._previousAreAllParametersSet = true;

            var paging = doLittle.read.PagingInfo.create({
                size: self.pageSize(),
                number: self.pageNumber()
            });
            self.queryService.execute(query, paging).continueWith(function (result) {
                self.totalItems(result.totalItems);
                self.target(result.items);
                self.onCompleted(result.items);
            });

            return self.target;
        };

        this.setPageInfo = function (pageSize, pageNumber) {
            
            if (pageSize === self.pageSize() && pageNumber === self.pageNumber()) {
                return;
            }
            
            self.canExecute = false;
            self.pageSize(pageSize);
            self.pageNumber(pageNumber);
            self.canExecute = true;
            self.execute();
        };
        
        observePropertiesFrom(self.query);

        if (self.query.areAllParametersSet()) {
            self.execute();
        }
      
    })
});
doLittle.read.Queryable.new = function (options, region) {
    var observable = ko.observableArray();
    options.targetObservable = observable;
    options.region = region;
    var queryable = doLittle.read.Queryable.create(options);
    doLittle.extend(observable, queryable);
    observable.isQueryable = true;
    return observable;
};
doLittle.namespace("doLittle.read", {
    queryableFactory: doLittle.Singleton(function () {
        this.create = function (query, region) {
            var queryable = doLittle.read.Queryable.new({
                query: query
            }, region);
            return queryable;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.queryableFactory = doLittle.interaction.queryableFactory;
doLittle.namespace("doLittle.read", {
    Query: doLittle.Type.extend(function (queryableFactory, region) {
        var self = this;
        this.target = this;

        this._name = "";
        this._generatedFrom = "";
        this._readModel = null;
        this.region = region;

        this.areAllParametersSet = null;

        this.hasReadModel = function () {
            return typeof self.target._readModel !== "undefined" && self.target._readModel != null;
        };

        this.setParameters = function (parameters) {
            try {
                for (var property in parameters) {
                    if (self.target.hasOwnProperty(property) && ko.isObservable(self.target[property]) === true) {
                        self.target[property](parameters[property]);
                    }
                }
            } catch(ex) {}
        };

        this.getParameters = function () {
            var parameters = {};

            for (var property in self.target) {
                if (ko.isObservable(self.target[property]) &&
                    property !== "areAllParametersSet") {
                    parameters[property] = self.target[property];
                }
            }

            return parameters;
        };

        this.getParameterValues = function () {
            var parameterValues = {};
            var value;

            var parameters = self.getParameters();
            for (var property in parameters) {
                value = parameters[property]();
                if (ko.isObservable(value)) {
                    value = value();
                }
                parameterValues[property] = value;
            }

            return parameterValues;
        };

        this.all = function () {
            var queryable = queryableFactory.create(self.target, region);
            return queryable;
        };

        this.paged = function (pageSize, pageNumber) {
            var queryable = queryableFactory.create(self.target, region);
            queryable.setPageInfo(pageSize, pageNumber);
            return queryable;
        };

        this.onCreated = function (query) {
            self.target = query;

            for (var property in self.target) {
                if (ko.isObservable(self.target[property]) === true) {
                    self.target[property].extend({ linked: {} });
                }
            }
            
            self.areAllParametersSet = ko.computed(function () {
                var isSet = true;
                var hasParameters = false;
                for (var property in self.target) {
                    if (ko.isObservable(self.target[property]) === true) {
                        hasParameters = true;
                        var value = ko.unwrap(self.target[property]());
                        if (typeof value === "undefined" || value === null) {
                            isSet = false;
                            break;
                        }
                    }
                }
                if (hasParameters === false) {
                    return true;
                }
                return isSet;
            });
        };
    })
});
doLittle.namespace("doLittle.read", {
    ReadModel: doLittle.Type.extend(function () {
        var self = this;
        var actualReadModel = this;


        this.copyTo = function (target) {
            for (var property in actualReadModel) {
                if (actualReadModel.hasOwnProperty(property) && property.indexOf("_") !== 0) {
                    var value = ko.utils.unwrapObservable(actualReadModel[property]);
                    if (!target.hasOwnProperty(property)) {
                        target[property] = ko.observable(value);
                    } else {
                        if (ko.isObservable(target[property])) {
                            target[property](value);
                        } else {
                            target[property] = value;
                        }
                    }
                }
            }
        };

        this.onCreated = function (lastDescendant) {
            actualReadModel = lastDescendant;
        };
    })
});
doLittle.namespace("doLittle.read", {
    ReadModelOf: doLittle.Type.extend(function (region, mapper, taskFactory, readModelSystemEvents) {
        var self = this;
        this.target = null;

        this._name = "";
        this._generatedFrom = "";
        this._readModelType = doLittle.Type.extend(function () { });
        this.instance = ko.observable();
        this.commandToPopulate = null;
        this.region = region;

        function unwrapPropertyFilters(propertyFilters) {
            var unwrappedPropertyFilters = {};
            for (var property in propertyFilters) {
                unwrappedPropertyFilters[property] = ko.utils.unwrapObservable(propertyFilters[property]);
            }
            return unwrappedPropertyFilters;
        }

        function performLoad(target, propertyFilters) {
            var task = taskFactory.createReadModel(target, propertyFilters);
            target.region.tasks.execute(task).continueWith(function (data) {
                if (!doLittle.isNullOrUndefined(data)) {
                    var mappedReadModel = mapper.map(target._readModelType, data);
                    self.instance(mappedReadModel);
                } else {
                    readModelSystemEvents.noInstance.trigger(target);
                }
            });
        }

        this.instanceMatching = function (propertyFilters) {
            function load() {
                var unwrappedPropertyFilters = unwrapPropertyFilters(propertyFilters);
                performLoad(self.target, unwrappedPropertyFilters);
            }

            load();

            for (var property in propertyFilters) {
                var value = propertyFilters[property];
                if (ko.isObservable(value)) {
                    value.subscribe(load);
                }
            }
        };

        this.populateCommandOnChanges = function (command) {
            command.populatedExternally();
            
            if (typeof self.instance() !== "undefined" && self.instance() != null) {
                command.populateFromExternalSource(self.instance());
            }

            self.instance.subscribe(function (newValue) {
                command.populateFromExternalSource(newValue);
            });
        };

        this.onCreated = function (lastDescendant) {
            self.target = lastDescendant;
            var readModelInstance = lastDescendant._readModelType.create();
            self.instance(readModelInstance);
        };
    })
});
doLittle.namespace("doLittle.read", {
    ReadModelTask: doLittle.tasks.LoadTask.extend(function (readModelOf, propertyFilters, taskFactory) {
        var url = "/doLittle/ReadModel/InstanceMatching?_rm=" + readModelOf._generatedFrom;
        var payload = {
            descriptor: {
                readModel: readModelOf._name,
                generatedFrom: readModelOf._generatedFrom,
                propertyFilters: propertyFilters
            }
        };

        this.readModel = readModelOf._generatedFrom;

        var innerTask = taskFactory.createHttpPost(url, payload);

        this.execute = function () {
            var promise = innerTask.execute();
            return promise;
        };
    })
});
doLittle.dependencyResolvers.readModelOf = {
    canResolve: function (namespace, name) {
        if (typeof read !== "undefined") {
            return name in read;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return read[name].create();
    }
};
doLittle.dependencyResolvers.query = {
    canResolve: function (namespace, name) {
        if (typeof read !== "undefined") {
            return name in read;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return read[name].create();
    }
};
doLittle.namespace("doLittle.read", {
    QueryTask: doLittle.tasks.LoadTask.extend(function (query, paging, taskFactory) {
        var url = "/doLittle/Query/Execute?_q=" + query._generatedFrom;
        var payload = {
            descriptor: {
                nameOfQuery: query._name,
                generatedFrom: query._generatedFrom,
                parameters: query.getParameterValues()
            },
            paging: {
                size: paging.size,
                number: paging.number
            }
        };

        this.query = query._name;
        this.paging = payload.paging;

        var innerTask = taskFactory.createHttpPost(url, payload);

        this.execute = function () {
            var promise = innerTask.execute();
            return promise;
        };
    })
});
doLittle.namespace("doLittle.read", {
    queryService: doLittle.Singleton(function (mapper, taskFactory) {
        var self = this;

        this.execute = function (query, paging) {
            var promise = doLittle.execution.Promise.create();
            var region = query.region;

            var task = taskFactory.createQuery(query, paging);
            region.tasks.execute(task).continueWith(function (result) {
                if (typeof result === "undefined" || result == null) {
                    result = {};
                }
                if (typeof result.items === "undefined" || result.items == null) {
                    result.items = [];
                }
                if (typeof result.totalItems === "undefined" || result.totalItems == null) {
                    result.totalItems = 0;
                }

                if (query.hasReadModel()) {
                    result.items = mapper.map(query._readModel, result.items);
                }
                promise.signal(result);
            });

            return promise;
        };
    })
});
doLittle.namespace("doLittle.sagas");
doLittle.sagas.Saga = (function () {
    function Saga() {
        var self = this;

        this.executeCommands = function (commands, options) {

            var canExecuteSaga = true;

            commands.forEach(function (command) {
                if (command.onBeforeExecute() === false) {
                    canExecuteSaga = false;
                    return false;
                }
            });

            if (canExecuteSaga === false) {
                return;
            }
            doLittle.commands.commandCoordinator.handleForSaga(self, commands, options);
        };
    }

    return {
        create: function (configuration) {
            var saga = new Saga();
            doLittle.extend(saga, configuration);
            return saga;
        }
    };
})();

doLittle.namespace("doLittle.sagas");
doLittle.sagas.sagaNarrator = (function () {
    var baseUrl = "/doLittle/SagaNarrator";
    // Todo : abstract away into general Service code - look at CommandCoordinator.js for the other copy of this!s
    function post(url, data, completeHandler) {
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'json',
            data: data,
            contentType: 'application/json; charset=utf-8',
            complete: completeHandler
        });
    }

    function isRequestSuccess(jqXHR, commandResult) {
        if (jqXHR.status === 200) {
            if (commandResult.success === true) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    return {
        conclude: function (saga, success, error) {
            var methodParameters = {
                sagaId: saga.Id
            };
            post(baseUrl + "/Conclude", JSON.stringify(methodParameters), function (jqXHR) {
                var commandResult = doLittle.commands.CommandResult.createFrom(jqXHR.responseText);
                var isSuccess = isRequestSuccess(jqXHR, commandResult);
                if (isSuccess === true && typeof success === "function") {
                    success(saga);
                }
                if (isSuccess === false && typeof error === "function") {
                    error(saga);
                }
            });
        }
    };
})();

doLittle.namespace("doLittle.messaging", {
    Messenger: doLittle.Type.extend(function () {
        var subscribers = [];

        this.publish = function (topic, message) {
            if (subscribers.hasOwnProperty(topic)) {
                subscribers[topic].subscribers.forEach(function (item) {
                    item(message);
                });
            }
        };

        this.subscribeTo = function (topic, subscriber) {
            var subscribersByTopic;

            if (subscribers.hasOwnProperty(topic)) {
                subscribersByTopic = subscribers[topic];
            } else {
                subscribersByTopic = { subscribers: [] };
                subscribers[topic] = subscribersByTopic;
            }

            subscribersByTopic.subscribers.push(subscriber);
        };

        return {
            publish: this.publish,
            subscribeTo: this.subscribeTo
        };
    })
});
doLittle.messaging.Messenger.global = doLittle.messaging.Messenger.create();
doLittle.WellKnownTypesDependencyResolver.types.globalMessenger = doLittle.messaging.Messenger.global;

doLittle.namespace("doLittle.messaging", {
    messengerFactory: doLittle.Singleton(function () {
        this.create = function () {
            var messenger = doLittle.messaging.Messenger.create();
            return messenger;
        };

        this.global = function () {
            return doLittle.messaging.Messenger.global;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.messengerFactory = doLittle.messaging.messengerFactory;
if (typeof ko !== 'undefined') {
    ko.observableMessage = function (message, defaultValue) {
        var observable = ko.observable(defaultValue);

        var internal = false;
        observable.subscribe(function (newValue) {
            if (internal === true) {
                return;
            }
            doLittle.messaging.Messenger.global.publish(message, newValue);
        });

        doLittle.messaging.Messenger.global.subscribeTo(message, function (value) {
            internal = true;
            observable(value);
            internal = false;
        });
        return observable;
    };
}
doLittle.namespace("doLittle.services", {
    Service: doLittle.Type.extend(function () {
        var self = this;

        this.url = "";
        this.name = "";

        function prepareArguments(args) {
            var prepared = {};

            for (var property in args) {
                prepared[property] = JSON.stringify(args[property]);
            }

            var stringified = JSON.stringify(prepared);
            return stringified;
        }

        function call(method, args, callback) {
            $.ajax({
                url: self.url + "/" + method,
                type: 'POST',
                dataType: 'json',
                data: prepareArguments(args),
                contentType: 'application/json; charset=utf-8',
                complete: function (result) {
                    var v = $.parseJSON(result.responseText);
                    callback(v);
                }
            });
        }


        this.callWithoutReturnValue = function (method, args) {
            var promise = doLittle.execution.Promise.create();
            call(method, args, function (v) {
                promise.signal();
            });
            return promise;
        };

        this.callWithObjectAsReturn = function (method, args) {
            var value = ko.observable();
            call(method, args, function (v) {
                value(v);
            });
            return value;
        };

        this.callWithArrayAsReturn = function (method, args) {
            var value = ko.observableArray();
            call(method, args, function (v) {
                value(v);
            });
            return value;
        };

        this.onCreated = function (lastDescendant) {
            self.url = lastDescendant.url;
            if (self.url.indexOf("/") !== 0) {
                self.url = "/" + self.url;
            }

            self.name = lastDescendant.name;
        };
    })
});
doLittle.dependencyResolvers.service = {
    canResolve: function (namespace, name) {
        if (typeof services !== "undefined") {
            return name in services;
        }
        return false;
    },

    resolve: function (namespace, name) {
        return services[name].create();
    }
};
doLittle.namespace("doLittle", {
    documentService: doLittle.Singleton(function (DOMRoot) {
        var self = this;

        this.DOMRoot = DOMRoot;

        this.pageHasViewModel = function (viewModel) {
            var context = ko.contextFor($("body")[0]);
            if (doLittle.isUndefined(context)) {
                return false;
            }
            return context.$data === viewModel;
        };

        this.getViewModelNameFor = function (element) {
            var dataViewModelName = element.attributes.getNamedItem("data-viewmodel-name");
            if (doLittle.isNullOrUndefined(dataViewModelName)) {
                dataViewModelName = document.createAttribute("data-viewmodel-name");
                dataViewModelName.value = doLittle.Guid.create();
            }
            element.attributes.setNamedItem(dataViewModelName);
            return dataViewModelName.value;
        };

        this.setViewModelParametersOn = function (element, parameters) {
            element.viewModelParameters = parameters;
        };

        this.getViewModelParametersFrom = function (element) {
            return element.viewModelParameters;
        };

        this.hasViewModelParameters = function (element) {
            return !doLittle.isNullOrUndefined(element.viewModelParameters);
        };

        this.cleanChildrenOf = function (element) {
            self.traverseObjects(function (child) {
                if (child !== element) {
                    $(child).unbind();
                    ko.cleanNode(child);
                }
            }, element);
        };

        this.hasViewFile = function (element) {
            var attribute = element.attributes["data-view-file"];
            return !doLittle.isNullOrUndefined(attribute);
        };

        this.getViewFileFrom = function (element) {
            if (self.hasViewFile(element)) {
                var attribute = element.attributes["data-view-file"];
                return attribute.value;
            }
            return null;
        };


        this.hasOwnRegion = function (element) {
            /// <summary>Check if element has its own region</summary>
            /// <param name="element" type="HTMLElement">HTML Element to check</param>
            /// <returns>true if it has its own region, false it not</returns>

            if (element.region) {
                return true;
            }
            return false;
        };

        this.getParentRegionFor = function (element) {
            /// <summary>Get the parent region for a given element</summary>
            /// <param name="element" type="HTMLElement">HTML Element to get for</param>
            /// <returns>An instance of the region, if no region is found it will return null</returns>
            var found = null;

            while (element.parentNode) {
                element = element.parentNode;
                if (element.region) {
                    return element.region;
                }
            }

            return found;
        };

        this.getRegionFor = function (element) {
            /// <summary>Get region for an element, either directly or implicitly through the nearest parent, null if none</summary>
            /// <param name="element" type="HTMLElement">HTML Element to get for</param>
            /// <returns>An instance of the region, if no region is found it will return null</returns>
            var found = null;

            if (element.region) {
                return element.region;
            }
            found = self.getParentRegionFor(element);
            return found;
        };

        this.setRegionOn = function (element, region) {
            /// <summary>Set region on a specific element</summary>
            /// <param name="element" type="HTMLElement">HTML Element to set on</param>
            /// <param name="region" type="doLittle.views.Region">Region to set on element</param>

            element.region = region;
        };

        this.clearRegionOn = function (element) {
            /// <summary>Clear region on a specific element</summary>
            /// <param name="element" type="HTMLElement">HTML Element to set on</param>
            element.region = null;
        };

        this.traverseObjects = function(callback, element) {
            /// <summary>Traverse objects and call back for each element</summary>
            /// <param name="callback" type="Function">Callback to call for each element found</param>
            /// <param name="element" type="HTMLElement" optional="true">Optional root element</param>
            element = element || self.DOMRoot;
            if (!doLittle.isNullOrUndefined(element)) {
                callback(element);

                if( element.hasChildNodes() ) {
                    var child = element.firstChild;
                    while (child) {
                        var nextSibling = child.nextSibling;
                        if( child.nodeType === 1 ) {
                            self.traverseObjects(callback, child);
                        }
                        child = nextSibling;
                    }
                }
            }
        };

        this.getUniqueStyleName = function(prefix) {
            var id = doLittle.Guid.create();
            var name = prefix+"_"+id;
            return name;
        };

        this.addStyle = function(selector, style) {
            /// <summary>Add a style dynamically into the browser</summary>
            /// <param name="selector" type="String">Selector that represents the class</param>
            /// <param name="style" type="Object">Key/value pair object for styles</param>
            if(!document.styleSheets) {
                return;
            }
            var i;
            var styleString = "";
            for( var property in style ) {
                styleString = styleString + property +":" + style[property]+";";
            }
            style = styleString;

            if(document.getElementsByTagName("head").length === 0) {
                return;
            }

            var styleSheet;
            var media;
            var mediaType;
            if(document.styleSheets.length > 0) {
                for( i = 0; i < document.styleSheets.length; i++) {
                    if(document.styleSheets[i].disabled) {
                        continue;
                    }
                    media = document.styleSheets[i].media;
                    mediaType = typeof media;

                    if(mediaType === "string") {
                        if(media === "" || (media.indexOf("screen") !== -1)) {
                            styleSheet = document.styleSheets[i];
                        }
                    } else if(mediaType === "object") {
                        if(media.mediaText === "" || (media.mediaText.indexOf("screen") !== -1)) {
                            styleSheet = document.styleSheets[i];
                        }
                    }

                    if( typeof styleSheet !== "undefined") {
                        break;
                    }
                }
            }

            if( typeof styleSheet === "undefined") {
                var styleSheetElement = document.createElement("style");
                styleSheetElement.type = "text/css";

                document.getElementsByTagName("head")[0].appendChild(styleSheetElement);

                for( i = 0; i < document.styleSheets.length; i++) {
                    if(document.styleSheets[i].disabled) {
                        continue;
                    }
                    styleSheet = document.styleSheets[i];
                }

                media = styleSheet.media;
                mediaType = typeof media;
            }

            if(mediaType === "string") {
                for( i = 0; i < styleSheet.rules.length; i++) {
                    if(styleSheet.rules[i].selectorText && styleSheet.rules[i].selectorText.toLowerCase() === selector.toLowerCase()) {
                        styleSheet.rules[i].style.cssText = style;
                        return;
                    }
                }

                styleSheet.addRule(selector, style);
            } else if(mediaType === "object") {
                for( i = 0; i < styleSheet.cssRules.length; i++) {
                    if(styleSheet.cssRules[i].selectorText && styleSheet.cssRules[i].selectorText.toLowerCase() === selector.toLowerCase()) {
                        styleSheet.cssRules[i].style.cssText = style;
                        return;
                    }
                }

                styleSheet.insertRule(selector + "{" + style + "}", 0);
            }
        };
    })
});
doLittle.namespace("doLittle.markup", {
    BindingContext: doLittle.Type.extend(function () {
        this.parent = null;
        this.current = null;

        this.changed = doLittle.Event.create();
    })
});
doLittle.namespace("doLittle.markup", {
    bindingContextManager: doLittle.Singleton(function () {

        this.ensure = function (element) {
            // If there is specific bindingContext for element, return it

            // If no specific, find nearest from parent element

            // If no parent element has one either, there is none - return null

            // If element has an attribute of bindingContext - we can now change it to what it is pointing at

            // If bindingContext changes due to a binding being related to the context from the attribute on the element, it should fire the changed thing on the binding context

            // Inherit from parent - always - parent is prototype of current, point back to parent
        };

        this.hasFor = function (element) {

        };

        this.getFor = function (element) {

        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.bindingContextManager = doLittle.markup.bindingContextManager;
doLittle.namespace("doLittle.markup", {
    attributeValues: doLittle.Singleton(function (valueProviderParser) {
        this.expandFor = function (element) {

        };
    })
});
doLittle.namespace("doLittle.markup", {
    valueProviderParser: doLittle.Singleton(function (valueProviders, valueConsumers, typeConverters) {
        var self = this;
        var regex = new RegExp("{{([a-z ,:{{}}}]*)}}", "g");

        function handleValue(instance, property, value) {
            var consumer = valueConsumers.getFor(instance, property);

            if (self.hasValueProvider(value)) {
                var providers = self.parseFor(instance, property, value);
                providers.forEach(function (provider) {
                    provider.provide(consumer);
                });
            } else {
                consumer.consume(value);
            }
        }


        this.hasValueProvider = function (value) {
            var result = value.match(regex);
            if (result) {
                return true;
            }

            return false;
        };

        this.parseFor = function (instance, name, value) {
            var providers = [];
            var result = value.match(regex);
            var expression = result[0].substr(2, result[0].length - 4);

            var rx = new RegExp("([a-z]*) +", "g");
            result = expression.match(rx);
            if (result.length === 1) {
                var valueProviderName = result[0].trim();

                if (valueProviders.isKnown(valueProviderName)) {
                    var provider = valueProviders.getInstanceOf(valueProviderName);
                    providers.push(provider);

                    if (expression.length > result[0].length) {
                        var configurationString = expression.substr(result[0].length);
                        var elements = configurationString.split(",");

                        elements.forEach(function (element) {
                            element = element.trim();

                            var keyValuePair = element.split(":");
                            if (keyValuePair.length === 0) {
                                // something is wrong
                            }
                            if (keyValuePair.length === 1) {
                                // Value only

                                // Only valid if value provider has default property and that property exist

                                var value = keyValuePair[0];
                                handleValue(provider, provider.defaultProperty, value);
                                
                            } else if (keyValuePair.length === 2) {
                                // Property and value

                                // Invalid if property does not exist

                                handleValue(provider, keyValuePair[0], keyValuePair[1]);
                            } else {
                                // something is wrong - there are too many
                            }
                        });
                    }
                }
            }
            return providers;
        };
    })
});
doLittle.namespace("doLittle.markup", {
    ElementVisitor: doLittle.Type.extend(function() {
        this.visit = function (element, resultActions) {

        };
    })
});
doLittle.namespace("doLittle.markup", {
    ElementVisitorResultActions: doLittle.Type.extend(function() {

    })
});
doLittle.namespace("doLittle.markup", {
    objectModelFactory: doLittle.Singleton(function (dependencyResolver, documentService) {
        
        function tryResolveTargetNamespaces(localName, targets, success, error) {
            function tryResolve(queue) {
                if (queue.length) {
                    var namespace = doLittle.namespace(targets.shift());

                    var found = false;
                    namespace._scripts.forEach(function (script) {
                        if (script.toLowerCase() === localName.toLowerCase()) {
                            dependencyResolver.beginResolve(namespace, script)
                                .continueWith(function (instance) {
                                    success(instance);
                                })
                                .onFail(function () {
                                    tryResolveTargetNamespaces(localName, targets, success, error);
                                });
                            found = true;
                        }
                    });

                    if (!found) {
                        tryResolveTargetNamespaces(localName, targets, success, error);
                    }

                } else {
                    error();
                }

            }

            tryResolve(targets);
        }


        this.createFrom = function (element, localName, namespaceDefinition, success, error) {
            tryResolveTargetNamespaces(localName, namespaceDefinition.targets, success, error);
        };
    })
});
doLittle.namespace("doLittle.markup", {
    MultipleNamespacesInNameNotAllowed: doLittle.Type.extend(function (tagName) {
        //"Syntax error: tagname '" + name + "' has multiple namespaces";
    })
});
doLittle.namespace("doLittle.markup", {
    MultiplePropertyReferencesNotAllowed: doLittle.Type.extend(function(tagName) {
        // "Syntax error: tagname '"+name+"' has multiple properties its referring to";
    })
}); 
doLittle.namespace("doLittle.markup", {
    ParentTagNameMismatched: doLittle.Type.extend(function (tagName, parentTagName) {
        // "Setting property using tag '"+name+"' does not match parent tag of '"+parentName+"'";
    })
});
doLittle.namespace("doLittle.markup", {
    NamespaceDefinition: doLittle.Type.extend(function (prefix) {
        var self = this;
        this.prefix = prefix;

        this.targets = [];

        this.addTarget = function (target) {
            self.targets.push(target);
        };
    })
});
doLittle.namespace("doLittle.markup", {
    namespaceDefinitions: doLittle.Singleton(function () {

        this.create = function (prefix) {
            var definition = doLittle.markup.NamespaceDefinition.create({
                prefix: prefix,
            });
            return definition;
        };
    })
});
doLittle.namespace("doLittle.markup", {
    namespaces: doLittle.Singleton(function (namespaceDefinitions, elementNaming) {
        var self = this;
        var ns = "ns:";

        this.global = namespaceDefinitions.create("__global");

        function findNamespaceDefinitionInElementOrParent(prefix, element) {
            if (!doLittle.isNullOrUndefined(element.__namespaces)) {
                var found = null;
                element.__namespaces.forEach(function (definition) {
                    if (definition.prefix === prefix) {
                        found = definition;
                        return false;
                    }
                });

                if (found != null) {
                    return found;
                }
            }
            if (doLittle.isNullOrUndefined(element.parentElement) ||
                element.parentElement.constructor === HTMLHtmlElement) {
                
                return null;
            }

            var parentResult = findNamespaceDefinitionInElementOrParent(prefix, element.parentElement);
            if (parentResult != null) {
                return parentResult;
            }

            return null;
        }


        this.expandNamespaceDefinitions = function (element) {
            for (var attributeIndex = 0; attributeIndex < element.attributes.length; attributeIndex++) {
                var attribute = element.attributes[attributeIndex];
                if( attribute.name.indexOf(ns) === 0) {
                    var prefix = attribute.name.substr(ns.length);
                    var target = attribute.value;

                    var namespaceDefinition = findNamespaceDefinitionInElementOrParent(prefix, element);
                    if (doLittle.isNullOrUndefined(namespaceDefinition)) {
                        if (doLittle.isNullOrUndefined(element.__namespaces)) {
                            element.__namespaces = [];
                        }
                        namespaceDefinition = namespaceDefinitions.create(prefix);
                        element.__namespaces.push(namespaceDefinition);
                    }

                    namespaceDefinition.addTarget(target);
                }
            }
        };

        this.resolveFor = function (element) {
            var prefix = elementNaming.getNamespacePrefixFor(element);
            if (doLittle.isNullOrUndefined(prefix) || prefix === "") {
                return self.global;
            }
            var definition = findNamespaceDefinitionInElementOrParent(prefix, element);
            return definition;
        };
    })
});
doLittle.namespace("doLittle.markup", {
    elementNaming: doLittle.Singleton(function () {

        function getNameAndNamespace(element) {
            var namespace;
            var name = element.localName.toLowerCase();

            var namespaceSplit = name.split(":");
            if (namespaceSplit.length > 2) {
                throw doLittle.markup.MultipleNamespacesInNameNotAllowed.create({ tagName: name });
            }
            if (namespaceSplit.length === 2) {
                name = namespaceSplit[1];
                namespace = namespaceSplit[0];
            }

            return {
                name: name,
                namespace: namespace
            };
        }


        this.getNamespacePrefixFor = function (element) {
            var nameAndNamespace = getNameAndNamespace(element);
            if (doLittle.isNullOrUndefined(nameAndNamespace.namespace)) {
                return "";
            }
            return nameAndNamespace.namespace;
        };

        this.getLocalNameFor = function (element) {
            var nameAndNamespace = getNameAndNamespace(element);
            return nameAndNamespace.name;
        };
    })
});
doLittle.namespace("doLittle.markup", {
    propertyExpander: doLittle.Singleton(function (valueProviderParser) {
        this.expand = function (element, target) {
            for (var attributeIndex = 0; attributeIndex < element.attributes.length; attributeIndex++) {
                var name = element.attributes[attributeIndex].localName;
                var value = element.attributes[attributeIndex].value;

                if (name in target) {
                    if (valueProviderParser.hasValueProvider(value)) {
                        valueProviderParser.parseFor(target, name, value);
                    }
                }
            }
        };
    })
});
doLittle.namespace("doLittle.markup", {
    ObjectModelElementVisitor: doLittle.markup.ElementVisitor.extend(function (elementNaming, namespaces, objectModelFactory, propertyExpander, UIElementPreparer, attributeValues, bindingContextManager) {
        this.visit = function(element, actions) {
            // Tags : 
            //  - tag names automatically match type names
            //  - due to tag names in HTML elements being without case - they become lower case in the
            //    localname property, we will have to search for type by lowercase
            //  - multiple types found with different casing in same namespace should throw an exception
            // Namespaces :
            //  - split by ':'
            //  - if more than one ':' - throw an exception
            //  - if no namespace is defined, try to resolve in the global namespace
            //  - namespaces in the object model can point to multiple JavaScript namespaces
            //  - multiple types with same name in namespace groupings should throw an exception
            //  - registering a namespace can be done on any tag by adding ns:name="point to JS namespace"
            //  - Wildcard registrations to capture anything in a namespace e.g. ns:controls="Web.Controls.*"
            //  - If one registers a namespace with a prefix a parent already has and no naming root sits in between, 
            //    it should add the namespace target on the same definition
            //  - Naming roots are important - if there occurs a naming root, everything is relative to that and 
            //    breaking any "inheritance"
            // Properties : 
            //  - Attributes on an element is a property
            //  - Values in property should always go through type conversion sub system
            //  - Values with encapsulated in {} should be considered markup extensions, go through 
            //    markup extension system for resolving them and then pass on the resulting value 
            //    to type conversion sub system
            //  - Properties can be set with tag suffixed with .<name of property> - more than one
            //    '.' in a tag name should throw an exception
            // Value Provider :
            //  - Any value escaped with {{ }} should be considered a value provider
            // Value Consumer :
            //  - In the opposite end of a value sits a consumer. If the target property is a consumer, pass this
            //    in to the value provider. If the property is just a regular property, use the default property 
            //    value consumer
            // Dependency Properties
            //  - A property type that has the ability of notifying something when it changes
            //    Typically a property gets registered with the ability to offer a callback
            //    Dependency properties needs to be explicitly setup
            //  - Attached dependency properties - one should be able to attach dependency properties 
            //    Adding new functionality to an existing element through exposing new properties on
            //    existing elements. It does not matter what elements, it could be existing ones.
            //    The attached dependency property defines what it is for by specifying a type. Once
            //    we're matching a particular dependency property in the markup with the type it supports
            //    its all good
            // Services
            //  - Nodes should have the ability to specify a service that is relevant for the node.
            //    The service will be called with the element itself. It is defined as an attribute on
            //    a node, any values in the attribute will be handed in, obviously resolved through
            //    the value provider system.
            // Child tags :
            //  - Children which are not a property reference are only allowed if a content or
            //    items property exist. There can only be one of the other, two of either or both
            //    at the same time should yield an exception
            // Templating :
            //  - If a UIElement is found, it will need to be instantiated
            //  - If the instance is of a Control type - we will look at the 
            //    ControlTemplate property for its template and use that to replace content
            //
            // Example : 
            // Simple control:
            // <somecontrol property="42"/>
            // 
            // Control in different namespace:
            // <ns:somecontrol property="42"/>
            //
            // Assigning property with tags:
            // <ns:somecontrol>
            //    <ns:somecontrol.property>42</ns:somcontrol.property>
            // </ns:somecontrol>
            // 
            // Using a markup extension:
            // <ns:somecontrol somevalue="{{binding property}}">
            // <ns:somecontrol
            //
            // <span>{{binding property}}</span>
            //
            // <ns:somecontrol>
            //    <ns:somecontrol.property>{{binding property}}</ns:somcontrol.property>
            // </ns:somecontrol>

            namespaces.expandNamespaceDefinitions(element);
            bindingContextManager.ensure(element);

            if (element.isKnownType()) {
                attributeValues.expandFor(element);
                return;
            }

            var localName = elementNaming.getLocalNameFor(element);
            var namespaceDefinition = namespaces.resolveFor(element);
            objectModelFactory.createFrom(element, localName, namespaceDefinition,
                function (instance) {
                    propertyExpander.expand(element, instance);
                    UIElementPreparer.prepare(element, instance);
                },
                function () {
                }
            );

        };
    })
});
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
doLittle.namespace("doLittle.markup", {
    UIElement: doLittle.markup.NamingRoot.extend(function () {

        this.prepare = function (type, element) {

        };
    })
});
doLittle.namespace("doLittle.markup", {
    UIElementPreparer: doLittle.Singleton(function () {
        this.prepare = function (element, instance) {
            var result = instance.prepare(instance._type, element);
            if (result instanceof doLittle.execution.Promise) {
                result.continueWith(function () {

                    if (!doLittle.isNullOrUndefined(instance.template)) {
                        var UIManager = doLittle.views.UIManager.create();

                        UIManager.handle(instance.template);

                        ko.applyBindingsToNode(instance.template, {
                            "with": instance
                        });

                        element.parentElement.replaceChild(instance.template, element);
                    }
                });
            }
        };
    })
});
doLittle.namespace("doLittle.markup", {
    Control: doLittle.markup.UIElement.extend(function () {
        var self = this;
        this.template = null;

        this.prepare = function (type, element) {
            var promise = doLittle.execution.Promise.create();

            var file = type._namespace._path + type._name + ".html";
            require(["text!" + file + "!strip"], function (v) {
                var container = document.createElement("div");
                container.innerHTML = v;
                self.template = container;

                promise.signal();
            });

            return promise;
        };
    })
});
doLittle.namespace("doLittle.views", {
    PostBindingVisitor: doLittle.Type.extend(function() {
        this.visit = function (element) {

        };
    })
});
doLittle.namespace("doLittle.views", {
    UIManager: doLittle.Singleton(function(documentService) {
        var elementVisitorTypes = doLittle.markup.ElementVisitor.getExtenders();
        var elementVisitors = [];
        var postBindingVisitorTypes = doLittle.views.PostBindingVisitor.getExtenders();
        var postBindingVisitors = [];

        elementVisitorTypes.forEach(function (type) {
            elementVisitors.push(type.create());
        });

        postBindingVisitorTypes.forEach(function (type) {
            postBindingVisitors.push(type.create());
        });

        this.handle = function (root) {
            documentService.traverseObjects(function(element) {
                elementVisitors.forEach(function(visitor) {
                    var actions = doLittle.markup.ElementVisitorResultActions.create();
                    visitor.visit(element, actions);
                });
            }, root);
        };

        this.handlePostBinding = function (root) {
            documentService.traverseObjects(function (element) {
                postBindingVisitors.forEach(function (visitor) {
                    visitor.visit(element);
                });
            }, root);
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.UIManager = doLittle.views.UIManager;
doLittle.namespace("doLittle.views", {
    Content: doLittle.Type.extend(function () {

    })
});
doLittle.namespace("doLittle.views", {
    Items: doLittle.Type.extend(function () {

    })
});
doLittle.namespace("doLittle.views", {
    ComposeTask: doLittle.tasks.Task.extend(function () {
        /// <summary>Represents a base task that represents anything that is executing</summary>
        this.execute = function () {
        };
    })
});
doLittle.namespace("doLittle.views", {
    viewManager: doLittle.Singleton(function (viewFactory, pathResolvers, regionManager, UIManager, viewModelManager, viewModelLoader, viewModelTypes, documentService) {
        var self = this;


        function setViewModelForElement(element, viewModel) {
            viewModelManager.masterViewModel.setFor(element, viewModel);

            var viewModelName = documentService.getViewModelNameFor(element);

            var dataBindString = "";
            var dataBind = element.attributes.getNamedItem("data-bind");
            if (!doLittle.isNullOrUndefined(dataBind)) {
                dataBindString = dataBind.value + ", ";
            } else {
                dataBind = document.createAttribute("data-bind");
            }
            dataBind.value = dataBindString + "viewModel: $root['" + viewModelName + "']";
            element.attributes.setNamedItem(dataBind);
        }

        this.initializeLandingPage = function () {
            var promise = doLittle.execution.Promise.create();
            var body = document.body;
            if (body !== null) {
                var file = doLittle.Path.getFilenameWithoutExtension(document.location.toString());
                if (file === "") {
                    file = "index";
                }

                if (pathResolvers.canResolve(body, file)) {
                    var actualPath = pathResolvers.resolve(body, file);
                    var view = viewFactory.createFrom(actualPath);
                    view.element = body;
                    view.content = body.innerHTML;
                    body.view = view;

                    var region = regionManager.getFor(view);
                    regionManager.describe(view, region).continueWith(function () {
                        if (viewModelManager.hasForView(actualPath)) {
                            var viewModelPath = viewModelManager.getViewModelPathForView(actualPath);
                            if (!viewModelManager.isLoaded(viewModelPath)) {
                                viewModelLoader.load(viewModelPath, region).continueWith(function (viewModel) {
                                    if (!doLittle.isNullOrUndefined(viewModel)) {
                                        setViewModelForElement(body, viewModel);
                                    }
                                });
                            } else {
                                viewModelTypes.beginCreateInstanceOfViewModel(viewModelPath, region).continueWith(function (viewModel) {
                                    if (!doLittle.isNullOrUndefined(viewModel)) {
                                        setViewModelForElement(body, viewModel);
                                    }
                                });
                            }
                        }
                    
                        UIManager.handle(body);
                        promise.signal();
                    });
                }
            }
            return promise;
        };

        this.attach = function (element) {
            UIManager.handle(element);
            viewModelManager.masterViewModel.applyTo(element);
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.viewManager = doLittle.views.viewManager;
doLittle.namespace("doLittle.views", {
    PathResolver: doLittle.Type.extend(function () {
        this.canResolve = function (element, path) {
            return false;
        };

        this.resolve = function (element, path) {

        };
    })
});
doLittle.namespace("doLittle.views", {
    pathResolvers: doLittle.Singleton(function () {

        function getResolvers() {
            var resolvers = [];
            for (var property in doLittle.views.pathResolvers) {
                if (doLittle.views.pathResolvers.hasOwnProperty(property)) {
                    var value = doLittle.views.pathResolvers[property];
                    if( typeof value === "function" &&
                        typeof value.create === "function") {

                        var resolver = value.create();
                        if (typeof resolver.canResolve === "function") {
                            resolvers.push(resolver);
                        }
                    }
                }
            }
            return resolvers;
        }


        this.canResolve = function (element, path) {
            var resolvers = getResolvers();
            for (var resolverIndex = 0; resolverIndex < resolvers.length; resolverIndex++) {
                var resolver = resolvers[resolverIndex];
                var result = resolver.canResolve(element, path);
                if (result === true) {
                    return true;
                }
            }
            return false;
        };

        this.resolve = function (element, path) {
            var resolvers = getResolvers();
            for (var resolverIndex = 0; resolverIndex < resolvers.length; resolverIndex++) {
                var resolver = resolvers[resolverIndex];
                if (resolver.canResolve(element, path)) {
                    return resolver.resolve(element, path);
                }
            }
            return null;
        };
    })
});
doLittle.namespace("doLittle.views", {
    UriMapperPathResolver: doLittle.views.PathResolver.extend(function () {
        this.canResolve = function (element, path) {
            var closest = $(element).closest("[data-urimapper]");
            if (closest.length === 1) {
                var mapperName = $(closest[0]).data("urimapper");
                if (doLittle.uriMappers[mapperName].hasMappingFor(path) === true) {
                    return true;
                }
            }
            return doLittle.uriMappers.default.hasMappingFor(path);
        };

        this.resolve = function (element, path) {
            var closest = $(element).closest("[data-urimapper]");
            if (closest.length === 1) {
                var mapperName = $(closest[0]).data("urimapper");
                if (doLittle.uriMappers[mapperName].hasMappingFor(path) === true) {
                    return doLittle.uriMappers[mapperName].resolve(path);
                }
            }
            return doLittle.uriMappers.default.resolve(path);
        };
    })
});
if (typeof doLittle.views.pathResolvers !== "undefined") {
    doLittle.views.pathResolvers.UriMapperPathResolver = doLittle.views.UriMapperPathResolver;
}
doLittle.namespace("doLittle.views", {
    RelativePathResolver: doLittle.views.PathResolver.extend(function () {
        this.canResolve = function (element, path) {
            var closest = $(element).closest("[data-view]");
            if (closest.length === 1) {
                var view = $(closest[0]).view;

            }
            return false;
        };

        this.resolve = function (element, path) {
            var closest = $(element).closest("[data-urimapper]");
            if (closest.length === 1) {
                var mapperName = $(closest[0]).data("urimapper");
                if (doLittle.uriMappers[mapperName].hasMappingFor(path) === true) {
                    return doLittle.uriMappers[mapperName].resolve(path);
                }
            }
            return doLittle.uriMappers.default.resolve(path);
        };
    })
});
if (typeof doLittle.views.pathResolvers !== "undefined") {
    doLittle.views.pathResolvers.RelativePathResolver = doLittle.views.RelativePathResolver;
}
doLittle.namespace("doLittle.views", {
    View: doLittle.Type.extend(function (viewLoader, viewModelTypes, viewModelManager, path) {
        var self = this;

        this.path = path;
        this.content = "[CONTENT NOT LOADED]";
        this.element = null;
        this.viewModelType = null;
        this.viewModelPath = null;
        this.region = null;

        this.load = function (region) {
            self.region = region;
            var promise = doLittle.execution.Promise.create();
            self.viewModelPath = viewModelManager.getViewModelPathForView(path);
            viewLoader.load(self.path, region).continueWith(function (html) {
                self.content = html;
                self.viewModelType = viewModelTypes.getViewModelTypeForPath(self.viewModelPath);
                promise.signal(self);
            });

            return promise;
        };
    })
});
doLittle.namespace("doLittle.views", {
    viewFactory: doLittle.Singleton(function () {
        this.createFrom = function (path) {
            var view = doLittle.views.View.create({
                path: path
            });
            return view;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.viewFactory = doLittle.views.viewFactory;
doLittle.namespace("doLittle.views", {
    ViewLoadTask: doLittle.views.ComposeTask.extend(function (files, fileManager) {
        /// <summary>Represents a task for loading files asynchronously</summary>

        var self = this;

        this.files = [];
        files.forEach(function (file) {
            self.files.push(file.path.fullPath);
        });

        this.execute = function () {
            var promise = doLittle.execution.Promise.create();

            fileManager.load(files).continueWith(function (instances) {
                var view = instances[0];
                promise.signal(view);
            });
            return promise;
        };
    })
});
doLittle.namespace("doLittle.views", {
    viewLoader: doLittle.Singleton(function (viewModelManager, taskFactory, fileFactory, regionManager) {
        this.load = function (path,region) {
            var promise = doLittle.execution.Promise.create();

            var files = [];

            var viewFile = fileFactory.create(path, doLittle.io.fileType.html);
            if (path.indexOf("?") > 0) {
                viewFile.path.fullPath = viewFile.path.fullPath + path.substr(path.indexOf("?"));
            }
            files.push(viewFile);

            var viewModelPath = null;
            if (viewModelManager.hasForView(path)) {
                viewModelPath = viewModelManager.getViewModelPathForView(path);
                if (!viewModelManager.isLoaded(viewModelPath)) {
                    var viewModelFile = fileFactory.create(viewModelPath, doLittle.io.fileType.javaScript);
                    files.push(viewModelFile);
                }
            }

            var task = taskFactory.createViewLoad(files);
            region.tasks.execute(task).continueWith(function (view) {
                promise.signal(view);
            });

            return promise;
        };
    })
});
doLittle.namespace("doLittle.views", {
    viewBindingHandler: doLittle.Type.extend(function (ViewBindingHandlerTemplateEngine, UIManager, viewFactory, viewManager, viewModelManager, documentService, regionManager, pathResolvers) {
        function makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor, bindingContext) {
            return function () {
                var viewUri = ko.utils.unwrapObservable(valueAccessor());

                if (element.viewUri !== viewUri) {
                    element.children.forEach(ko.removeNode);

                    element.viewModel = null;
                    element.view = null;
                    element.templateSource = null;
                    element.innerHTML = "";
                }

                element.viewUri = viewUri;

                var viewModel = ko.observable(element.viewModel);
                var viewModelParameters = allBindingsAccessor().viewModelParameters || {};

                var templateEngine = null;
                var view = null;
                var region = null;

                if (doLittle.isNullOrUndefined(viewUri) || viewUri === "") {
                    templateEngine = new ko.nativeTemplateEngine();
                } else {
                    templateEngine = ViewBindingHandlerTemplateEngine;
                    var actualPath = pathResolvers.resolve(element, viewUri);
                    view = viewFactory.createFrom(actualPath);
                    view.element = element;
                    region = regionManager.getFor(view);
                }

                return {
                    if: true,
                    data: viewModel,
                    element: element,
                    templateEngine: templateEngine,
                    viewUri: viewUri,
                    viewModelParameters: viewModelParameters,
                    view: view,
                    region: region
                };
            };
        }

        this.init = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            return ko.bindingHandlers.template.init(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor, bindingContext), allBindingsAccessor, viewModel, bindingContext);
        };

        this.update = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            return ko.bindingHandlers.template.update(element, makeTemplateValueAccessor(element, valueAccessor, allBindingsAccessor, bindingContext), allBindingsAccessor, viewModel, bindingContext);
        };
    })
});
doLittle.views.viewBindingHandler.initialize = function () {
    ko.bindingHandlers.view = doLittle.views.viewBindingHandler.create();
    ko.jsonExpressionRewriting.bindingRewriteValidators.view = false; // Can't rewrite control flow bindings
    ko.virtualElements.allowedBindings.view = true;
};
doLittle.namespace("doLittle.views", {
    ViewBindingHandlerTemplateSource: doLittle.Type.extend(function (viewFactory) {
        var content = "";


        this.loadFor = function (element, view, region) {
            var promise = doLittle.execution.Promise.create();

            view.load(region).continueWith(function (loadedView) {
                var wrapper = document.createElement("div");
                wrapper.innerHTML = loadedView.content;
                

                content = wrapper.innerHTML;

                if (doLittle.isNullOrUndefined(loadedView.viewModelType)) {
                    promise.signal(loadedView);
                } else {
                    doLittle.views.Region.current = region;
                    view.viewModelType.ensure().continueWith(function () {
                        promise.signal(loadedView);
                    });
                }
            });

            return promise;
        };

        this.data = function (key, value) { };

        this.text = function (value) {
            return content;
        };
    })
});
doLittle.namespace("doLittle.views", {
    ViewBindingHandlerTemplateEngine: doLittle.Type.extend(function (viewModelManager, regionManager, UIManager) {
        var self = this;
        this.renderTemplate = function (template, bindingContext, options) {
            var templateSource;
            if (doLittle.isNullOrUndefined(options.element.templateSource)) {
                templateSource = doLittle.views.ViewBindingHandlerTemplateSource.create({
                    viewUri: options.viewUri,
                    region: options.region
                });
                options.element.templateSource = templateSource;
            } else {
                templateSource = options.element.templateSource;
            }

            if (doLittle.isNullOrUndefined(options.element.view)) {
                templateSource.loadFor(options.element, options.view, options.region).continueWith(function (view) {
                    options.element.view = view;
                    regionManager.describe(options.view, options.region).continueWith(function () {
                        try {
                            // This is a bit dodgy, but can't find any way around it
                            // Put an empty object for dependency detection - we don't want Knockout to be observing any observables on our viewModel
                            ko.dependencyDetection.begin();

                            var instance;

                            if (!doLittle.isNullOrUndefined(view.viewModelType)) {
                                var viewModelParameters = options.viewModelParameters;
                                viewModelParameters.region = options.region;

                                instance = view.viewModelType.create(viewModelParameters);
                                options.element.viewModel = instance;
                                options.data(instance);

                                bindingContext.$data = instance;
                            } else {
                                instance = {};
                                options.data(instance);
                                bindingContext.$data = instance;
                            }
                        } finally {
                            ko.dependencyDetection.end();
                        }
                    });
                });
            }

            bindingContext.$root = bindingContext.$data;
            var renderedTemplateSource = self.renderTemplateSource(templateSource, bindingContext, options);

            renderedTemplateSource.forEach(function (element) {
                if (element.constructor !== Text && element.constructor !== Comment) {
                    UIManager.handle(element);
                }
            });

            
            return renderedTemplateSource;
        };
    })
});

(function () {
    var nativeTemplateEngine = new ko.nativeTemplateEngine();
    var baseCreate = doLittle.views.ViewBindingHandlerTemplateEngine.create;
    doLittle.views.ViewBindingHandlerTemplateEngine.create = function () {
        var instance = baseCreate.call(doLittle.views.ViewBindingHandlerTemplateEngine, arguments);

        for (var property in nativeTemplateEngine) {
            if (!instance.hasOwnProperty(property)) {
                instance[property] = nativeTemplateEngine[property];
            }
        }

        return instance;
    };
})();

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
doLittle.namespace("doLittle.views", {
    ViewModel: doLittle.Type.extend(function (region) {
        var self = this;
        this.targetViewModel = this;
        this.region = region;

        this.activated = function () {
            if (typeof self.targetViewModel.onActivated === "function") {
                self.targetViewModel.onActivated();
            }
        };

        this.deactivated = function () {
            if (typeof self.targetViewModel.onDeactivated === "function") {
                self.targetViewModel.onDeactivated();
            }
        };

        this.onCreated = function (lastDescendant) {
            self.targetViewModel = lastDescendant;
        };
    })
});
doLittle.namespace("doLittle.views", {
    viewModelTypes: doLittle.Singleton(function () {
        var self = this;

        function getNamespaceFrom(path) {
            var localPath = doLittle.Path.getPathWithoutFilename(path);
            var namespacePath = doLittle.namespaceMappers.mapPathToNamespace(localPath);
            if (namespacePath != null) {
                var namespace = doLittle.namespace(namespacePath);
                return namespace;
            }
            return null;
        }

        function getTypeNameFrom(path) {
            var localPath = doLittle.Path.getPathWithoutFilename(path);
            var filename = doLittle.Path.getFilenameWithoutExtension(path);
            return filename;
        }


        this.isLoaded = function (path) {
            var namespace = getNamespaceFrom(path);
            if (namespace != null) {
                var typename = getTypeNameFrom(path);
                return typename in namespace;
            }
            return false;
        };

        function getViewModelTypeForPathImplementation(path) {
            var namespace = getNamespaceFrom(path);
            if (namespace != null) {
                var typename = getTypeNameFrom(path);
                if (doLittle.isType(namespace[typename])) {
                    return namespace[typename];
                }
            }

            return null;
        }

        this.getViewModelTypeForPath = function (path) {
            var type = getViewModelTypeForPathImplementation(path);
            if (doLittle.isNullOrUndefined(type)) {
                var deepPath = path.replace(".js", "/index.js");
                type = getViewModelTypeForPathImplementation(deepPath);
                if (doLittle.isNullOrUndefined(type)) {
                    deepPath = path.replace(".js", "/Index.js");
                    getViewModelTypeForPathImplementation(deepPath);
                }
            }

            return type;
        };


        this.beginCreateInstanceOfViewModel = function (path, region, viewModelParameters) {
            var promise = doLittle.execution.Promise.create();

            var type = self.getViewModelTypeForPath(path);
            if (type != null) {
                var previousRegion = doLittle.views.Region.current;
                doLittle.views.Region.current = region;

                viewModelParameters = viewModelParameters || {};
                viewModelParameters.region = region;

                type.beginCreate(viewModelParameters)
                        .continueWith(function (instance) {
                            promise.signal(instance);
                        }).onFail(function (error) {
                            console.log("ViewModel '" + path + "' failed instantiation");
                            throw error;
                        });
            } else {
                console.log("ViewModel '" + path + "' does not exist");
                promise.signal(null);
            }

            return promise;
        };

    })
});
doLittle.WellKnownTypesDependencyResolver.types.viewModelTypes = doLittle.views.viewModelTypes;
doLittle.namespace("doLittle.views", {
    viewModelLoader: doLittle.Singleton(function (taskFactory, fileFactory, viewModelTypes) {
        this.load = function (path, region, viewModelParameters) {
            var promise = doLittle.execution.Promise.create();
            var file = fileFactory.create(path, doLittle.io.fileType.javaScript);
            var task = taskFactory.createViewModelLoad([file]);
            region.tasks.execute(task).continueWith(function () {
                viewModelTypes.beginCreateInstanceOfViewModel(path, region, viewModelParameters).continueWith(function (instance) {
                    promise.signal(instance);
                });
            });
            return promise;
        };
    })
});

doLittle.namespace("doLittle.views", {
    ViewModelLoadTask: doLittle.views.ComposeTask.extend(function (files, fileManager) {
        /// <summary>Represents a task for loading viewModels</summary>
        var self = this;

        this.files = [];
        files.forEach(function (file) {
            self.files.push(file.path.fullPath);
        });

        this.execute = function () {
            var promise = doLittle.execution.Promise.create();

            fileManager.load(files).continueWith(function (instances) {
                promise.signal(instances);
            });
            return promise;
        };
    })
});
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
doLittle.namespace("doLittle.views", {
    viewModelBindingHandler: doLittle.Type.extend(function(documentService, viewFactory, viewModelLoader, viewModelManager, viewModelTypes, regionManager) {
        this.init = function (element, valueAccessor, allBindingsAccessor, parentViewModel, bindingContext) {
            var path = ko.utils.unwrapObservable(valueAccessor());
            if (element._isLoading === true || (element._viewModelPath === path && !doLittle.isNullOrUndefined(element._viewModel))) {
                return;
            }

            element._isLoading = true;
            element._viewModelPath = path;

            var viewPath = "/";

            if( documentService.hasViewFile(element) ) {
                viewPath = documentService.getViewFileFrom(element);
            }

            var view = viewFactory.createFrom(viewPath);
            view.content = element.innerHTML;
            view.element = element;

            var viewModelInstance = ko.observable();

            var region = regionManager.getFor(view);
            regionManager.describe(view,region).continueWith(function () {
                var viewModelParameters = allBindingsAccessor().viewModelParameters || {};
                viewModelParameters.region = region;

                if (viewModelTypes.isLoaded(path)) {
                    var viewModelType = viewModelTypes.getViewModelTypeForPath(path);

                    var lastRegion = doLittle.views.Region.current;
                    doLittle.views.Region.current = region;

                    viewModelType.beginCreate(viewModelParameters).continueWith(function (viewModel) {
                        var childBindingContext = bindingContext.createChildContext(viewModel);
                        childBindingContext.$root = viewModel;
                        element._viewModel = viewModel;

                        viewModelInstance(viewModel);
                        doLittle.views.Region.current = lastRegion;

                        element._isLoading = false;
                    }).onFail(function(e) {
                        console.log("Could not create an instance of '" + viewModelType._namespace.name + "." + viewModelType._name+" - Reason : "+e);
                    });
                } else {
                    viewModelLoader.load(path, region, viewModelParameters).continueWith(function (viewModel) {
                        var childBindingContext = bindingContext.createChildContext(viewModel);
                        childBindingContext.$root = viewModel;
                        element._viewModel = viewModel;

                        viewModelInstance(viewModel);

                        element._isLoading = false;
                    });
                }
            });

            return ko.bindingHandlers.with.init(element, viewModelInstance, allBindingsAccessor, parentViewModel, bindingContext);
        };
    })
});
doLittle.views.viewModelBindingHandler.initialize = function () {
    ko.bindingHandlers.viewModel = doLittle.views.viewModelBindingHandler.create();
};


doLittle.namespace("doLittle.views", {
    Region: function(messengerFactory, operationsFactory, tasksFactory) {
        /// <summary>Represents a region in the visual composition on a page</summary>
        var self = this;

        /// <field name="view" type="observable of doLittle.views.View">Observable holding View for the composition</field>
        this.view = ko.observable();

        /// <field name="viewModel" type="doLittle.views.ViewModel">The ViewModel associated with the view</field>
        this.viewModel = null;

        /// <field name="messenger" type="doLittle.messaging.Messenger">The messenger for the region</field>
        this.messenger = messengerFactory.create();

        /// <field name="globalMessenger" type="doLittle.messaging.Messenger">The global messenger</field>
        this.globalMessenger = messengerFactory.global();

        /// <field name="operations" type="doLittle.interaction.Operations">Operations for the region</field>
        this.operations = operationsFactory.create();

        /// <field name="tasks" type="doLittle.tasks.Tasks">Tasks for the region</field>
        this.tasks = tasksFactory.create();

        /// <field name="parent" type="doLittle.views.Region">Parent region, null if there is no parent</field>
        this.parent = null;

        /// <field name="children" type="doLittle.views.Region[]">Child regions within this region</field>
        this.children = ko.observableArray();

        /// <field name="commands" type="observableArray">Array of commands inside the region</field>
        this.commands = ko.observableArray();

        /// <field name="isCommandRoot" type="observable">Whether this region is a command root.
        /// (i.e does not bubble its commands upwards)</field>
        this.isCommandRoot = ko.observable(false);

        /// <field name="aggregatedCommands" type="observableArray">Represents all commands in this region and any child regions</field>
        this.aggregatedCommands = ko.computed(function () {
            var commands = [];

            self.commands().forEach(function (command) {
                commands.push(command);
            });

            self.children().forEach(function (childRegion) {
                if (!childRegion.isCommandRoot()) {
                    childRegion.aggregatedCommands().forEach(function (command) {
                        commands.push(command);
                    });
                }
            });
            return commands;
        });


        function thisOrChildHasTaskType(taskType, propertyName) {
            return ko.computed(function () {
                var hasTask = false;
                self.children().forEach(function (childRegion) {
                    if (childRegion[propertyName]() === true) {
                        hasTask = true;
                        return;
                    }
                });

                self.tasks.all().forEach(function (task) {
                    if (task._type.typeOf(taskType) === true) {
                        hasTask = true;
                    }
                });

                return hasTask;
            });
        }


        function thisOrChildCommandHasPropertySetToTrue(commandPropertyName, breakIfThisHasNoCommands) {
            return ko.computed(function () {
                var isSet = true;

                var commands = self.aggregatedCommands();
                if (breakIfThisHasNoCommands === true) {
                    if (commands.length === 0) {
                        return false;
                    }
                }
                commands.forEach(function (command) {
                    if (command[commandPropertyName]() === false) {
                        isSet = false;
                        return;
                    }
                });

                return isSet;
            });
        }

        function thisOrChildCommandHasPropertySetToFalse(commandPropertyName) {
            return ko.computed(function () {
                var isSet = false;

                var commands = self.aggregatedCommands();
                commands.forEach(function (command) {
                    if (command[commandPropertyName]() === true) {
                        isSet = true;
                        return;
                    }
                });

                return isSet;
            });
        }

        /// <field name="isValid" type="observable">Indiciates wether or not region or any of its child regions are in an invalid state</field>
        this.isValid = thisOrChildCommandHasPropertySetToTrue("isValid");

        /// <field name="canCommandsExecute" type="observable">Indicates wether or not region or any of its child regions can execute their commands</field>
        this.canCommandsExecute = thisOrChildCommandHasPropertySetToTrue("canExecute", true);

        /// <field name="areCommandsAuthorized" type="observable">Indicates wether or not region or any of its child regions have their commands authorized</field>
        this.areCommandsAuthorized = thisOrChildCommandHasPropertySetToTrue("isAuthorized");

        /// <field name="areCommandsAuthorized" type="observable">Indicates wether or not region or any of its child regions have their commands changed</field>
        this.commandsHaveChanges = thisOrChildCommandHasPropertySetToFalse("hasChanges");

        /// <field name="areCommandsAuthorized" type="observable">Indicates wether or not region or any of its child regions have their commands ready to execute</field>
        this.areCommandsReadyToExecute = thisOrChildCommandHasPropertySetToTrue("isReadyToExecute", true);

        /// <field name="areCommandsAuthorized" type="observable">Indicates wether or not region or any of its child regions have changes in their commands or has any operations</field>
        this.hasChanges = ko.computed(function () {
            var commandsHaveChanges = self.commandsHaveChanges();

            
            var childrenHasChanges = false;
            self.children().forEach(function (childRegion) {
                if (!childRegion.isCommandRoot()) {
                    if (childRegion.hasChanges() === true) {
                        childrenHasChanges = true;
                        return;
                    }
                }
            });

            return commandsHaveChanges || (self.operations.stateful().length > 0) || childrenHasChanges;
        });

        /// <field name="validationMessages" type="observableArray">Holds the regions and any of its child regions validation messages</field>
        this.validationMessages = ko.computed(function () {
            var messages = [];

            var commands = self.aggregatedCommands();
            commands.forEach(function (command) {
                if (command.isValid() === false) {
                    command.validators().forEach(function (validator) {
                        if (validator.isValid() === false) {
                            messages.push(validator.message());
                        }
                    });
                }
            });

            return messages;
        });

        /// <field name="isExecuting" type="observable">Indiciates wether or not execution tasks are being performend in this region or any of its child regions</field>
        this.isExecuting = thisOrChildHasTaskType(doLittle.tasks.ExecutionTask, "isExecuting");

        /// <field name="isComposing" type="observable">Indiciates wether or not execution tasks are being performend in this region or any of its child regions</field>
        this.isComposing = thisOrChildHasTaskType(doLittle.views.ComposeTask, "isComposing");

        /// <field name="isLoading" type="observable">Indiciates wether or not loading tasks are being performend in this region or any of its child regions</field>
        this.isLoading = thisOrChildHasTaskType(doLittle.tasks.LoadTask, "isLoading");

        /// <field name="isBusy" type="observable">Indicates wether or not tasks are being performed in this region or any of its child regions</field>
        this.isBusy = ko.computed(function () {
            var isBusy = false;
            self.children().forEach(function (childRegion) {
                if (childRegion.isBusy() === true) {
                    isBusy = true;
                    return;
                }
            });
            
            if (self.tasks.all().length > 0) {
                isBusy = true;
            }

            return isBusy;
        });
    }
});
doLittle.views.Region.current = null;
doLittle.dependencyResolvers.Region = {
    canResolve: function (namespace, name) {
        return name === "region";
    },

    resolve: function (namespace, name) {
        return doLittle.views.Region.current;
    }
};
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
doLittle.namespace("doLittle.views", {
    RegionDescriptor: doLittle.Type.extend(function () {
        this.describe = function (region) {
        };
    })
});
doLittle.namespace("doLittle.views", {
    regionDescriptorManager: doLittle.Singleton(function () {
        /// <summary>Represents a manager that knows how to manage region descriptors</summary>

        this.describe = function (view, region) {
            /// <summary>Describe a specific region related to a view</summary>
            /// <param name="view" type="doLittle.views.View">View related to the region</param>
            /// <param name="region" type="doLittle.views.Region">Region that needs to be described</param>
            var promise = doLittle.execution.Promise.create();
            var localPath = doLittle.Path.getPathWithoutFilename(view.path);
            var namespacePath = doLittle.namespaceMappers.mapPathToNamespace(localPath);
            if (namespacePath != null) {
                var namespace = doLittle.namespace(namespacePath);

                doLittle.views.Region.current = region;
                doLittle.dependencyResolver.beginResolve(namespace, "RegionDescriptor").continueWith(function (descriptor) {
                    descriptor.describe(region);
                    promise.signal();
                }).onFail(function () {
                    promise.signal();
                });
            } else {
                promise.signal();
            }
            return promise;
        };

        this.describeTopLevel = function (region) {

        };
    })
});
doLittle.dependencyResolvers.RegionDescriptor = {
    canResolve: function (namespace, name) {
        return name === "RegionDescriptor";
    },

    resolve: function (namespace, name) {
        return {
            describe: function () { }
        };
    }
};
doLittle.namespace("doLittle.views", {
    DataViewAttributeElementVisitor: doLittle.markup.ElementVisitor.extend(function () {
        this.visit = function (element, actions) {

            var dataView = element.attributes.getNamedItem("data-view");
            if (!doLittle.isNullOrUndefined(dataView)) {
                var dataBindString = "";
                var dataBind = element.attributes.getNamedItem("data-bind");
                if (!doLittle.isNullOrUndefined(dataBind)) {
                    dataBindString = dataBind.value + ", ";
                } else {
                    dataBind = document.createAttribute("data-bind");
                }
                dataBind.value = dataBindString + "view: '" + dataView.value + "'";
                element.attributes.setNamedItem(dataBind);
                element.attributes.removeNamedItem("data-view");
            }
        };
    })
});
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
doLittle.namespace("doLittle.interaction", {
    VisualStateManagerElementVisitor: doLittle.markup.ElementVisitor.extend(function () {
        var visualStateActionTypes = doLittle.interaction.VisualStateAction.getExtenders();

        

        function parseActions(namingRoot, stateElement, state) {
            function parseAction(type) {
                if (type._name.toLowerCase() === child.localName) {
                    var action = type.create();

                    for (var attributeIndex = 0; attributeIndex < child.attributes.length; attributeIndex++) {
                        var name = child.attributes[attributeIndex].localName;
                        var value = child.attributes[attributeIndex].value;
                        if (action.hasOwnProperty(name)) {
                            action[name] = value;
                        }
                    }
                    action.initialize(namingRoot);
                    state.addAction(action);
                }
            }

            if (stateElement.hasChildNodes()) {
                var child = stateElement.firstChild;
                while (child) {
                    visualStateActionTypes.forEach(parseAction);
                    child = child.nextSibling;
                }
            }
        }

        function parseStates(namingRoot, groupElement, group) {
            if( groupElement.hasChildNodes() ) {
                var child = groupElement.firstChild;
                while( child ) {
                    if( child.localName === "visualstate" ) {
                        var state = doLittle.interaction.VisualState.create();
                        state.name = child.getAttribute("name");
                        group.addState(state);
                        parseActions(namingRoot, child, state);
                    }
                    child = child.nextSibling;
                }
            }
        }


        this.visit = function (element, actions) {
            if (element.localName === "visualstatemanager") {
                var visualStateManager = doLittle.interaction.VisualStateManager.create();
                var namingRoot = element.parentElement.namingRoot;
                element.parentElement.visualStateManager = visualStateManager;

                if (element.hasChildNodes()) {
                    var child = element.firstChild;
                    while (child) {
                        if (child.localName === "visualstategroup") {
                            var group = doLittle.interaction.VisualStateGroup.create();
                            visualStateManager.addGroup(group);

                            var duration = child.getAttribute("duration");
                            if (!doLittle.isNullOrUndefined(duration)) {
                                duration = parseFloat(duration);
                                if (!isNaN(duration)) {
                                    duration = duration * 1000;
                                    var timespan = doLittle.TimeSpan.fromMilliseconds(duration);
                                    group.defaultDuration = timespan;
                                }
                            }

                            parseStates(namingRoot, child, group);
                        }
                        child = child.nextSibling;
                    }
                }
            }
        };

    })
});
doLittle.namespace("doLittle.navigation", {
    NavigationFrame: doLittle.Type.extend(function (home, uriMapper, history) {
        var self = this;

        this.home = home;
        this.history = history;

        this.container = null;
        this.currentUri = ko.observable(home);
        this.uriMapper = uriMapper || null;

        this.setCurrentUri = function (path) {
            if (path.indexOf("/") === 0) {
                path = path.substr(1);
            }
            if (path.lastIndexOf("/") === path.length - 1) {
                path = path.substr(0, path.length - 1);
            }
            if (path == null || path.length === 0) {
                path = self.home;
            }
            if (self.uriMapper != null && !self.uriMapper.hasMappingFor(path)) {
                path = self.home;
            }
            self.currentUri(path);
        };

        this.setCurrentUriFromCurrentLocation = function () {
            var state = self.history.getState();
            var uri = doLittle.Uri.create(state.url);
            self.setCurrentUri(uri.path);
        };

        history.Adapter.bind(window, "statechange", function () {
            self.setCurrentUriFromCurrentLocation();
        });
        
        this.configureFor = function (container) {
            self.setCurrentUriFromCurrentLocation();
            self.container = container;

            var uriMapper = $(container).closest("[data-urimapper]");
            if (uriMapper.length === 1) {
                var uriMapperName = $(uriMapper[0]).data("urimapper");
                if (uriMapperName in doLittle.uriMappers) {
                    self.uriMapper = doLittle.uriMappers[uriMapperName];
                }
            }
            if (self.uriMapper == null) {
                self.uriMapper = doLittle.uriMappers.default;
            }
        };

        this.navigate = function (uri) {
            self.setCurrentUri(uri);
        };

    })
});
if (typeof ko !== 'undefined' && typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
    ko.bindingHandlers.navigateTo = {
        init: function (element, valueAccessor, allBindingAccessor, viewModel) {
            ko.applyBindingsToNode(element, {
                click: function() {
                    var featureName = valueAccessor()();
                    History.pushState({feature:featureName},$(element).attr("title"),"/"+ featureName);
                }
            }, viewModel);
        }
    };
}
doLittle.namespace("doLittle.navigation", {
    navigateTo: function (featureName, queryString) {
        var url = featureName;

        if (featureName.charAt(0) !== "/") {
            url = "/" + url;
        }

        if (queryString) {
            url += queryString;
        }

        // TODO: Support title somehow
        if (typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
            History.pushState({}, "", url);
        }
    },
    navigationManager: {
        getCurrentLocation: function() {
            var uri = doLittle.Uri.create(window.location.toString());
            return uri;
        },

        hookup: function () {
            if (typeof History !== "undefined" && typeof History.Adapter !== "undefined") {
                $("body").click(function (e) {
                    var href = e.target.href;
                    if (typeof href === "undefined") {
                        var closestAnchor = $(e.target).closest("a")[0];
                        if (!closestAnchor) {
                            return;
                        }
                        href = closestAnchor.href;
                    }
                    if (href.indexOf("#!") > 0) {
                        href = href.substr(0, href.indexOf("#!"));
                    }

                    if (href.length === 0) {
                        href = "/";
                    }
                    var targetUri = doLittle.Uri.create(href);
                    if (targetUri.isSameAsOrigin &&
                        targetUri.queryString.indexOf("postback")<0) {
                        var target = targetUri.path;
                        while (target.indexOf("/") === 0) {
                            target = target.substr(1);
                        }
                        e.preventDefault();

                        var result = $(e.target).closest("[data-navigation-target]");
                        if (result.length === 1) {
                            var id = $(result[0]).data("navigation-target");
                            var element = $("#"+id);
                            if (element.length === 1 && typeof element[0].navigationFrame !== "undefined") {
                                element[0].navigationFrame.navigate(targetUri.path);
                            } else {
                                // Element not found
                            }
                        } else {
                            var queryString = targetUri.queryString.length > 0 ? "?" + targetUri.queryString : "";
                            History.pushState({}, "", "/" + target + queryString);
                        }
                    }
                });
            }
        }
    }
});
doLittle.namespace("doLittle.navigation", {
    observableQueryParameterFactory: doLittle.Singleton(function () {
        var self = this;

        var historyEnabled = typeof History !== "undefined" && typeof History.Adapter !== "undefined";

        this.create = function (parameterName, defaultValue, navigationManager) {

            function getState() {
                var uri = navigationManager.getCurrentLocation();
                if (uri.parameters.hasOwnProperty(parameterName)) {
                    return uri.parameters[parameterName];
                }

                return null;
            }

            var observable = null;

            if (historyEnabled) {
                History.Adapter.bind(window, "statechange", function () {
                    if (observable != null) {
                        observable(getState());
                    }
                });
            } else {
                window.addEventListener("hashchange", function () {
                    if (observable != null) {
                        var state = getState();
                        if (observable() !== state) {
                            observable(state);
                        }
                    }
                }, false);
            }

            var state = getState();
            observable = ko.observable(state || defaultValue);

            function getQueryStringParametersWithValueForParameter(url, parameterValue) {
                var parameters = doLittle.hashString.decode(url);
                parameters[parameterName] = parameterValue;

                var queryString = "";
                var parameterIndex = 0;
                for (var parameter in parameters) {
                    var value = parameters[parameter];
                    if (!doLittle.isNullOrUndefined(value)) {
                        if (parameterIndex > 0) {
                            queryString += "&";
                        }
                        queryString += parameter + "=" + value;
                    }
                    parameterIndex++;
                }

                return queryString;
            }

            function cleanQueryString(queryString) {
                if (queryString.indexOf("#") === 0 || queryString.indexOf("?") === 0) {
                    queryString = queryString.substr(1);
                }
                return queryString;
            }

            observable.subscribe(function (newValue) {
                var queryString;
                if (historyEnabled) {
                    var state = History.getState();
                    state[parameterName] = newValue;
                    queryString = "?" + getQueryStringParametersWithValueForParameter(cleanQueryString(state.url), newValue);
                    History.pushState(state, state.title, queryString);
                } else {
                    queryString = "#" + getQueryStringParametersWithValueForParameter(cleanQueryString(document.location.hash), newValue);
                    document.location.hash = queryString;
                }
            });

            return observable;
        };
    })
});

ko.observableQueryParameter = function (parameterName, defaultValue) {
    var navigationManager = doLittle.navigation.navigationManager;
    var observable = doLittle.navigation.observableQueryParameterFactory.create().create(parameterName, defaultValue, navigationManager);
    return observable;
};

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

doLittle.namespace("doLittle.navigation", {
    navigationBindingHandler: doLittle.Type.extend(function () {
        function getNavigationFrameFor(valueAccessor) {
            var configurationString = ko.utils.unwrapObservable(valueAccessor());
            var configurationItems = ko.expressionRewriting.parseObjectLiteral(configurationString);
            var configuration = {};

            for (var index = 0; index < configurationItems.length; index++) {
                var item = configurationItems[index];
                configuration[item.key.trim()] = item.value.trim();
            }

            var uriMapperName = configuration.uriMapper;
            if (doLittle.isNullOrUndefined(uriMapperName)) {
                uriMapperName = "default";
            }

            var mapper = doLittle.uriMappers[uriMapperName];
            var frame = doLittle.navigation.NavigationFrame.create({
                locationAware: false,
                uriMapper: mapper,
                home: configuration.home || ''
            });

            return frame;
        }

        function makeValueAccessor(navigationFrame) {
            return function () {
                return navigationFrame.currentUri();
            };
        }

        this.init = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var navigationFrame = getNavigationFrameFor(valueAccessor);
            navigationFrame.configureFor(element);
            return ko.bindingHandlers.view.init(element, makeValueAccessor(navigationFrame), allBindingsAccessor, viewModel, bindingContext);
        };
        this.update = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var navigationFrame = getNavigationFrameFor(valueAccessor);
            navigationFrame.configureFor(element);
            return ko.bindingHandlers.view.update(element, makeValueAccessor(navigationFrame), allBindingsAccessor, viewModel, bindingContext);
        };
    })
});
doLittle.navigation.navigationBindingHandler.initialize = function () {
    ko.bindingHandlers.navigation = doLittle.navigation.navigationBindingHandler.create();
    ko.jsonExpressionRewriting.bindingRewriteValidators.navigation = false; // Can't rewrite control flow bindings
    ko.virtualElements.allowedBindings.navigation = true;
};

doLittle.namespace("doLittle.values", {
    TypeConverter: doLittle.Type.extend(function () {
        this.supportedType = null;

        this.convertFrom = function (value) {
            return value;
        };

        this.convertTo = function (value) {
            return value;
        };
    })
});
doLittle.namespace("doLittle.values", {
    NumberTypeConverter: doLittle.values.TypeConverter.extend(function () {
        var allowedCharacters = "0123456789.,";

        this.supportedType = Number;

        function stripLetters(value) {
            value = value.toString();
            var returnValue = "";

            for (var charIndex = 0; charIndex < value.length; charIndex++) {
                var found = false;
                for (var allowedCharIndex = 0; allowedCharIndex < allowedCharacters.length; allowedCharIndex++) {
                    if (value[charIndex] === allowedCharacters[allowedCharIndex]) {
                        found = true;
                        returnValue = returnValue + value[charIndex];
                        break;
                    }
                }
            }

            return returnValue;
        }

        this.convertFrom = function (value) {
            if (value.constructor === Number) {
                return value;
            }
            if (value === "") {
                return 0;
            }
            value = stripLetters(value);
            var result = 0;
            if (value.indexOf(".") >= 0) {
                result = parseFloat(value);
            } else {
                result = parseInt(value);
            }
            return result;
        };
    })
});
doLittle.namespace("doLittle.values", {
    DateTypeConverter: doLittle.values.TypeConverter.extend(function () {
        this.supportedType = Date;

        function isNull(time) {
            // Treat serialization of default(DateTime) from server as null.
            return doLittle.isNullOrUndefined(time) ||
                // ISO 8601 formats for default(DateTime):
                time === "0001-01-01T00:00:00" ||
                time === "0001-01-01T00:00:00Z" ||
                // new Date("0001-01-01T00:00:00") in Chrome and Firefox:
                (time instanceof Date && time.getTime() === -62135596800000) ||
                // new Date("0001-01-01T00:00:00") or any other invalid date in Internet Explorer:
                (time instanceof Date && isNaN(time.getTime()));
        }

        this.convertFrom = function (value) {
            if (isNull(value)) {
                return null;
            }
            var date = new Date(value);
            return date;
        };

        this.convertTo = function (value) {
            return value.format("yyyy-MM-dd");
        };
    })
});
doLittle.namespace("doLittle.values", {
    StringTypeConverter: doLittle.values.TypeConverter.extend(function () {
        this.supportedType = String;

        this.convertFrom = function (value) {
            return value.toString();
        };
    })
});
doLittle.namespace("doLittle.values", {
    typeConverters: doLittle.Singleton(function () {
        var convertersByType = {};

        var typeConverterTypes = doLittle.values.TypeConverter.getExtenders();
        typeConverterTypes.forEach(function (type) {
            var converter = type.create();
            convertersByType[converter.supportedType] = converter;
        });

        this.convertFrom = function (value, type) {
            var actualType = null;
            if (doLittle.isString(type)) {
                actualType = eval(type);
            } else {
                actualType = type;
            }
            if (convertersByType.hasOwnProperty(actualType)) {
                return convertersByType[actualType].convertFrom(value);
            }

            return value;
        };

        this.convertTo = function (value) {
            if (doLittle.isNullOrUndefined(value)) {
                return value;
            }
            for (var converter in convertersByType) {
                /* jshint eqeqeq: false */
                if (value.constructor == converter) {
                    return convertersByType[converter].convertTo(value);
                }
            }

            return value;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.typeConverters = doLittle.values.typeConverters;

doLittle.namespace("doLittle.values", {
    typeExtender: doLittle.Singleton(function () {
        this.extend = function (target, typeAsString) {
            target._typeAsString = typeAsString;
        };
    })
});
ko.extenders.type = doLittle.values.typeExtender.create().extend;

doLittle.namespace("doLittle.values", {
    Formatter: doLittle.Type.extend(function () {
        this.supportedType = null;

        this.format = function (value, format) {
            return value;
        };
    })
});
doLittle.namespace("doLittle.values", {
    DateFormatter: doLittle.values.Formatter.extend(function () {
        this.supportedType = Date;

        this.format = function (value, format) {
            return value.format(format);
        };
    })
});
doLittle.namespace("doLittle.values", {
    stringFormatter: doLittle.Singleton(function () {
        var formatterTypes = doLittle.values.Formatter.getExtenders();
        var formattersByType = {};

        formatterTypes.forEach(function (type) {
            var formatter = type.create();
            formattersByType[formatter.supportedType] = formatter;
        });

        function getFormat(element) {
            if (element.nodeType !== 1 || doLittle.isNullOrUndefined(element.attributes)) {
                return null;
            }
            var stringFormatAttribute = element.attributes.getNamedItem("data-stringformat");
            if (!doLittle.isNullOrUndefined(stringFormatAttribute)) {
                return stringFormatAttribute.value;
            }

            return null;
        }

        this.hasFormat = function (element) {
            var format = getFormat(element);
            return format !== null;
        };

        this.format = function (element, value) {
            var format = getFormat(element);

            if (formattersByType.hasOwnProperty(value.constructor)) {
                var formatter = formattersByType[value.constructor];
                var regex = new RegExp("{(.[^{}])*}", "g");
                var result = format.replace(regex, function (formatExpression) {
                    var expression = formatExpression.substr(1, formatExpression.length - 2);
                    return formatter.format(value, expression);
                });
                return result;
            }

            return format;
        };
    })
});
doLittle.namespace("doLittle.values", {
    valuePipeline: doLittle.Singleton(function (typeConverters, stringFormatter) {
        this.getValueForView = function (element, value) {
            if (doLittle.isNullOrUndefined(value)) {
                return value;
            }
            var actualValue = ko.utils.unwrapObservable(value);
            if (doLittle.isNullOrUndefined(actualValue)) {
                return value;
            }

            var returnValue = actualValue;

            if (stringFormatter.hasFormat(element)) {
                returnValue = stringFormatter.format(element, actualValue);
            } else {
                if (!doLittle.isNullOrUndefined(value._typeAsString)) {
                    returnValue = typeConverters.convertTo(actualValue);
                }
            }
            return returnValue;
        };

        this.getValueForProperty = function (property, value) {
            if (doLittle.isNullOrUndefined(property)) {
                return value;
            }
            if (doLittle.isNullOrUndefined(value)) {
                return value;
            }
            if (!doLittle.isNullOrUndefined(property._typeAsString)) {
                value = typeConverters.convertFrom(value, property._typeAsString);
            }

            return value;
        };
    })
});

(function () {
    var valuePipeline = doLittle.values.valuePipeline.create();

    var oldReadValue = ko.selectExtensions.readValue;
    ko.selectExtensions.readValue = function (element) {
        var value = oldReadValue(element);

        var bindings = ko.bindingProvider.instance.getBindings(element, ko.contextFor(element));
        if (doLittle.isNullOrUndefined(bindings)) {
            return value;
        }
        var result = valuePipeline.getValueForProperty(bindings.value, value);
        return result;
    };

    var oldWriteValue = ko.selectExtensions.writeValue;
    ko.selectExtensions.writeValue = function (element, value, allowUnset) {
        var result = value;
        var bindings = ko.bindingProvider.instance.getBindings(element, ko.contextFor(element));
        if (!doLittle.isNullOrUndefined(bindings)) {
            result = ko.utils.unwrapObservable(valuePipeline.getValueForView(element, bindings.value));
        }
        oldWriteValue(element, result, allowUnset);
    };

    var oldSetTextContent = ko.utils.setTextContent;
    ko.utils.setTextContent = function (element, value) {
        var result = valuePipeline.getValueForView(element, value);
        oldSetTextContent(element, result);
    };

    var oldSetHtml = ko.utils.setHtml;
    ko.utils.setHtml = function (element, value) {
        var result = valuePipeline.getValueForView(element, value);
        oldSetHtml(element, result);
    };
})();
doLittle.namespace("doLittle.values", {
    ValueProvider: doLittle.Type.extend(function () {

        this.defaultProperty = null;

        this.provide = function (consumer) {

        };
    })
});
doLittle.namespace("doLittle.values", {
    valueProviders: doLittle.Singleton(function () {

        this.isKnown = function (name) {
            var found = false;
            var valueProviders = doLittle.values.ValueProvider.getExtenders();
            valueProviders.forEach(function (valueProviderType) {
                if (valueProviderType._name.toLowerCase() === name) {
                    found = true;
                    return;
                }
            });
            return found;
        };

        this.getInstanceOf = function (name) {
            var instance = null;
            var valueProviders = doLittle.values.ValueProvider.getExtenders();
            valueProviders.forEach(function (valueProviderType) {
                if (valueProviderType._name.toLowerCase() === name) {
                    instance = valueProviderType.create();
                    return;
                }
            });

            return instance;
        };
    })
});
doLittle.WellKnownTypesDependencyResolver.types.valueProviders = doLittle.values.valueProviders;
doLittle.namespace("doLittle.values", {
    ValueConsumer: doLittle.Type.extend(function () {

        this.canNotifyChanges = function () {
            return false;
        };

        this.notifyChanges = function (callback) {
        };

        this.consume = function(value) {
        };
    })
});
doLittle.namespace("doLittle.values", {
    valueConsumers: doLittle.Singleton(function () {

        this.getFor = function (instance, propertyName) {
            var consumer = doLittle.values.DefaultValueConsumer.create({
                target: instance,
                property: propertyName
            });
            return consumer;
        };

    })
});
doLittle.WellKnownTypesDependencyResolver.types.valueConsumers = doLittle.values.valueConsumers;
doLittle.namespace("doLittle.values", {
    Binding: doLittle.values.ValueProvider.extend(function (bindingContextManager) {

        this.defaultProperty = "path";

        this.path = "";
        this.mode = null;
        this.converter = null;
        this.format = null;

        this.provide = function (consumer) {

        };
    })
});
doLittle.namespace("doLittle.values", {
    DefaultValueConsumer: doLittle.values.ValueConsumer.extend(function (target, property) {
        this.consume = function(value) {
            target[property] = value;
        };
    })
});
doLittle.namespace("doLittle", {
    configurator: doLittle.Type.extend(function () {
        this.configure = function (configure) {
        };
    })
});
doLittle.namespace("doLittle", {
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

