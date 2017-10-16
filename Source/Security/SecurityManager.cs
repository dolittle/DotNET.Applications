/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using doLittle.Execution;
using doLittle.Types;
using doLittle.DependencyInversion;

namespace doLittle.Security
{
    /// <summary>
    /// Represents an implementation of <see cref="ISecurityManager"/>
    /// </summary>
    [Singleton]
    public class SecurityManager : ISecurityManager
    {
        readonly ITypeFinder _typeFinder;
        readonly IContainer _container;
        IEnumerable<ISecurityDescriptor> _securityDescriptors;

        /// <summary>
        /// Initializes a new instance of <see cref="SecurityManager"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> to discover any <see cref="BaseSecurityDescriptor">security descriptors</see></param>
        /// <param name="container"><see cref="IContainer"/> to instantiate instances of <see cref="ISecurityDescriptor"/></param>
        public SecurityManager(ITypeFinder typeFinder, IContainer container)
        {
            _typeFinder = typeFinder;
            _container = container;

            PopulateSecurityDescriptors();
        }

        void PopulateSecurityDescriptors()
        {
            var securityDescriptorTypes = _typeFinder.FindMultiple<ISecurityDescriptor>();
            var instances = new List<ISecurityDescriptor>();
            instances.AddRange(securityDescriptorTypes.Select(t => _container.Get(t) as ISecurityDescriptor));
            _securityDescriptors = instances;
        }

        /// <inheritdoc/>
        public AuthorizationResult Authorize<T>(object target) where T : ISecurityAction
        {
            var result = new AuthorizationResult();
            if (!_securityDescriptors.Any())
                return result;

            var applicableSecurityDescriptors = _securityDescriptors.Where(sd => sd.CanAuthorize<T>(target));

            if (!applicableSecurityDescriptors.Any())
                return result;

            foreach(var securityDescriptor in applicableSecurityDescriptors)
                result.ProcessAuthorizeDescriptorResult(securityDescriptor.Authorize(target));

            return result;
        }
    }
}
