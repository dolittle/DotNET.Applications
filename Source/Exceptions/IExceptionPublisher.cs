/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.Exceptions
{
    /// <summary>
    /// Publishes exceptions to all <see cref="IExceptionSubscriber"/>s.
    /// </summary>
    public interface IExceptionPublisher
    {
        /// <summary>
        /// Publishes the exception to all <see cref="IExceptionSubscriber"/>.
        /// </summary>
        /// <param name="exception"></param>
        void Publish(Exception exception);
    }
}