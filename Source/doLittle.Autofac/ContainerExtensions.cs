/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.DependencyInversion;

namespace doLittle.Autofac
{

    /// <summary>
    /// Provides extensions to <see cref="Container"/>
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Sets AutoFac lifestyle from <see cref="BindingLifecycle"/>
        /// </summary>
        /// <typeparam name="TLimit">Type of Limit</typeparam>
        /// <typeparam name="TActivatorData">Typeof ActivatorData</typeparam>
        /// <typeparam name="TRegistrationStyle">Type of Registration Style</typeparam>
        /// <param name="builder">AutoFac <see cref="global::Autofac.Builder.IRegistrationBuilder{TLimit, TActivatorData, TRegistrationStyle}"/></param>
        /// <param name="lifecycle">The <see cref="BindingLifecycle">lifecycle</see> to apply</param>
        /// <returns>Chained <see cref="global::Autofac.Builder.IRegistrationBuilder{TLimit, TActivatorData, TRegistrationStyle}"/></returns>
        public static global::Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
            PerLifeStyle<TLimit, TActivatorData, TRegistrationStyle>
            (this global::Autofac.Builder.IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder,
             BindingLifecycle lifecycle)
        { 
            //no thread lifecycle
            switch (lifecycle)
            {
                case BindingLifecycle.Transient:
                    return builder.InstancePerDependency();
                case BindingLifecycle.Singleton:
                    return builder.SingleInstance();
                default:
                    return builder.InstancePerDependency();
            }
        }
    }
}