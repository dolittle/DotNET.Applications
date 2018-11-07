/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/




namespace Dolittle.Build.given
{
    public class an_ILogger
    {
        protected static readonly IBuildToolLogger logger = Moq.Mock.Of<IBuildToolLogger>();
    }
}