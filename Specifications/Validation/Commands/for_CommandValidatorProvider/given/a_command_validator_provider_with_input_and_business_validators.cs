// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Commands;
using Dolittle.Commands.Validation;
using Dolittle.DependencyInversion;
using Dolittle.Logging;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.FluentValidation.Commands.for_CommandValidatorProvider.given
{
    public class a_command_validator_provider_with_input_and_business_validators
    {
        protected static CommandValidatorProvider command_validator_provider;

        protected static Mock<IContainer> container_mock;
        protected static Mock<ITypeFinder> type_finder;

        protected static Type[] command_input_validators = new[]
        {
            typeof(SimpleCommandInputValidator),
            typeof(AnotherSimpleCommandInputValidator)
        };

        protected static Type[] command_business_validators = new[]
        {
            typeof(SimpleCommandBusinessValidator),
            typeof(AnotherSimpleCommandBusinessValidator)
        };

        protected static Type[] input_validators = new[]
        {
            typeof(LongConceptInputValidator),
            typeof(StringConceptInputValidator)
        };

        protected static Type[] business_validators = new[]
        {
            typeof(LongConceptBusinessValidator),
            typeof(StringConceptBusinessValidator)
        };

        Establish context = () =>
        {
            container_mock = new Mock<IContainer>();
            type_finder = new Mock<ITypeFinder>();

            type_finder.Setup(td => td.FindMultiple(typeof(ICommandInputValidator)))
                .Returns(new[]
                        {
                            typeof(SimpleCommandInputValidator),
                            typeof(AnotherSimpleCommandInputValidator),
                            typeof(NullCommandInputValidatorFor<ICommand>)
                        });

            type_finder.Setup(td => td.FindMultiple(typeof(ICommandBusinessValidator)))
                .Returns(new[]
                        {
                            typeof(SimpleCommandBusinessValidator),
                            typeof(AnotherSimpleCommandBusinessValidator),
                            typeof(NullCommandBusinessValidatorFor<ICommand>)
                        });

            type_finder.Setup(td => td.FindMultiple(typeof(ICommandInputValidator)))
                .Returns(new[]
                        {
                            typeof(SimpleCommandInputValidator),
                            typeof(AnotherSimpleCommandInputValidator),
                            typeof(NullCommandInputValidatorFor<ICommand>)
                        });

            type_finder.Setup(td => td.FindMultiple(typeof(ICommandBusinessValidator)))
                .Returns(new[]
                        {
                            typeof(SimpleCommandBusinessValidator),
                            typeof(AnotherSimpleCommandBusinessValidator),
                            typeof(NullCommandBusinessValidatorFor<ICommand>)
                        });

            command_validator_provider = new CommandValidatorProvider(
                type_finder.Object,
                container_mock.Object,
                Mock.Of<ILogger>());
        };
    }
}