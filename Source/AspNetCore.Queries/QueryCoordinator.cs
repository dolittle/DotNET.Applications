// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.Concepts;
using Dolittle.DependencyInversion;
using Dolittle.Dynamic;
using Dolittle.Logging;
using Dolittle.Queries;
using Dolittle.Queries.Coordination;
using Dolittle.Serialization.Json;
using Dolittle.Types;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

#pragma warning disable CA1308

namespace Dolittle.AspNetCore.Queries
{
    /// <summary>
    /// Represents an API endpoint for working with <see cref="IQuery">queries</see>.
    /// </summary>
    public class QueryCoordinator
    {
        readonly ITypeFinder _typeFinder;
        readonly IContainer _container;
        readonly IQueryCoordinator _queryCoordinator;
        readonly ISerializer _serializer;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCoordinator"/> class.
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> for finding types.</param>
        /// <param name="container"><see cref="IContainer"/> for getting instances of types.</param>
        /// <param name="queryCoordinator">The underlying <see cref="IQueryCoordinator"/>.</param>
        /// <param name="serializer">JSON <see cref="ISerializer"/>.</param>
        /// <param name="logger"><see cref="ILogger"/> for logging.</param>
        public QueryCoordinator(
            ITypeFinder typeFinder,
            IContainer container,
            IQueryCoordinator queryCoordinator,
            ISerializer serializer,
            ILogger logger)
        {
            _typeFinder = typeFinder;
            _container = container;
            _queryCoordinator = queryCoordinator;
            _serializer = serializer;
            _logger = logger;
        }

        /// <summary>
        /// Handles a <see cref="QueryRequest" /> from the <see cref="HttpRequest.Body" /> and writes the <see cref="QueryResult" /> to the <see cref="HttpResponse" />.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext" />.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(HttpContext context)
        {
            QueryRequest queryRequest = null;
            try
            {
                queryRequest = await context.RequestBodyFromJson<QueryRequest>().ConfigureAwait(false);
                _logger.Information("Executing query : {Query}", queryRequest.NameOfQuery);
                var queryType = _typeFinder.GetQueryTypeByName(queryRequest.GeneratedFrom);
                var query = _container.Get(queryType) as IQuery;
                var properties = queryType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty).ToDictionary(p => p.Name.ToLowerInvariant(), p => p);
                CopyPropertiesFromRequestToQuery(queryRequest, query, properties);

                var result = await _queryCoordinator.Execute(query, new PagingInfo()).ConfigureAwait(false);
                if (result.Success) AddClientTypeInformation(result);

                await context.RespondWithStatusCodeAndResult(StatusCodes.Status200OK, result, SerializationOptions.CamelCase).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null) ex = ex.InnerException;
                var queryName = queryRequest?.NameOfQuery ?? "Could not resolve query name";
                _logger.Error(ex, "Could not handle query request for the '{QueryName}' query", queryName);
                await context.RespondWithStatusCodeAndResult(
                    StatusCodes.Status200OK,
                    new QueryResult
                        {
                            Exception = ex,
                            QueryName = queryName
                        }).ConfigureAwait(false);
            }
        }

        void AddClientTypeInformation(QueryResult result)
        {
            var items = new List<object>();
            foreach (var item in result.Items)
            {
                var dynamicItem = item.AsExpandoObject();
                items.Add(dynamicItem);
            }

            result.Items = items;
        }

        void CopyPropertiesFromRequestToQuery(QueryRequest request, object instance, Dictionary<string, PropertyInfo> properties)
        {
            request.Parameters.Keys.ForEach(propertyName =>
            {
                var lowerCasedPropertyName = propertyName.ToLowerInvariant();
                if (properties.ContainsKey(lowerCasedPropertyName))
                {
                    var property = properties[lowerCasedPropertyName];
                    object value = request.Parameters[propertyName];

                    value = HandleValue(property.PropertyType, value);
                    property.SetValue(instance, value);
                }
            });
        }

        object HandleValue(Type targetType, object value)
        {
            if (value is JArray || value is JObject)
            {
                value = _serializer.FromJson(targetType, value.ToString());
            }
            else if (targetType.IsConcept())
            {
                value = ConceptFactory.CreateConceptInstance(targetType, value);
            }
            else if (targetType == typeof(DateTimeOffset))
            {
                if (value is DateTime time)
                    value = new DateTimeOffset(time);
            }
            else if (targetType.IsEnum)
            {
                value = Enum.Parse(targetType, value.ToString());
            }
            else if (targetType == typeof(Guid))
            {
                value = Guid.Parse(value.ToString());
            }
            else
            {
                if (!targetType.IsAssignableFrom(value.GetType()))
                    value = System.Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
            }

            return value;
        }
    }
}