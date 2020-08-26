// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.ReadModels;
using MongoDB.Bson.Serialization.Attributes;

namespace Dolittle.Machine.Specifications.MongoDB.given
{
    public class AReadModel : IReadModel
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}