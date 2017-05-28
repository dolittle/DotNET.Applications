describe("when executing and we get undefined back", function () {
    var query = {
        areAllParametersSet: function () {
            return true;
        }
    };
    var observable = ko.observableArray();
    var queryService = null;

    var pagingInfoType = null;
    var region = {};

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
                        callback(undefined);
                    }
                }
            }
        };

        var queryable = doLittle.read.Queryable.create({
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
        expect(observable()).toEqual([]);
    });

});