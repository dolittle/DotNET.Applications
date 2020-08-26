// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using MongoDB.Driver;

namespace Dolittle.ReadModels.MongoDB
{
    /// <summary>
    /// Represents an implementation of <see cref="IReadModelRepositoryFor{T}"/> for MongoDB.
    /// </summary>
    /// <typeparam name="T">Type of <see cref="IReadModel"/>.</typeparam>
    public class ReadModelRepositoryFor<T> : IReadModelRepositoryFor<T>
        where T : IReadModel
    {
        readonly string _collectionName = RemoveReadNamespace(typeof(T).FullName);
        readonly Configuration _configuration;
        readonly IMongoCollection<T> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadModelRepositoryFor{T}"/> class.
        /// </summary>
        /// <param name="configuration"><see cref="Configuration"/> to use.</param>
        public ReadModelRepositoryFor(Configuration configuration)
        {
            _configuration = configuration;
            _collection = configuration.Database.GetCollection<T>(_collectionName);
        }

        /// <inheritdoc/>
        public IQueryable<T> Query => _collection.AsQueryable<T>();

        /// <inheritdoc/>
        public void Delete(T readModel)
        {
            var objectId = readModel.GetObjectIdFrom();
            _collection.DeleteOne(Builders<T>.Filter.Eq("_id", objectId));
        }

        /// <inheritdoc/>
        public T GetById(object id)
        {
            var objectId = id.GetIdAsBsonValue();
            return _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefault();
        }

        /// <inheritdoc/>
        public void Insert(T readModel)
        {
            _collection.InsertOne(readModel);
        }

        /// <inheritdoc/>
        public void Update(T readModel)
        {
            var id = readModel.GetObjectIdFrom();

            var filter = Builders<T>.Filter.Eq("_id", id);
            _collection.ReplaceOne(filter, readModel, new ReplaceOptions() { IsUpsert = true });
        }

        static string RemoveReadNamespace(string source)
        {
            return source.StartsWith("Read.", StringComparison.InvariantCulture)
                ? source.Substring(5)
                : source;
        }
    }
}
