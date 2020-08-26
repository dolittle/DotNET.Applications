// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.AspNetCore.Debugging.Handlers;
using Dolittle.Reflection;
using Microsoft.AspNetCore.Http;

namespace Dolittle.AspNetCore.Debugging.Middleware
{
    /// <summary>
    /// An implementation of <see cref="IInvokeDebuggingHandlerMethods"/>.
    /// </summary>
    public class InvokeDebuggingHandlerMethods : IInvokeDebuggingHandlerMethods
    {
        readonly IFindDebuggingHandleMethod _methodFinder;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvokeDebuggingHandlerMethods"/> class.
        /// </summary>
        /// <param name="methodFinder">The <see cref="IFindDebuggingHandleMethod"/> used to find the appropriate Handle... method to invoke.</param>
        public InvokeDebuggingHandlerMethods(IFindDebuggingHandleMethod methodFinder)
        {
            _methodFinder = methodFinder;
        }

        /// <inheritdoc/>
        public async Task InvokeDebugginHandlerMethod(HttpContext context, IDebuggingHandler handler, Type artifactType, object artifact)
        {
            if (HttpMethods.IsGet(context.Request.Method) && handler.GetType().ImplementsOpenGeneric(typeof(ICanHandleGetRequests<>)))
            {
                var method = _methodFinder.FindMethod(handler, typeof(ICanHandleGetRequests<>), artifactType);
                await InvokeHandlerMethod(context, method, handler, artifact).ConfigureAwait(false);
                return;
            }

            if (HttpMethods.IsPost(context.Request.Method) && handler.GetType().ImplementsOpenGeneric(typeof(ICanHandlePostRequests<>)))
            {
                var method = _methodFinder.FindMethod(handler, typeof(ICanHandlePostRequests<>), artifactType);
                await InvokeHandlerMethod(context, method, handler, artifact).ConfigureAwait(false);
                return;
            }
        }

        async Task InvokeHandlerMethod(HttpContext context, MethodInfo method, IDebuggingHandler handler, object artifact)
        {
            try
            {
                var task = method.Invoke(handler, new[] { context, artifact }) as Task;
                await task.ConfigureAwait(false);
            }
            catch (TargetException exception)
            {
                throw new CouldNotInvokeHandleMethod(method, handler, artifact.GetType(), exception);
            }
            catch (TargetParameterCountException exception)
            {
                throw new CouldNotInvokeHandleMethod(method, handler, artifact.GetType(), exception);
            }
            catch (ArgumentException exception)
            {
                throw new CouldNotInvokeHandleMethod(method, handler, artifact.GetType(), exception);
            }
        }
    }
}