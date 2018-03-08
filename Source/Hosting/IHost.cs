/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using doLittle.DependencyInversion;

namespace doLittle.Hosting
{
    /// <summary>
    /// Defines a simple host
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Gets the <see cref="IContainer"/>
        /// </summary>
        IContainer Container { get; }

        /// <summary>
        /// Run the host
        /// </summary>
        void Run();
    }
}