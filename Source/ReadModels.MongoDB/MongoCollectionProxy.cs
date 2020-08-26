// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

#pragma warning disable CS0618

namespace Dolittle.ReadModels.MongoDB
{
    /// <summary>
    /// Represents a proxy for <see cref="IMongoDatabase"/>.
    /// </summary>
    /// <remarks>
    /// The proxy allows us to be living in the right scope and still be able to take dependencies to
    /// the IMongoDatabase directly. This means it can be safe to assume that you're getting the
    /// database for the correct tenant, unless being held onto in something that has a longer lifecycle
    /// than per tenant.
    /// </remarks>
    /// <typeparam name="T">Type of document.</typeparam>
    public class MongoCollectionProxy<T> : IMongoCollection<T>
    {
        readonly Configuration _configuration;
        readonly IMongoCollection<T> _actualCollection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoCollectionProxy{T}"/> class.
        /// </summary>
        /// <param name="configuration"><see cref="Configuration"/> to use.</param>
        public MongoCollectionProxy(Configuration configuration)
        {
            _configuration = configuration;
            _actualCollection = _configuration.Database.GetCollection<T>(typeof(T).FullName);
        }

        /// <inheritdoc/>
        public CollectionNamespace CollectionNamespace => _actualCollection.CollectionNamespace;

        /// <inheritdoc/>
        public IMongoDatabase Database => _configuration.Database;

        /// <inheritdoc/>
        public IBsonSerializer<T> DocumentSerializer => _actualCollection.DocumentSerializer;

        /// <inheritdoc/>
        public IMongoIndexManager<T> Indexes => _actualCollection.Indexes;

        /// <inheritdoc/>
        public MongoCollectionSettings Settings => _actualCollection.Settings;

        /// <inheritdoc/>
        public IAsyncCursor<TResult> Aggregate<TResult>(PipelineDefinition<T, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.Aggregate(pipeline, options, cancellationToken);
        }

