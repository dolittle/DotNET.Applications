using Bifrost.Configuration;
using System;

namespace Bifrost.Specs.Configuration
{
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        public Type EntityContextType { get; set; }

        public Bifrost.Entities.IEntityContextConnection Connection { get; set; }
    }
}
