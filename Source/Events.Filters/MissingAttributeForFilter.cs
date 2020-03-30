// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Exception that gets thrown when an <see cref="IEventStreamFilter">filter</see> is missing a required <see cref="Attribute" />.
    /// </summary>
    public class MissingAttributeForFilter : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingAttributeForFilter"/> class.
        /// </summary>
        /// <param name="filterType">Type of <see cref="IEventStreamFilter"/>.</param>
        /// <param name="attributeType">The <see cref="Type" /> of the attribute that is missing.</param>
        /// <param name="attributeDefaultValueString">The default attribute default value string.</param>
        public MissingAttributeForFilter(Type filterType, Type attributeType, string attributeDefaultValueString)
            : base($"Missing [{attributeType.Name.Replace("Attribute", string.Empty, StringComparison.InvariantCulture)}({attributeDefaultValueString})] attribute on '{filterType.AssemblyQualifiedName}'.")
        {
        }
    }
}