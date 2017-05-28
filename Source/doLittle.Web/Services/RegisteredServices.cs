/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using doLittle.Execution;

namespace doLittle.Web.Services
{
    [Singleton]
    public class RegisteredServices : IRegisteredServices
    {
        Dictionary<Type, string> _services = new Dictionary<Type, string>();

        public IEnumerable<KeyValuePair<Type, string>> GetAll()
        {
            return _services;
        }

        public void Register(Type service, string url)
        {
            _services[service] = url;
        }

    }
}
