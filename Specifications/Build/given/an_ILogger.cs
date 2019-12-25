// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Dolittle.Build.given
{
    public class an_ILogger
    {
        protected static readonly IBuildMessages build_messages = Moq.Mock.Of<IBuildMessages>();
    }
}