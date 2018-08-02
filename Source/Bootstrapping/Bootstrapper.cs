/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Collections;
using Dolittle.DependencyInversion;
using Dolittle.Types;

namespace Dolittle.Bootstrapping
{
    /// <summary>
    /// Represents the main bootstrapper that enables systems to be called during booting of the system
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// Start the boot
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> for getting instances</param>
        public static void Start(IContainer container)
        {
            var procedures = container.Get<IInstancesOf<ICanPerformBootProcedure>>();
            procedures.ForEach(_ => _.Perform());
        }
    }
}