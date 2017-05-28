/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.Configuration;
using FluentValidation;

namespace doLittle.FluentValidation
{
    /// <summary>
    /// Configures Validation
    /// </summary>
    public class Configurator : ICanConfigure
    {
        /// <summary>
        /// Instantiates the Configurator for Validation
        /// </summary>
        /// <param name="configure"></param>
        public void Configure(IConfigure configure)
        {
            ValidatorOptions.DisplayNameResolver = NameResolvers.DisplayNameResolver;
            ValidatorOptions.PropertyNameResolver = NameResolvers.PropertyNameResolver;
        }
    }
}