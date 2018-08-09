using System.Collections.Generic;
using Dolittle.Artifacts;
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
        static Mock<IArtifactTypeMap> artifact_type_map;
        static Artifact identifier;
        static CommandRequest request;
        static CommandRequestToCommandConverter converter;

        static IDictionary<string, object> content;

        static sub result;

        Establish context = () =>
        {
            correlation_id = TransactionCorrelationId.New();
            identifier = Artifact.New();

            content = new Dictionary<string, object>
            { { "an_integer", an_integer }
            };

            request = new CommandRequest(correlation_id, identifier.Id, identifier.Generation, content);

            artifact_type_map = new Mock<IArtifactTypeMap>();
            artifact_type_map.Setup(_ => _.GetTypeFor(identifier)).Returns(typeof(sub));

            converter = new CommandRequestToCommandConverter(artifact_type_map.Object);
        };

        Because of = () => result = converter.Convert(request) as sub;

        It should_hold_an_integer = () => result.an_integer.ShouldEqual(an_integer);
    }
}