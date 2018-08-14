/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.DependencyInversion;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// Represents bindings for this module
    /// </summary>
    public class Bindings : ICanProvideBindings
    {
        /// <inheritdoc/>
        public void Provide(IBindingProviderBuilder builder)
        {
            var boundedContextConfigurationManager = new BoundedContextConfigurationManager();
            builder.Bind<IBoundedContextConfigurationManager>().To(boundedContextConfigurationManager);
            builder.Bind<BoundedContextConfiguration>().To(boundedContextConfigurationManager.Current);
            builder.Bind<Application>().To(boundedContextConfigurationManager.Current.Application);
            builder.Bind<BoundedContext>().To(boundedContextConfigurationManager.Current.BoundedContext);          
        }
    }
}