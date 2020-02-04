// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Booting;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// The <see cref="ICanPerformBootProcedure">boot procedure</see> for in-process event processors.
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly ProcessMethodEventProcessors _processors;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootProcedure"/> class.
        /// </summary>
        /// <param name="processors">All <see cref="ProcessMethodEventProcessors"/>.</param>
        public BootProcedure(ProcessMethodEventProcessors processors)
        {
            _processors = processors;
        }

        /// <inheritdoc/>
        public bool CanPerform() => Dolittle.Artifacts.Configuration.BootProcedure.HasPerformed;

        /// <inheritdoc/>
        public void Perform()
        {
            _processors.Populate();
        }
    }
}