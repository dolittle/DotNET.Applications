using System;
using Dolittle.Domain;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor
{
    public class AggregateRootWithGuidConstructor : AggregateRoot
    {
        public AggregateRootWithGuidConstructor(Guid id) : base(id)
        {

        }
    }
}
