describe("when requested dependency is a valid script in namespace, but not registered in doLittle.namespace", function () {
    var resolver = new doLittle.DefaultDependencyResolver();
    var ns;
    var resolved = null;
    var actualResolved = null;
    var requireStub;
    var requireArg;
    var canResolve;
    var systemResolved = {
        someValue: "value"
    };
    var fileFactory = null;
    var fileManager = null;

    var file = {
        some: "file"
    };
    
    var fileFactoryMock = {
        create: sinon.mock().withArgs("/Someplace/On/Server/something.js", doLittle.io.fileType.javaScript).returns(file)
    };
    var fileManagerMock = {
        load: sinon.mock().withArgs([file]).returns({
            continueWith: function (callback) {
                callback([systemResolved]);
            }
        })
    };

    beforeEach(function () {
        ns = {
            _path: "/Someplace/On/Server",
            _scripts: ["something"]
        };

        fileFactory = doLittle.io.fileFactory;
        fileManager = doLittle.io.fileManager;

        doLittle.io.fileFactory = {
            create: sinon.stub().returns(fileFactoryMock)
        };

        doLittle.io.fileManager = {
            create: sinon.stub().returns(fileManagerMock)
        }
        
        canResolve = resolver.canResolve(ns, "something");
        resolved = resolver.resolve(ns, "something");
        resolved.continueWith(function (arg, nextPromise) {
            actualResolved = arg;
        });
    });

    afterEach(function () {
        doLittle.io.fileFactory = fileFactory;
        doLittle.io.fileManager = fileManager;
    });

    it("should be able to resolve", function () {
        expect(canResolve).toBe(true);
    });

    it("should create a file", function () {
        expect(fileFactoryMock.create.called).toBe(true);
    });

    it("should return a promise", function () {
        expect(resolved instanceof doLittle.execution.Promise).toBe(true);
    });

    it("should resolve system loaded into namespace", function () {
        expect(actualResolved).toBe(systemResolved);
    });
});