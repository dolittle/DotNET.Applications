/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Booting;
using Dolittle.Types;

namespace Dolittle.Events.Processing
{
    /// <summary>
    /// The <see cref="ICanPerformBootProcedure">boot procedure</see> for in-process event processors
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly ProcessMethodEventProcessors _processors;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processors"></param>
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