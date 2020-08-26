// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using System.Reflection;
using Dolittle.Concepts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dolittle.ReadModels.MongoDB
{
    /// <summary>
    /// Extension methods for easily working with <see cref="IReadModel">read models</see>.
    /// </summary>
    public static class ReadModelExtensions
    {
        /// <summary>
        /// Get the object Id from a <see cref="IReadModel">read model</see>.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IReadModel"/>.</typeparam>
        /// <param name="readModel"><see cref="IReadModel">Read model</see> to get from.</param>
        /// <returns>Object Id as a <see cref="BsonValue"/>.</returns>
        public static BsonValue GetObjectIdFrom<T>(this T readModel)
            where T : IReadModel
        {
            var propInfo = GetIdProperty(readModel);
            object id = propInfo.GetValue(readModel);

            return GetIdAsBsonValue(id);
        }

        /// <summary>
        /// Get Id as <see cref="BsonValue"/>.
        /// </summary>
        /// <param name="id">Id to convert.</param>
        /// <returns>The Id in the form of a <see cref="BsonValue"/>.</returns>
        public static BsonValue GetIdAsBsonValue(this object id)
        {
            var idVal = id;
            if (id.IsConcept())
                idVal = id.GetConceptValue();

            var idAsValue = BsonValue.Create(idVal);
            return idAsValue;
        }

        /// <summary>
        /// Get the property that represents the unique identifier for a <see cref="IReadModel">read model</see>.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="IReadModel"/>.</typeparam>
        /// <param name="readModel"><see cref="IReadModel">Read model</see> to get property from.</param>
        /// <returns><see cref="PropertyInfo"/> representing the property.</returns>
        public static PropertyInfo GetIdProperty<T>(this T readModel)
            where T : IReadModel
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).First(p => string.Equals(p.Name, "id", System.StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
