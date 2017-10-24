using System;
using doLittle.Domain;

namespace doLittle.Specs.Domain.for_AggregateRootRepositoryFor
{
    public class AggregateRootWithParameterlessConstructor : AggregateRoot
    {
        public AggregateRootWithParameterlessConstructor() : base(Guid.NewGuid())
        {

        }
    }
}
