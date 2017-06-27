/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.DependencyInversion;

namespace doLittle.Configuration
{
    /// <summary>
    /// Represents an implementation of a <see cref="IFrontendConfiguration"/>
    /// </summary>
    public class FrontendConfiguration : IFrontendConfiguration
    {
        /// <inheritdoc/>
        public IFrontendTargetConfiguration Target { get; set; }

        /// <inheritdoc/>
        public void Initialize(IContainer container)
        {
            if (Target != null)
                Target.Initialize(container);

        }
    }
}
