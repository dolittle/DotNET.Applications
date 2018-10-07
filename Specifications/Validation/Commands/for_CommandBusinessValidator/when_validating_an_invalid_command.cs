/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Dolittle.Commands.Validation;
using Dolittle.Validation;
using Machine.Specifications;

namespace Dolittle.FluentValidation.Commands.for_CommandBusinessValidator
{
    [Subject(typeof(CommandInputValidatorFor<>))]
    public class when_validating_an_invalid_command : given.a_command_business_validator
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
        {
            simple_command.SomeString = string.Empty;
            simple_command.SomeInt = -1;
        };

        Because of = () => results = simple_command_business_validator.ValidateFor(simple_command);

        It should_have_invalid_properties = () => results.Count().ShouldEqual(2);
    }

    [Subject(typeof(CommandInputValidatorFor<>))]
    public class when_validating_an_invalid_property_in_the_ruleset_and_ruleset_is_not_specified : given.a_command_business_validator_with_ruleset
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
        {
            simple_command.SomeString = "And to him only shall be given...";
            simple_command.SomeInt = -1;
        };

        Because of = () => results = simple_command_business_validator.ValidateFor(simple_command);

        It should_not_have_invalid_properties = () => results.Any().ShouldBeFalse();
    }

    [Subject(typeof(CommandInputValidatorFor<>))]
    public class when_validating_an_invalid_property_in_the_ruleset_and_ruleset_is_specified : given.a_command_business_validator_with_ruleset
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
        {
            simple_command.SomeString = "Alms for an ex-leper";
            simple_command.SomeInt = -1;
        };

        Because of = () => results = simple_command_business_validator.ValidateFor(simple_command, SimpleCommandInputValidatorWithRuleset.SERVER_ONLY_RULESET);

        It should_have_an_invalid_property= () => results.Count().ShouldEqual(1);
    }

    [Subject(typeof(CommandInputValidatorFor<>))]
    public class when_validating_an_invalid_property_not_in_the_ruleset_and_ruleset_is_not_specified : given.a_command_business_validator_with_ruleset
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
        {
            simple_command.SomeString = string.Empty;
            simple_command.SomeInt = 10;
        };

        Because of = () => results = simple_command_business_validator.ValidateFor(simple_command);

        It should_have_an_invalid_property = () => results.Count().ShouldEqual(1);
    }

    [Subject(typeof(CommandInputValidatorFor<>))]
    public class when_validating_an_invalid_property_not_in_the_ruleset_and_ruleset_is_specified_with_no_default_ruleset : given.a_command_business_validator_with_ruleset
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
        {
            simple_command.SomeString = string.Empty;
            simple_command.SomeInt = 10;
        };

        Because of = () => results = simple_command_business_validator.ValidateFor(simple_command, SimpleCommandInputValidatorWithRuleset.SERVER_ONLY_RULESET, includeDefaultRuleset: false);

        It should_not_have_an_invalid_property = () => results.Any().ShouldBeFalse();
    }

    [Subject(typeof(CommandInputValidatorFor<>))]
    public class when_validating_an_invalid_property_not_in_the_ruleset_and_ruleset_is_specified_along_with_the_default_ruleset : given.a_command_business_validator_with_ruleset
    {
        static IEnumerable<ValidationResult> results;

        Establish context = () =>
        {
            simple_command.SomeString = string.Empty;
            simple_command.SomeInt = 10;
        };

        Because of = () => results = simple_command_business_validator.ValidateFor(simple_command, SimpleCommandInputValidatorWithRuleset.SERVER_ONLY_RULESET);

        It should_have_an_invalid_property = () => results.Count().ShouldEqual(1);
    }
}