// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Machine.Specifications.MongoDB.given;
using Dolittle.ReadModels;
using Machine.Specifications;

namespace Dolittle.Machine.Specifications.MongoDB
{
    [Subject(typeof(a_mongo_db_instance))]
    public class when_fetching_a_document_by_id : a_mongo_db_instance
    {
        static readonly string collection_name = typeof(AReadModel).Name;
        static Guid _id;
        static AReadModel _result;
        static IReadModelRepositoryFor<AReadModel> _repo;

        Establish context = () =>
        {
            _id = Guid.NewGuid();
            var doc = new AReadModel
            {
                Id = _id,
            };
            _repo = GetReadModelRepositoryFor<AReadModel>();
            _repo.Insert(doc);
        };

        Because of = () =>
        {
            _result = _repo.GetById(_id);
        };

        It should_returns_a_repository_that_can_insert_and_query_collections = () => _result.ShouldNotBeNull();
    }
}