        /// <inheritdoc/>
        public IAsyncCursor<TResult> Aggregate<TResult>(IClientSessionHandle session, PipelineDefinition<T, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.Aggregate(session, pipeline, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<T, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.AggregateAsync(pipeline, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(IClientSessionHandle session, PipelineDefinition<T, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.AggregateAsync(session, pipeline, options, cancellationToken);
        }

        /// <inheritdoc/>
        public BulkWriteResult<T> BulkWrite(IEnumerable<WriteModel<T>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.BulkWrite(requests, options, cancellationToken);
        }

        /// <inheritdoc/>
        public BulkWriteResult<T> BulkWrite(IClientSessionHandle session, IEnumerable<WriteModel<T>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.BulkWrite(session, requests, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<BulkWriteResult<T>> BulkWriteAsync(IEnumerable<WriteModel<T>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.BulkWriteAsync(requests, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<BulkWriteResult<T>> BulkWriteAsync(IClientSessionHandle session, IEnumerable<WriteModel<T>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.BulkWriteAsync(session, requests, options, cancellationToken);
        }

        /// <inheritdoc/>
        public long Count(FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.CountDocuments(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public long Count(IClientSessionHandle session, FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.CountDocuments(session, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<long> CountAsync(FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.CountDocumentsAsync(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<long> CountAsync(IClientSessionHandle session, FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.CountDocumentsAsync(session, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public long CountDocuments(FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.CountDocuments(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public long CountDocuments(IClientSessionHandle session, FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.CountDocuments(session, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<long> CountDocumentsAsync(FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.CountDocumentsAsync(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<long> CountDocumentsAsync(IClientSessionHandle session, FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.CountDocumentsAsync(session, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public DeleteResult DeleteMany(FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteMany(filter, cancellationToken);
        }

        /// <inheritdoc/>
        public DeleteResult DeleteMany(FilterDefinition<T> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteMany(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public DeleteResult DeleteMany(IClientSessionHandle session, FilterDefinition<T> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteMany(session, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteManyAsync(filter, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteManyAsync(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DeleteResult> DeleteManyAsync(IClientSessionHandle session, FilterDefinition<T> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteManyAsync(session, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public DeleteResult DeleteOne(FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteOne(filter, cancellationToken);
        }

        /// <inheritdoc/>
        public DeleteResult DeleteOne(FilterDefinition<T> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteOne(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public DeleteResult DeleteOne(IClientSessionHandle session, FilterDefinition<T> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteOne(session, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteOneAsync(filter, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteOneAsync(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<DeleteResult> DeleteOneAsync(IClientSessionHandle session, FilterDefinition<T> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DeleteOneAsync(session, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public IAsyncCursor<TField> Distinct<TField>(FieldDefinition<T, TField> field, FilterDefinition<T> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.Distinct(field, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public IAsyncCursor<TField> Distinct<TField>(IClientSessionHandle session, FieldDefinition<T, TField> field, FilterDefinition<T> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.Distinct(session, field, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IAsyncCursor<TField>> DistinctAsync<TField>(FieldDefinition<T, TField> field, FilterDefinition<T> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DistinctAsync(field, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IAsyncCursor<TField>> DistinctAsync<TField>(IClientSessionHandle session, FieldDefinition<T, TField> field, FilterDefinition<T> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.DistinctAsync(session, field, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public long EstimatedDocumentCount(EstimatedDocumentCountOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.EstimatedDocumentCount(options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<long> EstimatedDocumentCountAsync(EstimatedDocumentCountOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.EstimatedDocumentCountAsync(options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(FilterDefinition<T> filter, FindOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindAsync(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(IClientSessionHandle session, FilterDefinition<T> filter, FindOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindAsync(session, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public TProjection FindOneAndDelete<TProjection>(FilterDefinition<T> filter, FindOneAndDeleteOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndDelete(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public TProjection FindOneAndDelete<TProjection>(IClientSessionHandle session, FilterDefinition<T> filter, FindOneAndDeleteOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndDelete(session, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TProjection> FindOneAndDeleteAsync<TProjection>(FilterDefinition<T> filter, FindOneAndDeleteOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndDeleteAsync(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TProjection> FindOneAndDeleteAsync<TProjection>(IClientSessionHandle session, FilterDefinition<T> filter, FindOneAndDeleteOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndDeleteAsync(session, filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public TProjection FindOneAndReplace<TProjection>(FilterDefinition<T> filter, T replacement, FindOneAndReplaceOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndReplace(filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public TProjection FindOneAndReplace<TProjection>(IClientSessionHandle session, FilterDefinition<T> filter, T replacement, FindOneAndReplaceOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndReplace(session, filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TProjection> FindOneAndReplaceAsync<TProjection>(FilterDefinition<T> filter, T replacement, FindOneAndReplaceOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndReplaceAsync(filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TProjection> FindOneAndReplaceAsync<TProjection>(IClientSessionHandle session, FilterDefinition<T> filter, T replacement, FindOneAndReplaceOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndReplaceAsync(session, filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public TProjection FindOneAndUpdate<TProjection>(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndUpdate(filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public TProjection FindOneAndUpdate<TProjection>(IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndUpdate(session, filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndUpdateAsync(filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TProjection> FindOneAndUpdateAsync<TProjection>(IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindOneAndUpdateAsync(session, filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public IAsyncCursor<TProjection> FindSync<TProjection>(FilterDefinition<T> filter, FindOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindSync(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public IAsyncCursor<TProjection> FindSync<TProjection>(IClientSessionHandle session, FilterDefinition<T> filter, FindOptions<T, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.FindSync(filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public void InsertMany(IEnumerable<T> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
            _actualCollection.InsertMany(documents, options, cancellationToken);
        }

        /// <inheritdoc/>
        public void InsertMany(IClientSessionHandle session, IEnumerable<T> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
            _actualCollection.InsertMany(session, documents, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task InsertManyAsync(IEnumerable<T> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.InsertManyAsync(documents, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task InsertManyAsync(IClientSessionHandle session, IEnumerable<T> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.InsertManyAsync(session, documents, options, cancellationToken);
        }

        /// <inheritdoc/>
        public void InsertOne(T document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            _actualCollection.InsertOne(document, options, cancellationToken);
        }

        /// <inheritdoc/>
        public void InsertOne(IClientSessionHandle session, T document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            _actualCollection.InsertOne(session, document, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task InsertOneAsync(T document, CancellationToken cancellationToken)
        {
            return _actualCollection.InsertOneAsync(document, null, cancellationToken);
        }

        /// <inheritdoc/>
        public Task InsertOneAsync(T document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.InsertOneAsync(document, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task InsertOneAsync(IClientSessionHandle session, T document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.InsertOneAsync(session, document, options, cancellationToken);
        }

        /// <inheritdoc/>
        public IAsyncCursor<TResult> MapReduce<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<T, TResult> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.MapReduce(map, reduce, options, cancellationToken);
        }

        /// <inheritdoc/>
        public IAsyncCursor<TResult> MapReduce<TResult>(IClientSessionHandle session, BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<T, TResult> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.MapReduce(session, map, reduce, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<T, TResult> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.MapReduceAsync(map, reduce, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(IClientSessionHandle session, BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<T, TResult> options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.MapReduceAsync(session, map, reduce, options, cancellationToken);
        }

        /// <inheritdoc/>
        public IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>()
            where TDerivedDocument : T
        {
            return _actualCollection.OfType<TDerivedDocument>();
        }

        /// <inheritdoc/>
        public ReplaceOneResult ReplaceOne(FilterDefinition<T> filter, T replacement, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.ReplaceOne(filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public ReplaceOneResult ReplaceOne(IClientSessionHandle session, FilterDefinition<T> filter, T replacement, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.ReplaceOne(session, filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public ReplaceOneResult ReplaceOne(FilterDefinition<T> filter, T replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.ReplaceOne(filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public ReplaceOneResult ReplaceOne(IClientSessionHandle session, FilterDefinition<T> filter, T replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.ReplaceOne(session, filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<T> filter, T replacement, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.ReplaceOneAsync(filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ReplaceOneResult> ReplaceOneAsync(IClientSessionHandle session, FilterDefinition<T> filter, T replacement, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.ReplaceOneAsync(session, filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<T> filter, T replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.ReplaceOneAsync(filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<ReplaceOneResult> ReplaceOneAsync(IClientSessionHandle session, FilterDefinition<T> filter, T replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.ReplaceOneAsync(session, filter, replacement, options, cancellationToken);
        }

        /// <inheritdoc/>
        public UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.UpdateMany(filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public UpdateResult UpdateMany(IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.UpdateMany(session, filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.UpdateManyAsync(filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<UpdateResult> UpdateManyAsync(IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.UpdateManyAsync(session, filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.UpdateOne(filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public UpdateResult UpdateOne(IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.UpdateOne(session, filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.UpdateOneAsync(filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<UpdateResult> UpdateOneAsync(IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return _actualCollection.UpdateOneAsync(session, filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public IMongoCollection<T> WithReadConcern(ReadConcern readConcern)
        {
            return _actualCollection.WithReadConcern(readConcern);
        }

        /// <inheritdoc/>
        public IMongoCollection<T> WithReadPreference(ReadPreference readPreference)
        {
            return _actualCollection.WithReadPreference(readPreference);
        }

        /// <inheritdoc/>
        public IMongoCollection<T> WithWriteConcern(WriteConcern writeConcern)
        {
            return _actualCollection.WithWriteConcern(writeConcern);
        }

        /// <inheritdoc/>
        IChangeStreamCursor<TResult> IMongoCollection<T>.Watch<TResult>(PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline, ChangeStreamOptions options, CancellationToken cancellationToken)
        {
            return _actualCollection.Watch<TResult>(pipeline, options, cancellationToken);
        }

        /// <inheritdoc/>
        IChangeStreamCursor<TResult> IMongoCollection<T>.Watch<TResult>(IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline, ChangeStreamOptions options, CancellationToken cancellationToken)
        {
            return _actualCollection.Watch<TResult>(session, pipeline, options, cancellationToken);
        }

        /// <inheritdoc/>
        Task<IChangeStreamCursor<TResult>> IMongoCollection<T>.WatchAsync<TResult>(PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline, ChangeStreamOptions options, CancellationToken cancellationToken)
        {
            return _actualCollection.WatchAsync<TResult>(pipeline, options, cancellationToken);
        }

        /// <inheritdoc/>
        Task<IChangeStreamCursor<TResult>> IMongoCollection<T>.WatchAsync<TResult>(IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<T>, TResult> pipeline, ChangeStreamOptions options, CancellationToken cancellationToken)
        {
            return _actualCollection.WatchAsync<TResult>(session, pipeline, options, cancellationToken);
        }
    }
}
