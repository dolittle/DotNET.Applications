describe("when creating asynchronously with one dependency and other parameters", function () {

    doLittle.dependencyResolver = {
        beginResolve: function (namespace, name) {
            var promise = doLittle.execution.Promise.create();
            promise.signal(name);
            return promise;
        },
        getDependenciesFor: function () {
            return ["first","options"];
        }
    }

    var type = doLittle.Type.extend(function (first, options) {
        this.something = "Hello";
        this.first = first;
        this.options = options;
    });


    var result = null;
    type.beginCreate({
        options: {
            value: "Hello"
        }
    }).continueWith(function (parameter, nextPromise) {
        result = parameter;
    });

    it("should resolve the first dependency", function () {
        expect(result.first).toBe("first");
    });

    it("should pass along the options", function () {
        
        expect(result.options.value).toBe("Hello");
    });
});