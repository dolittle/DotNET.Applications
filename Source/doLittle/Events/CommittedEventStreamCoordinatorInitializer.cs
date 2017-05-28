/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Configuration;

namespace doLittle.Events
{
    /// <summary>
    /// Represents a system that can initialize the <see cref="ICommittedEventStreamCoordinator"/>
    /// </summary>
    public class CommittedEventStreamCoordinatorInitializer : IWantToKnowWhenConfigurationIsDone
    {
        /// <inheritdoc/>
        public void Configured(IConfigure configure)
        {
            configure.Container.Get<ICommittedEventStreamCoordinator>().Initialize();
        }
    }
}
