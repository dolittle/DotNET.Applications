// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Commands;
using Dolittle.FluentValidation.Concepts.given;

namespace Dolittle.FluentValidation.Commands
{
    public class MySimpleCommand : ICommand
    {
        public StringConcept StringConcept { get; set; }

        public LongConcept LongConcept { get; set; }
    }
}