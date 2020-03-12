// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Dolittle.Configuration;
using Dolittle.Tenancy;

namespace Dolittle.EventHorizon
{
    /// <summary>
    /// Represents the configuration for hosts by <see cref="EventHorizonsConfiguration"/>.
    /// </summary>
    [Name(ConfigurationName)]
    public class EventHorizonsConfiguration :
        ReadOnlyDictionary<TenantId, IReadOnlyList<EventHorizon>>,
        IConfigurationObject
    {
        /// <summary>
        /// The name of the configuration.
        /// </summary>
        public const string ConfigurationName = "event-horizons";

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHorizonsConfiguration"/> class.
        /// </summary>
        /// <param name="configuration">Dictionary for <see cref="TenantId"/> with <see cref="IReadOnlyList{T}" /> of ><see cref="EventHorizon"/>.</param>
        public EventHorizonsConfiguration(IDictionary<TenantId, IReadOnlyList<EventHorizon>> configuration)
            : base(configuration)
        {
        }
    }
}