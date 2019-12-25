/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Configuration;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents the default configuration for <see cref="RuntimeConfiguration"/> if none is provided
    /// </summary>
    public class RuntimeConfigurationDefaultProvider : ICanProvideDefaultConfigurationFor<RuntimeConfiguration>
    {
        /// <inheritdoc/>
        public RuntimeConfiguration Provide()
        {
            return new RuntimeConfiguration("0.0.0.0", 50053);
        }
    }
}