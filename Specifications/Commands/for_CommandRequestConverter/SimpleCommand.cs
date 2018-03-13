using System;
using Dolittle.Commands;

namespace Dolittle.Runtime.Commands.Coordination.Specs
{
    public class SimpleCommand : ICommand
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string SomeString { get; set; }

        public int SomeInt { get; set; }
    }
}
