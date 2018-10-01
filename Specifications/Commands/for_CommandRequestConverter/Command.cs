/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Commands;

namespace Dolittle.Runtime.Commands.for_CommandRequestConverter
{
    public class Command : ICommand
    {
        public string   StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
}
