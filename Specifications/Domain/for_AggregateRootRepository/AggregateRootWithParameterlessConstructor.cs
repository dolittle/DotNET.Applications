using System;
using doLittle.Domain;

namespace doLittle.Specs.Domain.for_AggregateRootRepository
{
    public class AggregateRootWithParameterlessConstructor : AggregateRoot
    {
        public AggregateRootWithParameterlessConstructor() : base(Guid.NewGuid())
        {

        }
    }
}
