namespace Dolittle.Events
{
    using Dolittle.DependencyInversion;
    using Dolittle.Runtime.Events.Coordination;
    using Dolittle.Events.Coordination;

    /// <summary>
    /// Bindings for hooking Events implementations to Runtime Interfaces
    /// </summary>
    public class EventsBindings : ICanProvideBindings 
    {
        /// <inheritdoc />
        public void Provide(IBindingProviderBuilder builder)
        {
            new BindingBuilder(Binding.For(typeof(IUncommittedEventStreamCoordinator))).To<Dolittle.Events.Coordination.UncommittedEventStreamCoordinator>().Build();
        }
    }
}