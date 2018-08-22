using System.Collections.Generic;
using System.Linq;
using Dolittle.Artifacts.Configuration;
using Dolittle.Concepts.Serialization.Json;
using Dolittle.Logging;
using Dolittle.Serialization.Json;
using Newtonsoft.Json;

namespace Dolittle.Build
{
    internal class ConverterProvider : ICanProvideConverters
    {
        readonly ILogger _logger;
        internal ConverterProvider(ILogger logger)
        {
            _logger = logger;
        }
        /// <inheritdoc/>
        public IEnumerable<JsonConverter> Provide()
        {
            var converters = new Dolittle.Concepts.Serialization.Json.ConverterProvider(_logger).Provide().ToList();
            
            converters.Add(new ClrTypeConverter());
            return converters;
        }
    }
}