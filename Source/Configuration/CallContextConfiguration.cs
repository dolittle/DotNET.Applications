/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using doLittle.Execution;
using doLittle.DependencyInversion;

namespace doLittle.Configuration
{
    /// <summary>
    /// Represents the configuration for <see cref="ICallContext"/>
    /// </summary>
    public class CallContextConfiguration : ICallContextConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CallContextConfiguration"/>
        /// </summary>
        public CallContextConfiguration()
        {
            CallContextType = typeof(DefaultCallContext);
        }

        /// <inheritdoc/>
        public Type CallContextType { get; set; }

        /// <inheritdoc/>
        public void Initialize(IContainer container)
        {
            container.Bind<ICallContext>(CallContextType);
        }
    }
}
