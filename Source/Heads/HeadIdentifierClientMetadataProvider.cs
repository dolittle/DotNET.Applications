// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Services.Clients;
using Grpc.Core;

namespace Dolittle.Heads
{
    /// <summary>
    /// Represents an implementation of <see cref="ICanProvideClientMetadata"/>.
    /// </summary>
    public class HeadIdentifierClientMetadataProvider : ICanProvideClientMetadata
    {
        readonly Head _head;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeadIdentifierClientMetadataProvider"/> class.
        /// </summary>
        /// <param name="head">The current <see cref="Head"/>.</param>
        public HeadIdentifierClientMetadataProvider(Head head)
        {
            _head = head;
        }

        /// <inheritdoc/>
        public IEnumerable<Metadata.Entry> Provide()
        {
            return new[]
            {
                new Metadata.Entry($"headid{Metadata.BinaryHeaderSuffix}", _head.Id.Value.ToByteArray())
            };
        }
    }
}