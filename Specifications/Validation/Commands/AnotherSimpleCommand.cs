using System;
using Dolittle.Commands;

namespace Dolittle.FluentValidation.Commands
{
    public class AnotherSimpleCommand : ICommand
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string SomeString { get; set; }

        public int SomeInt { get; set; }
    }
}
