// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Concepts;

namespace Dolittle.Commands.for_CommandRequestToCommandConverter
{
    public class concept_as_guid : ConceptAs<Guid>
    {
        public static implicit operator concept_as_guid(Guid guid)
        {
            return new concept_as_guid { Value = guid };
        }
    }
}