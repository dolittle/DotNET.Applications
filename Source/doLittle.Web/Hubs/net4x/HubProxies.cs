/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Reflection;
using System.Text;
using doLittle.CodeGeneration;
using doLittle.CodeGeneration.JavaScript;
using doLittle.Execution;
using doLittle.Extensions;
using doLittle.Web.Configuration;
using doLittle.Web.Proxies;
#if(NET461)
using Microsoft.AspNet.SignalR;
#else
using Microsoft.AspNetCore.SignalR;
#endif

namespace doLittle.Web.Hubs
{
    public class HubProxies : IProxyGenerator
    {
        ITypeDiscoverer _typeDiscoverer;
        ICodeGenerator _codeGenerator;
        WebConfiguration _configuration;

        public HubProxies(ITypeDiscoverer typeDiscoverer, ICodeGenerator codeGenerator, WebConfiguration configuration)
        {
            _typeDiscoverer = typeDiscoverer;
            _codeGenerator = codeGenerator;
            _configuration = configuration;
        }


        public string Generate()
        {
            var typesByNamespace = _typeDiscoverer.FindMultiple<Hub>().GroupBy(t=>t.Namespace);
            var result = new StringBuilder();

            Namespace currentNamespace;
            Namespace globalHubs = _codeGenerator.Namespace(Namespaces.HUBS);

            foreach (var @namespace in typesByNamespace)
            {
                if (_configuration.NamespaceMapper.CanResolveToClient(@namespace.Key))
                    currentNamespace = _codeGenerator.Namespace(_configuration.NamespaceMapper.GetClientNamespaceFrom(@namespace.Key));
                else
                    currentNamespace = globalHubs;

                foreach (var type in @namespace)
                {
                    if (type.GetTypeInfo().IsGenericType) continue;

                    var name = type.Name.ToCamelCase();

                    currentNamespace.Content.Assign(name)
                        .WithType(t =>
                            t.WithSuper("doLittle.hubs.Hub")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .Property("_name", p => p.WithString(name))
                                        .WithServerMethodsFrom(type)
                        );
                }

                if (currentNamespace != globalHubs)
                    result.Append(_codeGenerator.GenerateFrom(currentNamespace));


            }
            result.Append(_codeGenerator.GenerateFrom(globalHubs));

            return result.ToString();
        }
    }
}
