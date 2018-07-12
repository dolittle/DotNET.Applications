using Dolittle.Applications;

namespace Dolittle.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultApplicationConfiguration
    {
        
        public static DefaultApplicationConfiguration GetDefaultConfiguration()
        {
            return new DefaultApplicationConfiguration(JsonFile.GetConfig());
        }

        public static DefaultApplicationConfiguration GetDefaultConfiguration(Config config)
        {
            return new DefaultApplicationConfiguration(config);
        }

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
        public IBoundedContext BoundedContext {get;}

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

            ApplicationConfiguration = new ApplicationConfigurationBuilder(Config.ApplicationName)
                .Application(applicationBuilder => defaultApplication.ApplicationBuilder)
                .StructureMappedTo(structureMapBuilder => structureMapBuilder
                    .Include(Config.DomainAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.EventsAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.ReadAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.FrontendAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*"))
                .Build();
        }

        DefaultApplicationConfiguration(DefaultApplication applicationConfig)
        {
            Config = applicationConfig.Config;
            BoundedContext = applicationConfig.BoundedContext;

            ApplicationConfiguration = new ApplicationConfigurationBuilder(Config.ApplicationName)
                .Application(applicationBuilder => applicationConfig.ApplicationBuilder)
                .StructureMappedTo(structureMapBuilder => structureMapBuilder
                    .Include(Config.DomainAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.EventsAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.ReadAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*")
                    .Include(Config.FrontendAreaName + ".-^{Module}.-^{Feature}.-^{SubFeature}*"))
                .Build();
        }
    }
}