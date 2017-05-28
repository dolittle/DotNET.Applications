/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using doLittle.CodeGeneration;
using doLittle.CodeGeneration.JavaScript;
using doLittle.Web.Configuration;
using doLittle.Web.Proxies;

namespace doLittle.Web.Configuration
{
    public class NamespaceConfigurationProxies : IProxyGenerator
    {
        ICodeGenerator _codeGenerator;
        WebConfiguration _configuration;

        public NamespaceConfigurationProxies(WebConfiguration configuration, ICodeGenerator codeGenerator)
        {
            _codeGenerator = codeGenerator;
            _configuration = configuration;
        }

        public string Generate()
        {
            var global = _codeGenerator
                .Global()
                    .Variant("namespaceMapper", v => v.WithFunctionCall(f=>f.WithName("doLittle.StringMapper.create")))
                    .WithNamespaceMappersFrom(_configuration.PathsToNamespaces)
                    .AssignAccessor("doLittle.namespaceMappers.default", a => a.WithLiteral("namespaceMapper"))
                    ;

            var result = _codeGenerator.GenerateFrom(global);
            return result;
        }
    }
}
