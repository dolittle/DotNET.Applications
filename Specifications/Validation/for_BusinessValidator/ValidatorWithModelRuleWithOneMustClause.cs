using FluentValidation;

namespace Dolittle.FluentValidation.Specs.for_BusinessValidator
{
    public class ValidatorWithModelRuleWithOneMustClause : BusinessValidator<SimpleObject>
    {
        public bool CallbackCalled = false;
        public ValidatorWithModelRuleWithOneMustClause()
        {
            ModelRule().Must(o => CallbackCalled = true);
        }
    }
}
