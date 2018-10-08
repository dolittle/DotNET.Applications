using System.IO;
using Dolittle.Bootstrapping;
using Dolittle.Collections;
using Dolittle.Execution;
using Dolittle.Resources.Configuration;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Performs the boot procedures for the application configuration
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        IBoundedContextLoader _boundedContextLoader;
        IExecutionContextManager _executionContextManager;
        IResourceConfiguration _resourceConfiguration;
        /// <summary>
        /// Instantiates an instance of <see cref="BootProcedure"/>
        /// </summary>
        /// <param name="boundedContextLoader"></param>
        /// <param name="executionContextManager"></param>
        /// <param name="resourceConfiguration"></param>
        public BootProcedure(IBoundedContextLoader boundedContextLoader, IExecutionContextManager executionContextManager, IResourceConfiguration resourceConfiguration )
        {
            _boundedContextLoader = boundedContextLoader;
            _executionContextManager = executionContextManager;
            _resourceConfiguration = resourceConfiguration;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            var boundedContextConfig = _boundedContextLoader.Load(Path.Combine("..", "bounded-context.json"));
            boundedContextConfig.Resources.ForEach(kvp => 
            {
                var resourceType = kvp.Key;
                var resourceTypeImplementation = _executionContextManager.Current.Environment.Value == "Production"? kvp.Value.Production : kvp.Value.Development;

                _resourceConfiguration.SetResourceType(resourceType, resourceTypeImplementation);
            });
        }
    }
}