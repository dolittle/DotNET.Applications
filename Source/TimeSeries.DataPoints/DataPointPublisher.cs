// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

extern alias contracts;

using System.Threading.Tasks;
using contracts::Dolittle.Runtime.TimeSeries.DataTypes;
using Dolittle.TimeSeries.DataTypes;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static contracts::Dolittle.Runtime.TimeSeries.DataPoints.DataPointStream;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents an implementation of <see cref="IDataPointPublisher"/>.
    /// </summary>
    public class DataPointPublisher : IDataPointPublisher
    {
        readonly AsyncClientStreamingCall<DataPoint, Empty> _streamCall;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPointPublisher"/> class.
        /// </summary>
        /// <param name="client">The <see cref="DataPointStreamClient"/>.</param>
        public DataPointPublisher(DataPointStreamClient client)
        {
            _streamCall = client.Open();
        }

        /// <inheritdoc/>
        public async Task Publish<TValue>(DataPoint<TValue> dataPoint)
            where TValue : IMeasurement
        {
            await _streamCall.RequestStream.WriteAsync(dataPoint.ToRuntime()).ConfigureAwait(false);
        }
    }
}