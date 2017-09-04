/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.Concepts
{
    /// <summary>
    /// Provides extensions related to strings and conecpts
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// Convert a string into the desired type
        /// </summary>
        /// <param name="input">the string to parse</param>
        /// <param name="type">the desired type</param>
        /// <returns>value as the desired type</returns>
        public static object ParseTo(this string input, Type type)
        {
            if (type == typeof(Guid)) 
            {
                Guid result;
                if (Guid.TryParse(input, out result)) return result;
                return Guid.Empty;
            }

            if (type.IsConcept())
            {
                var primitiveType = type.GetConceptValueType();
                var primitive = ParseTo(input, primitiveType);
                return ConceptFactory.CreateConceptInstance(type, primitive);
            }

            return Convert.ChangeType(input, type, null);           
        }
    }
}