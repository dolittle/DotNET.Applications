using System;
using doLittle.Domain;

namespace doLittle.Specs.Domain.for_AggregateRootRepository
{
    public class AggregateRootWithMultipleConstructorParameters : AggregateRoot
    {
        public AggregateRootWithMultipleConstructorParameters(Guid id, int something) : base(id)
        {

        }
    }
}
