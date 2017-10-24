using System;
using doLittle.Domain;

namespace doLittle.Specs.Domain.for_AggregateRootRepositoryFor
{
    public class AggregateRootWithGuidConstructor : AggregateRoot
    {
        public AggregateRootWithGuidConstructor(Guid id) : base(id)
        {

        }
    }
}
