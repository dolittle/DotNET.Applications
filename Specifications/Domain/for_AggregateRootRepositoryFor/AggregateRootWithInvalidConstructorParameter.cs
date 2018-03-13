using System;
using Dolittle.Domain;

namespace Dolittle.Specs.Domain.for_AggregateRootRepositoryFor
{
    public class AggregateRootWithInvalidConstructorParameter : AggregateRoot
    {
        public AggregateRootWithInvalidConstructorParameter(int something) : base(Guid.NewGuid())
        {

        }
    }
}
