// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Commands;

namespace Dolittle.FluentValidation.Commands
{
    public class SimpleCommand : ICommand
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string SomeString { get; set; }

        public int SomeInt { get; set; }
    }
}
