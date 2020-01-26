/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Threading.Tasks;
using Dolittle.Logging;
using Dolittle.TimeSeries.DataPoints;
using Dolittle.TimeSeries.DataTypes;

namespace PullConnector
{
    public class MyProcessor : ICanProcessDataPoints
    {
        readonly IDataPointPublisher _publisher;
        readonly ILogger _logger;

        public MyProcessor(IDataPointPublisher publisher, ILogger logger)
        {
            _publisher = publisher;
            _logger = logger;
        }

        [DataPointProcessor]
        public async Task Tension(DataPoint<Single> dataPoint)
        {
            _logger.Information($"DataPoint received for '{dataPoint.TimeSeries}' with value '{dataPoint.Measurement.Value}' generated @ '{dataPoint.Timestamp}'");

            dataPoint.Measurement.Value *= 10;

            await _publisher.Publish(dataPoint);
        }
    }
}
