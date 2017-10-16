/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.DependencyInversion;
using doLittle.Execution;
using doLittle.Runtime.Execution;

namespace doLittle.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IExecutionContextConfiguration"/>
    /// </summary>
    public class ExecutionContextConfiguration : IExecutionContextConfiguration
    {
        /// <inheritdoc/>
        public void Initialize(IContainer container)
        {
            container.Bind(() => container.Get<IExecutionContextManager>().Current);
        }
    }
}
