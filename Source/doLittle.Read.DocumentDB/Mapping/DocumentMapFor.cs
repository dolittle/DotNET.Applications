/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Mapping;
using Microsoft.Azure.Documents;

namespace doLittle.Read.DocumentDB.Mapping
{
    /// <summary>
    /// Represents a concrete map for mapping any type to a <see cref="Document"/>
    /// </summary>
    public abstract class DocumentMapFor<T> : Map<T, Document>
    {
    }
}
