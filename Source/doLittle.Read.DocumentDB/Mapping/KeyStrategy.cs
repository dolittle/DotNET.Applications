/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using doLittle.Mapping;

namespace doLittle.Read.DocumentDB.Mapping
{
    /// <summary>
    /// Represents an implementation of <see cref="IPropertyMappingStrategy"/> for defining a key
    /// </summary>
    public class KeyStrategy : IPropertyMappingStrategy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mappingTarget"></param>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void Perform(IMappingTarget mappingTarget, object target, object value)
        {
            throw new NotImplementedException();
        }
    }
}
