// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Dolittle.Events.Coordination
{
    /// <summary>
    /// Exception that gets thrown when there are no envelopes.
    /// </summary>
    public class MissingEnvelopes : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingEnvelopes"/> class.
        /// </summary>
        public MissingEnvelopes()
            : base("There are no envelops")
        {
        }
    }
}