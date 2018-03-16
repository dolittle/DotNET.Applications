using Dolittle.Commands.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation.Specs.Commands
{
    public class SimpleCommandInputValidatorWithRuleset : CommandInputValidatorFor<SimpleCommand>
    {
        public const string SERVER_ONLY_RULESET = "ServerOnly";

        public SimpleCommandInputValidatorWithRuleset()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();

            RuleSet(SERVER_ONLY_RULESET, () =>
            {
                RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1);
            });
            
        }
    }
}