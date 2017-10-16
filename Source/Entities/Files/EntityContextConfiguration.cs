/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using doLittle.Configuration;

namespace doLittle.Entities.Files
{
    /// <summary>
    /// Represents an implementation of <see cref="IEntityContextConfiguration"/> for the simple file based <see cref="EntityContext{T}"/>
    /// </summary>
    public class EntityContextConfiguration : IEntityContextConfiguration
    {
        /// <summary>
        /// Gets or sets the path of where to store files
        /// </summary>
        public string Path { get; set; }

#pragma warning disable 1591 // Xml Comments
        public Type EntityContextType
        {
            get { return typeof(EntityContext<>); }
        }

        public IEntityContextConnection Connection { get; set; }
#pragma warning restore 1591 // Xml Comments
    }
}
