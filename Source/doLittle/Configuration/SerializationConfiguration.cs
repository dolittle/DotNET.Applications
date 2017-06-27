/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Serialization;
using System;
using doLittle.DependencyInversion;

namespace doLittle.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="ISerializationConfiguration"/>
    /// </summary>
    public class SerializationConfiguration : ISerializationConfiguration
    {
        /// <inheritdoc/>
        public Type SerializerType { get; set; }

        /// <inheritdoc/>
        public void Initialize (IContainer container)
        {
            if( SerializerType != null )
                container.Bind<ISerializer>(SerializerType, BindingLifecycle.Singleton);
        }
    }
}

