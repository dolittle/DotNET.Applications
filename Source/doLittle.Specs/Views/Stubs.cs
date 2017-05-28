using System;
using doLittle.Views;

namespace doLittle.Specs.Views
{
    public class SimpleObjectWithId : IHaveId
    {
        public SimpleObjectWithId()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }

    public class SimpleObjectWithoutId
    {
        
    }
}
