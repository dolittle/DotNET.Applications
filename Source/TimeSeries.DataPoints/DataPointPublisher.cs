// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Dolittle.Heads;
using Dolittle.TimeSeries.DataTypes;
using Dolittle.TimeSeries.DataTypes.Runtime;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using static Dolittle.TimeSeries.DataPoints.Runtime.DataPointStream;

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
        /// <param name="client"><see cref="IClientFor{T}"/> <see cref="DataPointStreamClient"/>.</param>
        public DataPointPublisher(IClientFor<DataPointStreamClient> client)
        {
            _streamCall = client.Instance.Open();
        }

        /// <inheritdoc/>
        public async Task Publish<TValue>(DataPoint<TValue> dataPoint)
            where TValue : IMeasurement
        {
            await _streamCall.RequestStream.WriteAsync(dataPoint.ToRuntime()).ConfigureAwait(false);
        }
    }
}