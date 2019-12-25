// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Commands;

namespace Dolittle.FluentValidation.MetaData.for_ValidationMetaDataGenerator
{
    public class CommandForValidation : ICommand
    {
        public const string SomeStringName = "someString";
        public const string SomeIntName = "someInt";

        public string SomeString { get; set; }

        public int SomeInt { get; set; }
    }
}
