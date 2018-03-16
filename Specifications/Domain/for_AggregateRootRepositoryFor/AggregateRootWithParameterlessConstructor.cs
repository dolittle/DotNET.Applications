using System;
using Dolittle.Domain;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor
{
    public class AggregateRootWithParameterlessConstructor : AggregateRoot
    {
        public AggregateRootWithParameterlessConstructor() : base(Guid.NewGuid())
        {

        }
    }
}
