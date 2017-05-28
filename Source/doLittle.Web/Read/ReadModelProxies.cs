/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using System.Text;
using doLittle.CodeGeneration;
using doLittle.CodeGeneration.JavaScript;
using doLittle.Execution;
using doLittle.Extensions;
using doLittle.Read;
using doLittle.Web.Configuration;
using doLittle.Web.Proxies;

namespace doLittle.Web.Read
{
    public class ReadModelProxies : IProxyGenerator
    {
       
        ITypeDiscoverer _typeDiscoverer;
        ICodeGenerator _codeGenerator;
        WebConfiguration _configuration;

        public ReadModelProxies(ITypeDiscoverer typeDiscoverer, ICodeGenerator codeGenerator, WebConfiguration configuration)
        {
            _typeDiscoverer = typeDiscoverer;
            _codeGenerator = codeGenerator;
            _configuration = configuration;
        }

        public string Generate()
        {
            var typesByNamespace = _typeDiscoverer.FindMultiple<IReadModel>().GroupBy(t => t.Namespace);

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
                    currentNamespace.Content.Assign(name)
                        .WithType(t =>
                            t.WithSuper("doLittle.read.ReadModel")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .Property("_generatedFrom", p => p.WithString(type.FullName))
                                        .WithPropertiesFrom(type, typeof(IReadModel)));

                    currentNamespace.Content.Assign("readModelOf" + name.ToPascalCase())
                        .WithType(t =>
                            t.WithSuper("doLittle.read.ReadModelOf")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .Property("_name", p => p.WithString(name))
                                        .Property("_generatedFrom", p => p.WithString(type.FullName))
                                        .Property("_readModelType", p => p.WithLiteral(currentNamespace.Name+"." + name))
                                        .WithReadModelConvenienceFunctions(type));
                }

                if (currentNamespace != globalRead)
                    result.Append(_codeGenerator.GenerateFrom(currentNamespace));
            }
            result.Append(_codeGenerator.GenerateFrom(globalRead));
            return result.ToString();
        }
    }
}
