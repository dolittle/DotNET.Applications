/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/


using Dolittle.Logging;

namespace Dolittle.Build.given
{
    public class an_ILogger
    {
        protected static readonly ILogger logger = Moq.Mock.Of<ILogger>();
    }
}