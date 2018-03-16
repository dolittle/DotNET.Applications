using System;
using Dolittle.Commands;

namespace Dolittle.Commands.Diagnostics.for_CommandInheritanceRule
{
    public class CommandImplentingICommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
