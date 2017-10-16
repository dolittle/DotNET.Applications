using System;
using doLittle.Domain;

namespace doLittle.Specs.Domain.for_AggregateRootRepository
{
    public class AggregateRootWithGuidConstructor : AggregateRoot
    {
        public AggregateRootWithGuidConstructor(Guid id) : base(id)
        {

        }
    }
}
