/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using doLittle.Concepts;
using doLittle.Extensions;
using doLittle.Logging;
using doLittle.Serialization;

namespace doLittle.Web.Services
{
    public class RestServiceMethodInvoker : IRestServiceMethodInvoker
    {
        private readonly ISerializer _serializer;
        private readonly IJsonInterceptor _jsonInterceptor;
        private readonly ILogger _logger;

        public RestServiceMethodInvoker(
            ISerializer serializer,
            IJsonInterceptor jsonInterceptor,
            ILogger logger)
        {
            _serializer = serializer;
            _jsonInterceptor = jsonInterceptor;
            _logger = logger;
        }

        public string Invoke(string baseUrl, object instance, Uri uri, NameValueCollection inputParameters)
        {
            _logger.Trace($"BaseUrl : '{baseUrl}'");
            _logger.Trace($"Uri : '{uri}'");

            FilterInputParameters(inputParameters);

            var type = instance.GetType();
            var methodName = GetMethodNameFromUri(baseUrl, uri);
            _logger.Trace($"Method : {methodName}");

            ThrowIfMethodNameNotSpecified(methodName, instance, uri);
            ThrowIfMethodMissing(methodName, instance, uri);

            var method = type.GetMethod(methodName);
            ThrowIfParameterCountMismatches(method, type, uri, inputParameters);
            ThrowIfParameterMissing(method, type, uri, inputParameters);

            var values = GetParameterValues(inputParameters, method);

            _logger.Trace($"Invoke with {values.Length} parameters");
            var result = method.Invoke(instance, values);

            var serializedResult = _serializer.ToJson(result, SerializationOptions.CamelCase);

            serializedResult = _jsonInterceptor.Intercept(serializedResult);

            return serializedResult;
        }

        void FilterInputParameters(NameValueCollection inputParameters)
        {
            inputParameters.Remove("_");
            inputParameters.Remove("_q");
            inputParameters.Remove("_rm");
            inputParameters.Remove("_cmd");
        }

        object[] GetParameterValues(NameValueCollection inputParameters, MethodInfo method)
        {
            _logger.Trace($"GetParameterValues for {method.Name} on {method.DeclaringType.FullName}");
            var values = new List<object>();
            var parameters = method.GetParameters();
            foreach (var parameter in parameters)
            {
                var parameterAsString = inputParameters[parameter.Name];
                _logger.Trace($"Parameter {parameter.Name} - {parameterAsString}");
                values.Add(HandleValue(parameter, parameterAsString));
            }
            return values.ToArray();
        }

        string Unescape(string value)
        {
            if (value.StartsWith("\"")) value = value.Substring(1);
            if (value.EndsWith("\"")) value = value.Substring(0, value.Length - 1);

            return value;
        }

        object HandleValue(ParameterInfo parameter, string input)
        {            
            _logger.Trace($"Parameter : {parameter.Name} - {input}");
            if (parameter.ParameterType == typeof(string))
                return input;

            input = Unescape(input);

            if (parameter.ParameterType.GetTypeInfo().IsValueType)
                return TypeDescriptor.GetConverter(parameter.ParameterType).ConvertFromInvariantString(input);

            if(parameter.ParameterType.IsConcept())
            {
                var genericArgumentType = parameter.ParameterType.GetTypeInfo().BaseType.GetTypeInfo().GetGenericArguments()[0];
                var value = input.ParseTo(genericArgumentType);
                return ConceptFactory.CreateConceptInstance(parameter.ParameterType, value);
            }

            input = _jsonInterceptor.Intercept(input);

            _logger.Trace($"Deserialize '{input}' into {parameter.ParameterType}");
            var deserialized = _serializer.FromJson(parameter.ParameterType, input, SerializationOptions.CamelCase);
            return deserialized;
        }

        string GetMethodNameFromUri(string baseUrl, Uri uri)
        {
            var path = uri.AbsolutePath;
            if (path.StartsWith("/"))
                path = path.Substring(1);

            var segments = path.Split('/');
            if (segments.Length > 1)
                return segments[segments.Length-1];

            return string.Empty;
        }

        void ThrowIfParameterMissing(MethodInfo methodInfo, Type type, Uri uri, NameValueCollection inputParameters)
        {
            var parameters = methodInfo.GetParameters();
            foreach (var parameter in parameters)
                if (!inputParameters.AllKeys.Contains(parameter.Name))
                    throw new MissingParameterException(parameter.Name, type.Name, uri);
        }

        void ThrowIfParameterCountMismatches(MethodInfo methodInfo, Type type, Uri uri, NameValueCollection inputParameters)
        {
            var parameters = methodInfo.GetParameters();
            if( inputParameters.Count != parameters.Length )
                throw new ParameterCountMismatchException(uri, type.Name, inputParameters.Count, parameters.Length);
        }

        void ThrowIfMethodNameNotSpecified(string methodName, object instance, Uri uri)
        {
            if (string.IsNullOrEmpty(methodName))
                throw new MethodNotSpecifiedException(instance.GetType(), uri);
        }

        void ThrowIfMethodMissing(string methodName, object instance, Uri uri)
        {
            var method = instance.GetType().GetMethod(methodName);
            if (method == null)
                throw new MissingMethodException(string.Format("Missing method '{0}' for Uri '{1}'", methodName, uri));
        }
    }
}
