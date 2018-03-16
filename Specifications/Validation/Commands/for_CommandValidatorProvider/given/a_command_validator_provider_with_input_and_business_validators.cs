using System;
using Dolittle.Commands;
using Dolittle.DependencyInversion;
using Dolittle.Commands.Validation;
using Dolittle.Types;
using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.FluentValidation.Specs.Commands.for_CommandValidatorProvider.given
{
    public class a_command_validator_provider_with_input_and_business_validators : commands
    {
        protected static CommandValidatorProvider command_validator_provider;

        protected static Mock<IContainer> container_mock;
        protected static Mock<ITypeFinder> type_finder;


        protected static Type[] command_input_validators = new[] {
                                                               typeof(SimpleCommandInputValidator),
                                                               typeof(AnotherSimpleCommandInputValidator)
                                                         };
        protected static Type[] command_business_validators = new[] {
                                                                typeof(SimpleCommandBusinessValidator),
                                                                typeof(AnotherSimpleCommandBusinessValidator)
                                                            };

        protected static Type[] input_validators = new[] {
                                                               typeof(LongConceptInputValidator),
                                                               typeof(StringConceptInputValidator)
                                                         };
        protected static Type[] business_validators = new[] {
                                                                typeof(LongConceptBusinessValidator),
                                                                typeof(StringConceptBusinessValidator)
                                                            };

        Establish context = () =>
                                {
                                    container_mock = new Mock<IContainer>();
                                    type_finder = new Mock<ITypeFinder>();

                                    type_finder.Setup(td => td.FindMultiple(typeof (ICommandInputValidator)))
                                        .Returns(new []
                                                {
                                                    typeof(SimpleCommandInputValidator),
                                                    typeof(AnotherSimpleCommandInputValidator),
                                                    typeof(NullCommandInputValidatorFor<ICommand>)
                                                }
                                        );

                                    type_finder.Setup(td => td.FindMultiple(typeof (ICommandBusinessValidator)))
                                        .Returns(new []
                                                {
                                                    typeof(SimpleCommandBusinessValidator),
                                                    typeof(AnotherSimpleCommandBusinessValidator),
                                                    typeof(NullCommandBusinessValidatorFor<ICommand>)
                                                }
                                        );

                                    type_finder.Setup(td => td.FindMultiple(typeof(ICommandInputValidator)))
                                        .Returns(new[]
                                                {
                                                    typeof(SimpleCommandInputValidator),
                                                    typeof(AnotherSimpleCommandInputValidator),
                                                    typeof(NullCommandInputValidatorFor<ICommand>)
                                                }
                                        );

                                    type_finder.Setup(td => td.FindMultiple(typeof(ICommandBusinessValidator)))
                                        .Returns(new[]
                                                {
                                                    typeof(SimpleCommandBusinessValidator),
                                                    typeof(AnotherSimpleCommandBusinessValidator),
                                                    typeof(NullCommandBusinessValidatorFor<ICommand>)
                                                }
                                        );


                                    command_validator_provider = new CommandValidatorProvider(
                                        type_finder.Object,
                                        container_mock.Object,
                                        Mock.Of<ILogger>()
                                    );
                                };
    }
}