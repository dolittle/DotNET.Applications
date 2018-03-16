using System;
using Dolittle.Domain;

namespace Dolittle.Domain.for_AggregateRootRepositoryFor
{
    public class AggregateRootWithInvalidConstructorParameter : AggregateRoot
    {
        public AggregateRootWithInvalidConstructorParameter(int something) : base(Guid.NewGuid())
        {

        }
    }
}
