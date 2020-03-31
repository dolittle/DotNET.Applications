// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Artifacts;
using Dolittle.Events.EventHorizon;

namespace Dolittle.Events.Handling.EventHorizon
{
    /// <summary>
    /// Exception that gets thrown when an event marked with <see cref="IExternalEvent" /> does not have an <see cref="ArtifactAttribute" />.
    /// </summary>
    public class ExternalEventMustHaveArtifactAttribute : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalEventMustHaveArtifactAttribute"/> class.
        /// </summary>
        /// <param name="eventType">The <see cref="Type" /> of the event.</param>
        public ExternalEventMustHaveArtifactAttribute(Type eventType)
            : base($"The '{typeof(IExternalEvent).Name}' event '{eventType.AssemblyQualifiedName}' must have an '{typeof(ArtifactAttribute).FullName}' on the class.")
        {
        }
    }
}