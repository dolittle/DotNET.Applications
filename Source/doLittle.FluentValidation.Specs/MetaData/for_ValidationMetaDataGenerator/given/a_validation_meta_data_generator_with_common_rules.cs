using System.Collections.Generic;
using doLittle.Execution;
using doLittle.FluentValidation.Commands;
using doLittle.FluentValidation.MetaData;
using Machine.Specifications;
using Moq;

namespace doLittle.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator.given
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