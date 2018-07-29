/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Artifacts.Configuration
{
    /// <summary>
    /// Represents a wrapper for <see cref="Type"/> for serialization purposes
    /// During deserialization and later serialization, we want to preserve the string representation.
    /// When used in places where the actual type might not be there, we don't want to run the risk of losing
    /// the type string
    /// </summary>
    public class ClrType
    {
        /// <summary>
        /// Gets or sets the string representation of the <see cref="Type"/>
        /// </summary>
        /// <value></value>
        public string TypeString {  get; set; }

        /// <summary>
        /// Get actual <see cref="Type"/> for the <see cref="ClrType"/>
        /// </summary>
        /// <returns><see cref="Type"/> representing - or null</returns>
        /// <remarks>
        /// If the type can't be resolved through the type string, this method
        /// will return null.
        /// </remarks>
        public Type GetActualType()
        {
            try
            {
                return Type.GetType(TypeString);
            } 
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Create a <see cref="ClrType"/> from <see cref="Type"/>
        /// </summary>
        /// <param name="type"><see cref="Type"/> to create from</param>
        /// <returns>New instance of <see cref="ClrType"/></returns>
        public static ClrType FromType(Type type)
        {
            var name = $"{type.FullName}, {type.Assembly.GetName().Name}";
            var clrType = new ClrType { TypeString = name };
            return clrType;
        }
    }
}