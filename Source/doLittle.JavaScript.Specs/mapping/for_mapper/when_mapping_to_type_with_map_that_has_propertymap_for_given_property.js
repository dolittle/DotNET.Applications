describe("when mapping to type with map that has propertymap for given property", function () {
    var data = { number: 42 };

    var mapStub = {
        canMapProperty: sinon.stub().returns(true),
        mapProperty: sinon.stub()
    };

    var parameters = {
        typeConverters: {},
        maps: {
            hasMapFor: sinon.stub().returns(true),
            getMapFor: sinon.stub().returns(mapStub)
        }
    };

    var type = doLittle.Type.extend(function () {
        this.number = 0;
    });

    var mapper = doLittle.mapping.mapper.create(parameters);
    var mappedInstance = mapper.map(type, data);

    it("should map the property through the map", function () {
        expect(mapStub.mapProperty.calledWith("number", data, mappedInstance)).toBe(true);
    });
});