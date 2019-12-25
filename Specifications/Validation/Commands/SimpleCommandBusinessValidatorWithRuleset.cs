// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Commands.Validation;
using FluentValidation;

namespace Dolittle.FluentValidation.Commands
{
    public class SimpleCommandBusinessValidatorWithRuleset : CommandBusinessValidatorFor<SimpleCommand>
    {
        public const string SERVER_ONLY_RULESET = "ServerOnly";

        public SimpleCommandBusinessValidatorWithRuleset()
        {
            RuleFor(asc => asc.SomeString).NotEmpty();

            RuleSet(SERVER_ONLY_RULESET, () => RuleFor(asc => asc.SomeInt).GreaterThanOrEqualTo(1));
        }
    }
}