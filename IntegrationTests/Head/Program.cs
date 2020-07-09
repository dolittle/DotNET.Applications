// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;
using Dolittle.Hosting.Microsoft;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Head
{
    static class Program
    {
        public static async Task Main(string[] args) =>
            await CreateHostBuilder(args)
                .Build()
                .RunAsync().ConfigureAwait(false);

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseEnvironment("Development")
                .UseDolittle()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls("http://*:5000")
                        .UseKestrel()
                        .UseStartup<Startup>();
                });
        }
    }
}
