/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Text;
using System.Linq;
using doLittle.CodeGeneration;
using doLittle.CodeGeneration.JavaScript;
using doLittle.Execution;
using doLittle.Strings;
using doLittle.Read;
using doLittle.Web.Configuration;
using doLittle.Web.Proxies;
using System.Reflection;
using doLittle.Types;

namespace doLittle.Web.Read
{
    public class QueryProxies : IProxyGenerator
    {
        ITypeFinder _typeFinder;
        ICodeGenerator _codeGenerator;
        WebConfiguration _configuration;

        public QueryProxies(ITypeFinder typeDiscoverer, ICodeGenerator codeGenerator, WebConfiguration configuration)
        {
            _typeFinder = typeDiscoverer;
            _codeGenerator = codeGenerator;
            _configuration = configuration;
        }

        public string Generate()
        {
            var typesByNamespace = _typeFinder.FindMultiple(typeof(IQueryFor<>)).GroupBy(t => t.Namespace);

            var result = new StringBuilder();

            Namespace currentNamespace;
            Namespace globalRead = _codeGenerator.Namespace(Namespaces.READ);

            foreach (var @namespace in typesByNamespace)
            {
                if (_configuration.NamespaceMapper.CanResolveToClient(@namespace.Key))
                    currentNamespace = _codeGenerator.Namespace(_configuration.NamespaceMapper.GetClientNamespaceFrom(@namespace.Key));
                else
                    currentNamespace = globalRead;

                foreach (var type in @namespace)
                {
                    var name = type.Name.ToCamelCase();
                    var queryForTypeName = type.GetTypeInfo().GetInterface(typeof(IQueryFor<>).Name).GetGenericArguments()[0].Name.ToCamelCase();
                    currentNamespace.Content.Assign(name)
                        .WithType(t =>
                            t.WithSuper("doLittle.read.Query")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .Property("_name", p => p.WithString(name))
                                        .Property("_generatedFrom", p => p.WithString(type.FullName))
                                        .Property("_readModel", p => p.WithLiteral(currentNamespace.Name + "." + queryForTypeName))
                                        .WithObservablePropertiesFrom(type, excludePropertiesFrom: typeof(IQueryFor<>), propertyVisitor: (p) => p.Name != "Query"));

                }
                if (currentNamespace != globalRead)
                    result.Append(_codeGenerator.GenerateFrom(currentNamespace));
            }

            result.Append(_codeGenerator.GenerateFrom(globalRead));
            return result.ToString();
        }

    }
}
