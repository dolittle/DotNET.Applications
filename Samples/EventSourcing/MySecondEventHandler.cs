// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Events.Processing;
using Dolittle.Logging;

namespace EventSourcing
{
    [EventHandler("d87ed076-b18e-4d68-affd-73e56b9ba324")]
    public class MySecondEventHandler : ICanHandleEvents
    {
        readonly ILogger _logger;

        public MySecondEventHandler(ILogger logger)
        {
            _logger = logger;
        }

        public void Handle(MyEvent @event)
        {
            _logger.Information($"Processing event : '{@event}'");
        }
    }
}