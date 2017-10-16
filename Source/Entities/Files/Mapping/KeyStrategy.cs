/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Mapping;

namespace doLittle.Entities.Files.Mapping
{
    /// <summary>
    /// Represents an implementation of <see cref="IPropertyMappingStrategy"/> for defining a key for a <see cref="Document"/>
    /// </summary>
    public class KeyStrategy : IPropertyMappingStrategy
    {
#pragma warning disable 1591 // Xml Comments
        public void Perform(IMappingTarget mappingTarget, object target, object value)
        {
            var dynamicTarget = (dynamic)target;
            dynamicTarget.Id = value;
        }
#pragma warning restore 1591 // Xml Comments
    }
}
