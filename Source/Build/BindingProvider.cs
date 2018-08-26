using Dolittle.Commands;
using Dolittle.DependencyInversion;
using Dolittle.Events;
using Dolittle.Events.Processing;
using Dolittle.Queries;
using Dolittle.ReadModels;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;

namespace Dolittle.Build
{
    /// <summary>
    /// A class providing the startup bindings
    /// </summary>
    public class BindingProvider : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<DolittleArtifactTypes>().To(new DolittleArtifactTypes());
        }
    }
}