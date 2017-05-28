/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Security;

namespace doLittle.Services
{
    /// <summary>
    /// Represents a <see cref="ISecurityAction"/> for services
    /// </summary>
    public class InvokeService : SecurityAction
    {
        /// <summary>
        /// Returns a string description of this <see cref="ISecurityAction"/>
        /// </summary>
        public override string ActionType
        {
            get { return "Invoke"; }
        }
    }
}
