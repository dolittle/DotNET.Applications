using doLittle.Configuration;
using System;

namespace doLittle.Specs.Configuration
{
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        public Type EntityContextType { get; set; }

        public doLittle.Entities.IEntityContextConnection Connection { get; set; }
    }
}
