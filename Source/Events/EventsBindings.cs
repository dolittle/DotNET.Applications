// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.DependencyInversion;
using Dolittle.Runtime.Events.Coordination;

namespace Dolittle.Events
{
    /// <summary>
    /// Bindings for hooking Events implementations to Runtime Interfaces.
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