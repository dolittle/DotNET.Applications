using Dolittle.Applications;

namespace Dolittle.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultApplication
    {
        /// <summary>
        /// Gets the default <see cref="IApplicationBuilder"/> for a given <see cref="Config"/> with <see cref="BoundedContext"/> as prefix
        /// </summary>
        /// <param name="config">The configuration</param>
        /// <param name="boundedContext">The <see cref="BoundedContext"/> to use as location prefix</param>
        /// <returns></returns>
        public static IApplicationBuilder GetDefaultApplicationBuilderForConfig(Config config, BoundedContext boundedContext)
        {
            return new ApplicationBuilder(config.Application)
                .PrefixLocationsWith(boundedContext)
                .WithStructureStartingWith<BoundedContext>(bc => bc.Required
                    .WithChild<Module>(m => m
                        .WithChild<Feature>(f => f
                            .WithChild<SubFeature>(sf => sf.Recursive))));
        }
        /// <summary>
        /// Gets the <see cref="DefaultApplication"/> based on the configuration details specified in the dolittle configuration json file.
        /// </summary>
        public static DefaultApplication GetDefaultApplication()
        {
            return new DefaultApplication(JsonFile.GetConfig());    
        }

        /// <summary>
        /// Gets the <see cref="DefaultApplication"/> based on the given <see cref="Config"/>
        /// </summary>
        public static DefaultApplication GetDefaultApplication(Config config)
        {
            return new DefaultApplication(config);    
        }

        /// <summary>
        /// Gets the <see cref="Config"/> the <see cref="DefaultApplication"/> is built from
        /// </summary>
        public Config Config {get; } 

        /// <summary>
        /// Gets the default <see cref="IApplicationBuilder"/>.
        /// The default structure consists of a required <see cref ="BoundedContext"/> follow by a optional <see cref="Module"/> followed by an optional <see cref="Feature"/>
        /// followed by a recursive optional <see cref ="SubFeature"/> 
        /// </summary>
        /// <value></value>
        public IApplicationBuilder ApplicationBuilder {get; }

        /// <summary>
        /// The default <see cref="IBoundedContext"/> that's generated.
        /// </summary>
        public BoundedContext BoundedContext {get; }

        DefaultApplication(Config config)
        {
            ThrowIfNoConfigurationDetails(config);

            Config = config;
            BoundedContext = new BoundedContext(config.BoundedContext);

            ApplicationBuilder = GetDefaultApplicationBuilderForConfig(config, BoundedContext);

        }

        /// <summary>
        /// Gets the built <see cref="IApplication"/> from the <see cref="IApplicationBuilder"/>
        /// </summary>
        /// <returns></returns>
        public IApplication Application => ApplicationBuilder.Build();

        void ThrowIfNoConfigurationDetails(Config config)
        {
            if (config.Application.Equals(ApplicationName.NotSet) 
                || config.BoundedContext.Equals(BoundedContextName.NotSet))
            {
                throw new ConfigurationNotSpecified();
            }
        }
    }
}