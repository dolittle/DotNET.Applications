describe("when both sides are not satisfied but they become satisfied", function () {
    
    var leftHandSideEvaluator = ko.observable(false);
    var leftHandSide = doLittle.specifications.Specification.create()
    leftHandSide.evaluator = leftHandSideEvaluator;

    var rightHandSideEvaluator = ko.observable(false);
    var rightHandSide = doLittle.specifications.Specification.create();
    rightHandSide.evaluator = rightHandSideEvaluator;

    var instance = { something: 42 };
    var rule = doLittle.specifications.Or.create({
        leftHandSide: leftHandSide,
        rightHandSide: rightHandSide
    });
    rule.evaluate(instance);

    var result = false;
    rule.isSatisfied.subscribe(function (newValue) {
        result = newValue;
    });
    result = false;

    leftHandSideEvaluator(true);
    rightHandSideEvaluator(true);

    it("should be considered satisfied", function () {
        expect(result).toBe(true);
    });
});