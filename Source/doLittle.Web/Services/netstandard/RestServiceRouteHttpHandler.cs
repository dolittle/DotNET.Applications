/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Threading.Tasks;
using doLittle.Configuration;
using doLittle.Exceptions;
using doLittle.Execution;
using doLittle.Logging;
using doLittle.Security;
using doLittle.Services;
using Microsoft.AspNetCore.Http;

namespace doLittle.Web.Services
{
    public class RestServiceRouteHttpHandler
    {
        Type _type;
        string _url;
        IRequestParamsFactory _factory;
        IRestServiceMethodInvoker _invoker;
        IContainer _container;
        ISecurityManager _securityManager;
        IExceptionPublisher _exceptionPublisher;
        ILogger _logger;

        public RestServiceRouteHttpHandler(Type type, string url) 
            : this(
                type,
                url,
                Configure.Instance.Container.Get<IRequestParamsFactory>(),
                Configure.Instance.Container.Get<IRestServiceMethodInvoker>(),
                Configure.Instance.Container,
                Configure.Instance.Container.Get<ISecurityManager>(),
                Configure.Instance.Container.Get<IExceptionPublisher>(),
                Configure.Instance.Container.Get<ILogger>())
        {}

        public RestServiceRouteHttpHandler(
            Type type,
            string url,
            IRequestParamsFactory factory,
            IRestServiceMethodInvoker invoker,
            IContainer container,
            ISecurityManager securityManager,
            IExceptionPublisher exceptionPublisher,
            ILogger logger)
        {
            _type = type;
            _url = url;
            _factory = factory;
            _invoker = invoker;
            _container = container;
            _securityManager = securityManager;
            _exceptionPublisher = exceptionPublisher;
            _logger = logger;
        }

        public Task ProcessRequest(HttpContext context)
        {
            try
            {
                var request = context.Request;
                _logger.Information($"Request : {request.Path}");
                
                var form = _factory.BuildParamsCollectionFrom(new HttpRequest(context.Request));
                var serviceInstance = _container.Get(_type);

                _logger.Trace("Authorize");
                var authorizationResult = _securityManager.Authorize<InvokeService>(serviceInstance);

                if (!authorizationResult.IsAuthorized)
                {
                    _logger.Trace("Not authorized");
                    throw new HttpStatus.HttpStatusException(404, "Forbidden");
                }
                _logger.Trace("Authorized");

                
                var url = $"{request.Scheme}://{request.Host}{request.Path}";

                _logger.Trace($"URL : {url}");
                var result = _invoker.Invoke(_url, serviceInstance, new Uri(url), form);

                _logger.Trace($"Result : {result}");
                return context.Response.WriteAsync(result);
            }
            catch (Exception e)
            {
                _exceptionPublisher.Publish(e);
                var message = string.Empty;
                if (e.InnerException is HttpStatus.HttpStatusException)
                {
                    var ex = e.InnerException as HttpStatus.HttpStatusException;
                    context.Response.StatusCode = ex.Code;
                    message = ex.Description;
                }
                else
                {
                    context.Response.StatusCode = 500;
                    message = e.Message.Substring(0,e.Message.Length >= 512 ? 512: e.Message.Length);
                }
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync($"{{'message':'{message}'}}");
            }
        }
    }
}
