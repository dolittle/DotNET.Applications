using System;
using System.Linq.Expressions;
using Dolittle.Validation;
using FluentValidation.Validators;

namespace Dolittle.FluentValidation.for_RuleBuilderExtensions
{
    public class FakePropertyValidatorWithDynamicState : PropertyValidatorWithDynamicState
    {

        public FakePropertyValidatorWithDynamicState() : base("") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            return true;
        }


        public bool AddExpressionCalled = false;

        public override void AddExpression<T>(Expression<Func<T, object>> expression)
        {
            AddExpressionCalled = true;
        }
    }
}
