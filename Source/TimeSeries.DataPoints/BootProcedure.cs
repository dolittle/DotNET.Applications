// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Booting;
using Dolittle.Heads;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents the <see cref="ICanPerformBootProcedure">boot procedure</see> for dealing
    /// with data points.
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly IDataPointsProcessors _processors;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootProcedure"/> class.
        /// </summary>
        /// <param name="processors"><see cref="IDataPointsProcessors"/> in the system.</param>
        public BootProcedure(IDataPointsProcessors processors)
        {
            _processors = processors;
        }

        /// <inheritdoc/>
        public bool CanPerform() => Head.Connected;

        /// <inheritdoc/>
        public void Perform()
        {
            _processors.Start();
        }
    }
}