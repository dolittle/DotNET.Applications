describe("when executing and we get two items back", function () {
    var items = [
        { firstItem: 1 },
        { secondItem: 2 }
    ];
    var query = {
        areAllParametersSet: function () {
            return true;
        }
    };
    var observable = ko.observableArray();
    var queryService = null;
    var region = {};

    var pagingInfoType = null;

    var queryable = null;

    beforeEach(function () {
        pagingInfoType = doLittle.read.PagingInfo;
        doLittle.read.PagingInfo = {
            create: function () {
                return {};
            }
        };

        queryService = {
            execute: function () {
                return {
                    continueWith: function (callback) {
                        
                        callback({
                            items: items,
                            totalItems: 4
                        });
                    }
                }
            }
        };

        queryable = doLittle.read.Queryable.create({
            query: query,
            region: region,
            queryService: queryService,
            targetObservable: observable
        });

        queryable.execute();

    });

    afterEach(function () {
        doLittle.read.PagingInfo = pagingInfoType;
    });


    it("should populate the target observable", function () {
        expect(observable()).toBe(items);
    });

    it("should set the total items", function () {
        expect(queryable.totalItems()).toBe(4);
    });
});