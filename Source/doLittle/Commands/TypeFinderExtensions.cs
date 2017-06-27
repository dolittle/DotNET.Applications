/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using doLittle.Execution;
using doLittle.Extensions;
using doLittle.Types;

namespace doLittle.Commands
{
    /// <summary>
    /// Extensions methods for <see cref="ITypeFinder"/> for dealing with <see cref="ICommand"/>
    /// </summary>
    public static class TypeFinderExtensions
    {
        /// <summary>
        /// Get the type of the command matching the fullname.  This can be in any loaded assembly and does not require the 
        /// </summary>
        /// <param name="typeFinder">instance of <see cref="ITypeFinder"/> being extended</param>
        /// <param name="fullName">The full name of the type</param>
        /// <returns>the type if found, <see cref="UnknownCommandException" /> if not found or type is not a command</returns>
        public static Type GetCommandTypeByName(this ITypeFinder typeFinder, string fullName)
        {
            var commandType = typeFinder.FindTypeByFullName(fullName);

            if(commandType == null || !commandType.HasInterface(typeof(ICommand)))
                throw new UnknownCommandException(fullName);

            return commandType;
        }
    }
}
