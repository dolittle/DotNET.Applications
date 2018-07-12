using Dolittle.Applications;

namespace Dolittle.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultApplication
    {

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
        public IBoundedContext BoundedContext {get; }

        DefaultApplication(Config config)
        {
            ThrowIfNoConfigurationDetails(config);
            
            Config = config;
            BoundedContext = new BoundedContext(config.BoundedContextName);

            ApplicationBuilder = new ApplicationBuilder(config.ApplicationName)
                .PrefixLocationsWith(BoundedContext)
                .WithStructureStartingWith<BoundedContext>(bc => bc.Required
                    .WithChild<Module>(m => m
                        .WithChild<Feature>(f => f
                            .WithChild<SubFeature>(sf => sf.Recursive))));

        }

        /// <summary>
        /// Gets the built <see cref="IApplication"/> from the <see cref="IApplicationBuilder"/>
        /// </summary>
        /// <returns></returns>
        public IApplication Application => ApplicationBuilder.Build();

        void ThrowIfNoConfigurationDetails(Config config)
        {
            if (config.ApplicationName.Equals(ApplicationName.NotSet) 
                || config.BoundedContextName.Equals(BoundedContextName.NotSet))
            {
                throw new ConfigurationNotSpecified();
            }
        }
    }
}