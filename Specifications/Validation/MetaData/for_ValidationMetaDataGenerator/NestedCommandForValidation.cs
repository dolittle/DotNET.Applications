/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Commands;

namespace Dolittle.FluentValidation.MetaData.for_ValidationMetaDataGenerator
{
    public class NestedCommandForValidation : ICommand
    {
        public const string SomeObjectName = "someCommand";
        public const string FirstLevelStringName = "firstLevelString";
        public CommandForValidation SomeCommand { get; set; }
        public string FirstLevelString { get; set; }
    }
}