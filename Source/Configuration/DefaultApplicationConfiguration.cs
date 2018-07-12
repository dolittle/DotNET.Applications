using Dolittle.Applications;

namespace Dolittle.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultApplicationConfiguration
    {
        
        /// <summary>
        /// Gets the <see cref="DefaultApplicationConfiguration"/> based on the configuration details specified in the dolittle configuration json file.
        /// </summary>
        public static DefaultApplicationConfiguration GetDefaultConfiguration()
        {
            return new DefaultApplicationConfiguration(JsonFile.GetConfig());
        }

        /// <summary>
        /// Gets the <see cref="DefaultApplicationConfiguration"/> based on the given <see cref="Config"/>
        /// </summary>
        public static DefaultApplicationConfiguration GetDefaultConfiguration(Config config)
        {
            return new DefaultApplicationConfiguration(config);
        }

        /// <summary>
        /// Gets the <see cref="DefaultApplicationConfiguration"/> based on the given <see cref="DefaultApplication"/>
        /// </summary>

        public static DefaultApplicationConfiguration GetDefaultConfiguration(DefaultApplication applicationConfig)
        {
            return new DefaultApplicationConfiguration(applicationConfig);
        }

        /// <summary>
        /// Gets the <see cref="Config"/> the <see cref="DefaultApplicationConfiguration"/> is built from
        /// </summary>        
        public Config Config {get; }

        /// <summary>
        /// The default <see cref="IBoundedContext"/> that's generated.
        /// </summary>
        public IBoundedContext BoundedContext {get; }

        /// <summary>
        /// The default <see cref="IApplication"/> and <see cref="IApplicationStructureMap"/> based on the provided configuration details.
        /// </summary>
        /// <value></value>
        public (IApplication application, IApplicationStructureMap structureMap) ApplicationConfiguration {get; }


        DefaultApplicationConfiguration(Config config)
        {
            Config = config;

            var defaultApplication = DefaultApplication.GetDefaultApplication(Config);

            BoundedContext = defaultApplication.BoundedContext;

            ApplicationConfiguration = new ApplicationConfigurationBuilder(Config.Application)
                .Application(applicationBuilder => defaultApplication.ApplicationBuilder)
                .StructureMappedTo(structureMapBuilder => structureMapBuilder
                    .Include(Config.DomainAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.EventsAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.ReadAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.FrontendAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*"))
                .Build();
        }

        DefaultApplicationConfiguration(DefaultApplication defaultApplication)
        {
            Config = defaultApplication.Config;
            BoundedContext = defaultApplication.BoundedContext;

            ApplicationConfiguration = new ApplicationConfigurationBuilder(Config.Application)
                .Application(applicationBuilder => defaultApplication.ApplicationBuilder)
                .StructureMappedTo(structureMapBuilder => structureMapBuilder
                    .Include(Config.DomainAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.EventsAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.ReadAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.FrontendAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*"))
                .Build();
        }
    }
}