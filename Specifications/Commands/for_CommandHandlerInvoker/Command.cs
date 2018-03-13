using System;
using Dolittle.Commands;

namespace Dolittle.Runtime.Commands.Specs.for_CommandHandlerInvoker
{
    public class Command : ICommand
    {
        public Guid Id { get; set; }
    }
}
