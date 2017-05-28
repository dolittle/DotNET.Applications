/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Web;
using doLittle.Configuration;
using doLittle.Web.Commands;

namespace doLittle.Web.Security
{
    public class SecurityRouteHttpHandler : IHttpHandler
    {
        public SecurityRouteHttpHandler()
        {
        }

        public bool IsReusable { get { return true; } }

        public void ProcessRequest(HttpContext context)
        {
            var proxies = Configure.Instance.Container.Get<CommandSecurityProxies>();
            context.Response.ContentType = "text/javascript";
            context.Response.Write(proxies.Generate());
        }
    }
}
