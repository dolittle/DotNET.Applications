using System.Collections.Generic;
using Dolittle.Applications;
using Dolittle.Runtime.Commands;
using Dolittle.Runtime.Transactions;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Dolittle.Commands.Handling.for_CommandRequestToCommandConverter
{
    public class when_converting_sub_class_with_properties_on_super
    {
        const int an_integer = 42;
        class super : ICommand
        {
            public int an_integer {  get; set; }
        }

        class sub : super
        {

        }

        static TransactionCorrelationId correlation_id;
        static Mock<IApplicationArtifactResolver> application_artifact_resolver;
        static Mock<IApplicationArtifactIdentifier> identifier;
        static CommandRequest request;
        static CommandRequestToCommandConverter converter;
        

        static IDictionary<string, object> content;

        static sub result;

        Establish context = () =>
        {
            correlation_id = TransactionCorrelationId.New();
            identifier = new Mock<IApplicationArtifactIdentifier>();
            
            content = new Dictionary<string, object>
            { { "an_integer", an_integer }
            };

            request = new CommandRequest(correlation_id, identifier.Object, content);

            application_artifact_resolver = new Mock<IApplicationArtifactResolver>();
            application_artifact_resolver.Setup(_ => _.Resolve(identifier.Object)).Returns(typeof(sub));

            converter = new CommandRequestToCommandConverter(application_artifact_resolver.Object);
        };

        Because of = () => result = converter.Convert(request) as sub;

        It should_hold_an_integer = () => result.an_integer.ShouldEqual(an_integer);
    }
}