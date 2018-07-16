using Dolittle.Logging;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Applications.for_ApplicationArtifactsResolver.given
{
    public class a_standard_configuration
    {
         protected static BoundedContext bounded_context;
        protected static (IApplication application, IApplicationStructureMap structure_map) application_configuration;
        protected static IApplicationLocationResolver location_resolver;
        protected static Mock<ITypeFinder> type_finder;
        protected static Mock<ILogger> logger;


        Establish context = () => 
        {   
            bounded_context = new BoundedContext("DefaultBoundedContext");
            application_configuration = new ApplicationConfigurationBuilder("DefaultApplication")
                .Application(appBuilder => appBuilder
                .PrefixLocationsWith(bounded_context)
                .WithStructureStartingWith<BoundedContext>(bc => bc.Required
                    .WithChild<Module>(m => m.Required
                        .WithChild<Feature>(f => f
                            .WithChild<SubFeature>(sf => sf.Recursive)))))
                .StructureMappedTo(mapBuilder => mapBuilder
                    .Include("Dolittle.^{Module}.-^{Feature}.-^{SubFeature}*"))
                .Build();

            location_resolver = new ApplicationLocationResolver(application_configuration.application, application_configuration.structure_map);

            type_finder = new Mock<ITypeFinder>();
            logger = new Mock<ILogger>();
            
        };
    }
}