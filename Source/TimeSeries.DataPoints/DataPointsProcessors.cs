// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.Heads;
using Dolittle.Lifecycle;
using Dolittle.Logging;
using Dolittle.Protobuf;
using Dolittle.TimeSeries.DataTypes;
using Dolittle.Types;
using Grpc.Core;
using static Dolittle.TimeSeries.DataPoints.Runtime.DataPointProcessors;
using grpc = Dolittle.TimeSeries.DataPoints.Runtime;

namespace Dolittle.TimeSeries.DataPoints
{
    /// <summary>
    /// Represents an implementation of <see cref="IDataPointsProcessors"/>.
    /// </summary>
    [Singleton]
    public class DataPointsProcessors : IDataPointsProcessors
    {
        readonly IInstancesOf<ICanProcessDataPoints> _processors;
        readonly ConcurrentDictionary<DataPointProcessorId, DataPointProcessor> _dataProcessors = new ConcurrentDictionary<DataPointProcessorId, DataPointProcessor>();
        readonly ILogger _logger;
        readonly IClientFor<DataPointProcessorsClient> _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPointsProcessors"/> class.
        /// </summary>
        /// <param name="processors">Instances of <see cref="ICanProcessDataPoints">processors</see>.</param>
        /// <param name="client"><see cref="IClientFor{T}">Client</see> for <see cref="DataPointProcessorsClient"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public DataPointsProcessors(
            IInstancesOf<ICanProcessDataPoints> processors,
            IClientFor<DataPointProcessorsClient> client,
            ILogger logger)
        {
            _processors = processors;
            _logger = logger;
            _client = client;
        }

        /// <inheritdoc/>
        public void Start()
        {
            Discover();

            _dataProcessors.Values.ForEach(_ =>
            {
                _logger.Information($"Opening data processor {_.Id}");

                Task.Run(async () =>
                {
                    try
                    {
                        var streamingCall = _client.Instance.Open(new grpc.DataPointProcessor
                        {
                            Id = _.Id.ToProtobuf()
                        });

                        await Process(_, streamingCall.ResponseStream).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, $"Couldn't open data point processor '{_.Id}'");
                        Environment.Exit(1);
                    }
                });
            });
        }

        /// <inheritdoc/>
        public DataPointProcessor GetById(DataPointProcessorId id)
        {
            return _dataProcessors[id];
        }

        async Task Process(DataPointProcessor processor, IAsyncStreamReader<grpc.DataPoints> streamReader)
        {
            while (await streamReader.MoveNext().ConfigureAwait(false))
            {
                var dataPoints = streamReader.Current;

                try
                {
                    foreach (var dataPoint in dataPoints.DataPoints_)
                    {
                        await processor.Invoke(new TimeSeriesMetadata(dataPoint.TimeSeries.To<TimeSeriesId>()), dataPoint.ToDataPoint()).ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error processing datapoint");
                }
            }
        }

        void Discover()
        {
            _processors.ForEach(_ =>
            {
                var methods = _.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                var processorMethods = methods.Where(method => method.GetCustomAttributes().Any(attribute => attribute is DataPointProcessorAttribute));
                if (!processorMethods.Any())
                {
                    _logger.Warning($"DataPoint processor of type '{_.GetType().AssemblyQualifiedName}' does not seem to have any methods adorned with [DataPointProcessor] - this means it does not have any processors");
                }
                else
                {
                    processorMethods.ForEach(method =>
                    {
                        var processor = new DataPointProcessor(_, method);
                        _dataProcessors[processor.Id] = processor;
                    });
                }
            });
        }
    }
}