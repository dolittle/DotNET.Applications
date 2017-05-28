describe("when mapping to type with matching observable string property", function(){
    var data = { stringProperty: "fourty two" };

	var parameters = {
	    typeConverters: {},
	    maps: { hasMapFor: sinon.stub().returns(false) }
	};

	var type = doLittle.Type.extend(function () {
	    this.stringProperty = ko.observable("");
	});

	var mappedInstance = null;

	(function becauseOf(){
		var mapper = doLittle.mapping.mapper.create(parameters);
		mappedInstance = mapper.map(type, data);
	})();

	it("should map the corresponding objectProperty", function(){
	    expect(mappedInstance.stringProperty()).toEqual(data.stringProperty);
	});

});