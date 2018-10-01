/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Domain;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor
{
    public class AggregateRootWithMultipleConstructorParameters : AggregateRoot
    {
        public AggregateRootWithMultipleConstructorParameters(Guid id, int something) : base(id)
        {

        }
    }
}
