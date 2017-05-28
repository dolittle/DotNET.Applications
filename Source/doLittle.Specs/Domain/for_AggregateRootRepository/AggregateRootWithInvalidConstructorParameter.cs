using System;
using doLittle.Domain;

namespace doLittle.Specs.Domain.for_AggregateRootRepository
{
    public class AggregateRootWithInvalidConstructorParameter : AggregateRoot
    {
        public AggregateRootWithInvalidConstructorParameter(int something) : base(Guid.NewGuid())
        {

        }
    }
}
