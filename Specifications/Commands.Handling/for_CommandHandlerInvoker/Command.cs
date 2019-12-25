// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Commands.Handling.for_CommandHandlerInvoker
{
    public class Command : ICommand
    {
        public Guid Id { get; set; }
    }
}
