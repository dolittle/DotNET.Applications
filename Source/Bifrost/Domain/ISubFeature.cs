﻿/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
namespace Bifrost.Domain
{
    /// <summary>
    /// Defines a <see cref="Feature">feature</see> inside a <see cref="Feature">feature</see>
    /// </summary>
    public interface ISubFeature : IFeature
    {

        /// <summary>
        /// Gets the <see cref="Feature">parent feature</see>
        /// </summary>
        IFeature Parent { get; }
    }
}
