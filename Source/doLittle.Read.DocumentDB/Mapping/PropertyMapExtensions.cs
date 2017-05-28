/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Mapping;

namespace doLittle.DocumentDB.Mapping
{
    /// <summary>
    /// Extensions for <see cref="IPropertyMap"/>
    /// </summary>
    public static class PropertyMapExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyMap"></param>
        /// <returns></returns>
        public static IPropertyMap Key(this IPropertyMap propertyMap)
        {
            propertyMap.Strategy = new KeyStrategy();
            return propertyMap;
        }
    }
}
