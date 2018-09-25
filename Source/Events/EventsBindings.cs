namespace Dolittle.Events
{
    using Dolittle.DependencyInversion;
    using Dolittle.Runtime.Events.Coordination;
    using Dolittle.Events.Coordination;
    using System;

    /// <summary>
    /// Bindings for hooking Events implementations to Runtime Interfaces
    /// </summary>
    public class EventsBindings : ICanProvideBindings 
    {
        /// <inheritdoc />
        public void Provide(IBindingProviderBuilder builder)
        {
            builder.Bind<IUncommittedEventStreamCoordinator>().To<UncommittedEventStreamCoordinator>();            
        }
    }
}