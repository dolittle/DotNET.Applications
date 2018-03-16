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
