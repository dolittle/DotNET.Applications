/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using doLittle.Logging;
using doLittle.Serialization;

namespace doLittle.Web.Services
{
    /// <summary>
    /// Represents an implementation of <see cref="IRequestParamsFactory"/>
    /// </summary>
    public class RequestParamsFactory : IRequestParamsFactory
    {
        readonly ISerializer _serializer;
        readonly ILogger _logger;

        public RequestParamsFactory(ISerializer serializer, ILogger logger)
        {
            _serializer = serializer;
            _logger = logger;
        }

        /// <inheritdoc/>
        public RequestParams BuildParamsCollectionFrom(IHttpRequest request)
        {
            var queystring = request.QueryString;
            var form = request.Form;
            var requestBody = BuildFormFromInputStream(request.InputStream);

            var requestParams = new RequestParams {queystring, form, requestBody }; 

            return requestParams;
        }

        NameValueCollection BuildFormFromInputStream(Stream stream)
        {
            var input = new byte[stream.Length];
            stream.Read(input, 0, input.Length);
            var inputAsString = System.Text.Encoding.UTF8.GetString(input);
            _logger.Trace($"RAW JSON: {inputAsString}");

            var inputDictionary = _serializer.FromJson(typeof(Dictionary<string, string>), inputAsString, null) as Dictionary<string,string>;

            var inputStreamParams = new NameValueCollection();

            if (inputDictionary != null)
            {
                foreach (var key in inputDictionary.Keys)
                    inputStreamParams[key] = inputDictionary[key];
            }
            
            return inputStreamParams;
        }
    }
}