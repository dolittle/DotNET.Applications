/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using Dolittle.Commands.Validation;
using Dolittle.Validation.MetaData;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.FluentValidation.MetaData.for_ValidationMetaDataGenerator.given
{
    public class a_validation_meta_data_generator_with_common_rules
    {
        protected static ValidationMetaDataGenerator generator;
        protected static Mock<ICommandValidatorProvider> command_validator_provider_mock;

        Establish context = () =>
        {
            var generators = new ICanGenerateRule[]
            {
                new RequiredGenerator(),
                new EmailGenerator(),
                new LessThanGenerator(),
                new GreaterThanGenerator(),
                new NotNullGenerator(),
            };
            var generatorInstances = new Mock<IInstancesOf<ICanGenerateRule>>();
            generatorInstances.Setup(g => g.GetEnumerator()).Returns(new List<ICanGenerateRule>(generators).GetEnumerator());

            command_validator_provider_mock = new Mock<ICommandValidatorProvider>();

            generator = new ValidationMetaDataGenerator(generatorInstances.Object, command_validator_provider_mock.Object);
        };
    }
}