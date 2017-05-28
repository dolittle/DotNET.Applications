/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Conventions;

namespace doLittle.Configuration
{
    /// <summary>
    /// Represents an interface for configuring doLittle.
    /// </summary>
    /// <remarks>
    /// An application can implement any number of these conventions.
    /// They will be called when the <see cref="IConfigure"/> object is initialized.
    /// </remarks>
    public interface ICanConfigure : IConvention
    {
        /// <summary>
        /// Gets called when the application can configure doLittle
        /// </summary>
        /// <param name="configure"><see cref="IConfigure"/> to configure</param>
        void Configure(IConfigure configure);
    }
}
