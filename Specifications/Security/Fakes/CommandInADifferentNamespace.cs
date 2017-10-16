using System;
using doLittle.Commands;

namespace doLittle.SomeRandomNamespace
{
    public class CommandInADifferentNamespace : ICommand
    {
        public Guid Id { get; set; }
    }
}