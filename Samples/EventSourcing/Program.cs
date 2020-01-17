// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Dolittle.Booting;
using Dolittle.Domain;
using Dolittle.Execution;
using Dolittle.Runtime.Commands;
using Dolittle.Runtime.Commands.Coordination;
using Dolittle.Tenancy;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventSourcing
{
    static class Program
    {
        static readonly CommandRequest NullCommandRequest = new CommandRequest(CorrelationId.New(), Guid.Parse("7f1d64af-2ec7-4b6e-bac2-fa0e0b18a661"), 1, new Dictionary<string, object>());

        static void Main()
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.ConfigureLogging(_ => _.AddConsole());
            hostBuilder.UseEnvironment("Development");
            var host = hostBuilder.Build();
            var loggerFactory = host.Services.GetService(typeof(ILoggerFactory)) as ILoggerFactory;

            var result = Bootloader.Configure(_ =>
            {
                _.UseLoggerFactory(loggerFactory);
                _.Development();
            }).Start();

            var commandContextManager = result.Container.Get<ICommandContextManager>();
            var executionContextManager = result.Container.Get<IExecutionContextManager>();

            executionContextManager.CurrentFor(TenantId.Development);

            using (commandContextManager.EstablishForCommand(NullCommandRequest))
            {
                var aggregateOf = result.Container.Get<IAggregateOf<MyAggregate>>();
                aggregateOf.Create().Perform(_ => _.DoStuff());
            }
        }
    }
}
