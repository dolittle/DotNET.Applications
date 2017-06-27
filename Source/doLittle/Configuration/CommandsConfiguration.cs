/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using doLittle.Commands;
using doLittle.DependencyInversion;

namespace doLittle.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="ICommandsConfiguration"/>
    /// </summary>
    public class CommandsConfiguration : ConfigurationStorageElement, ICommandsConfiguration
    {
        /// <inheritdoc/>
        public Type CommandCoordinatorType { get; set; }

        /// <inheritdoc/>
        public override void Initialize(IContainer container)
        {
            if (CommandCoordinatorType != null)
                container.Bind<ICommandCoordinator>(CommandCoordinatorType);

            if (EntityContextConfiguration != null)
            {
                base.Initialize(container);
            }
        }
    }
}