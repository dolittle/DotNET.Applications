// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if false
using System;
using Dolittle.TimeSeries.DataTypes.Runtime;

namespace Dolittle.TimeSeries.DataTypes
{
    /// <summary>
    /// The exception that gets thrown when a unsupported primitive type is used for <see cref="IValue"/>
    /// and the protobuf representation <see cref="Value"/>
    /// </summary>
    public class UnsupportedValueType : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnsupportedValueType"/>
        /// </summary>
        /// <param name="type">Unsupported <see cref="Type"/></param>
        public UnsupportedValueType(Type type) : base($"Type '{type.AssemblyQualifiedName}' is not a supported value type") {}
    }
}
#endif