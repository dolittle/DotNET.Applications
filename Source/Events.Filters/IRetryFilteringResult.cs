// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Dolittle.Events.Processing;

namespace Dolittle.Events.Filters
{
    /// <summary>
    /// Defines the filtering result for when filtering should be tried again.
    /// </summary>
    public interface IRetryFilteringResult : IRetryProcessingResult, IFilterResult
    {
    }
}