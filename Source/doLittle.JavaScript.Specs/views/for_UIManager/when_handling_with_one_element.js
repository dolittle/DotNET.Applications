describe("when handling with one element", function() {
	var root = document.createElement("div");
	var element = document.createElement("button");
	
	var documentService = {
		traverseObjects: function(callback) {
			callback(element);
		}
	};

	var visitStub = sinon.stub();

	var visitorType = doLittle.markup.ElementVisitor.extend(function() {
		this.visit = visitStub;
	});
	var actions = { some: "actions" };

	beforeEach(function() {
	    sinon.stub(doLittle.markup.ElementVisitor, "getExtenders").returns([visitorType]);
	    sinon.stub(doLittle.markup.ElementVisitorResultActions, "create").returns(actions);

	    var instance = doLittle.views.UIManager.createWithoutScope({
			documentService: documentService
		})

		instance.handle(root);
	});

	afterEach(function() {
	    doLittle.markup.ElementVisitor.getExtenders.restore();
	    doLittle.markup.ElementVisitorResultActions.create.restore();
	});

	it("should call the visit function of the visitor", function() {
		expect(visitStub.calledWith(element, actions)).toBe(true);
	});
});