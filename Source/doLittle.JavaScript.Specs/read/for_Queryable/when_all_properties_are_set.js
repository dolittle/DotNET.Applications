describe("when all properties are set", function () {
    var query = {
        foo: ko.observable(),
        areAllParametersSet: function () { return false; }
    };
    var paging = {
        size : 0,
        number : 0
    };

    var region = {};
    var pagingInfoType = null;
    var queryService = {
        execute: sinon.mock().withArgs(query, paging).once().returns({
            continueWith: function () { }
        })
    };

    beforeEach(function () {
        pagingInfoType = doLittle.read.PagingInfo;

        doLittle.read.PagingInfo = {
            create: function () {
                return paging;
            }
        };

        var instance = doLittle.read.Queryable.create({
            query: query,
            region: region,
            queryService: queryService,
            targetObservable: {}
        });

        query.areAllParametersSet = function () {return true};
        query.foo(42);
    });

    afterEach(function () {
        doLittle.read.PagingInfo = pagingInfoType;
    });
    
    it("should execute the query on the query service", function () {
        expect(queryService.execute.once().verify()).toBe(true);
    });
});