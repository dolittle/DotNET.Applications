// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Defines a system that knows about <see cref="ICanProcessDataPoints"/>.
    /// </summary>
    public interface IDataPointsProcessors
    {
        /// <summary>
        /// Starts all <see cref="ICanProcessDataPoints">processors</see>.
        /// </summary>
        void Start();

        /// <summary>
        /// Get a <see cref="DataPointProcessor"/> by its <see cref="DataPointProcessorId">unique identifier</see>.
        /// </summary>
        /// <param name="id"><see cref="DataPointProcessorId"/> to get by.</param>
        /// <returns>The <see cref="DataPointProcessor"/> for the id.</returns>
        DataPointProcessor GetById(DataPointProcessorId id);
    }
}