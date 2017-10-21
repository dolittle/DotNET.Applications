using System;
using doLittle.Commands;

namespace doLittle.Specs.Commands.Diagnostics.for_CommandInheritanceRule
{
    public class CommandImplentingICommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
