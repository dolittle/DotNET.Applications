/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;

namespace doLittle.Execution
{
    /// <summary>
    /// Gets called when an assembly is added
    /// </summary>
    /// <param name="assembly"><see cref="Assembly"/> that was added</param>
    public delegate void AssemblyAdded(Assembly assembly);
}
