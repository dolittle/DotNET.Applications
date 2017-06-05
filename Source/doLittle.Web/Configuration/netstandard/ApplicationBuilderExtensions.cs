/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using doLittle.Configuration;
using doLittle.Web;
using doLittle.Web.Assets;
using doLittle.Web.Commands;
using doLittle.Web.Configuration;
using doLittle.Web.Proxies;
using doLittle.Web.Read;
using doLittle.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        static AsyncLocal<ClaimsPrincipal> _currentPrincipal = new AsyncLocal<ClaimsPrincipal>();

        public static IApplicationBuilder UsedoLittle(this IApplicationBuilder builder, IHostingEnvironment hostingEnvironment)
        {            
            Configure.DiscoverAndConfigure(builder.ApplicationServices.GetService<ILoggerFactory>());

            builder.Use(WebCallContext.Middleware);

            builder.Map("/doLittle/Application", a => a.Run(Application));
            builder.Map("/doLittle/Proxies", a => a.Run(Proxies));
            builder.Map("/doLittle/Security", a => a.Run(SecurityProxies));
            builder.Map("/doLittle/AssetsManager", a => a.Run(AssetsManager));

            var routeBuilder = new RouteBuilder(builder);
            routeBuilder.MapService<CommandCoordinatorService>("doLittle/CommandCoordinator");
            routeBuilder.MapService<CommandSecurityService>("doLittle/CommandSecurity");
            routeBuilder.MapService<QueryService>("doLittle/Query");
            routeBuilder.MapService<ReadModelService>("doLittle/ReadModel");

            var routes = routeBuilder.Build();
            builder.UseRouter(routes);

            var webConfiguration = Configure.Instance.Container.Get<WebConfiguration>();
            webConfiguration.ApplicationPhysicalPath = hostingEnvironment.WebRootPath;

            if( ClaimsPrincipal.ClaimsPrincipalSelector == null )
            {
                builder.Use(async (context, next) =>
                {
                    _currentPrincipal.Value = context.User;
                    await next();
                });
                ClaimsPrincipal.ClaimsPrincipalSelector = () => _currentPrincipal.Value;
            }

            return builder;
        }

        static async Task Application(HttpContext context)
        {
            var configuration = Configure.Instance.Container.Get<ConfigurationAsJavaScript>();
            context.Response.ContentType = "text/javascript";
            if (context.Request.Query.ContainsKey("nocache")) configuration.Initialize();
            await context.Response.WriteAsync(configuration.AsString);
        }


        static async Task Proxies(HttpContext context)
        {
            var proxies = Configure.Instance.Container.Get<GeneratedProxies>();
            context.Response.ContentType = "text/javascript";
            await context.Response.WriteAsync(proxies.All);
        }

        static async Task SecurityProxies(HttpContext context)
        {
            var proxies = Configure.Instance.Container.Get<CommandSecurityProxies>();
            context.Response.ContentType = "text/javascript";
            await context.Response.WriteAsync(proxies.Generate());
        }

        static async Task AssetsManager(HttpContext context)
        {
            var assetsManager = Configure.Instance.Container.Get<IAssetsManager>();
            context.Response.ContentType = "text/javascript";

            IEnumerable<string> assets = new string[0];
            if (context.Request.Query.ContainsKey("extension"))
            {
                var extension = context.Request.Query["extension"];
                assets = assetsManager.GetFilesForExtension(extension);
                if (context.Request.Query.ContainsKey("structure"))
                    assets = assetsManager.GetStructureForExtension(extension);
            }
            var serialized = JsonConvert.SerializeObject(assets);
            await context.Response.WriteAsync(serialized);
        }
    }
}