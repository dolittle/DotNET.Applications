/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using doLittle.Applications;
using doLittle.CodeGeneration;
using doLittle.CodeGeneration.JavaScript;
using doLittle.Commands;
using doLittle.Execution;
using doLittle.Extensions;
using doLittle.Types;
using doLittle.Web.Configuration;
using doLittle.Web.Proxies;

namespace doLittle.Web.Commands
{
    public class CommandProxies : IProxyGenerator
    {
        internal static List<string> _namespacesToExclude = new List<string>();

        IApplicationResources _applicationResources;
        IApplicationResourceIdentifierConverter _applicationResourceIdentifierConverter;
        ITypeFinder _typeFinder;
        IInstancesOf<ICanExtendCommandProperty> _commandPropertyExtenders;
        ICodeGenerator _codeGenerator;
        WebConfiguration _configuration;

        static CommandProxies()
        {
            ExcludeCommandsStartingWithNamespace("doLittle");
        }

        public static void ExcludeCommandsStartingWithNamespace(string @namespace)
        {
            _namespacesToExclude.Add(@namespace);
        }

        public CommandProxies(
            IApplicationResources applicationResources,
            IApplicationResourceIdentifierConverter applicationResourceIdentifierConverter, 
            ITypeFinder typeFinder, 
            IInstancesOf<ICanExtendCommandProperty> commandPropertyExtenders,
            ICodeGenerator codeGenerator, 
            WebConfiguration configuration)
        {
            _applicationResources = applicationResources;
            _applicationResourceIdentifierConverter = applicationResourceIdentifierConverter;
            _typeFinder = typeFinder;
            _commandPropertyExtenders = commandPropertyExtenders;
            _codeGenerator = codeGenerator;
            
            _configuration = configuration;
        }

        public string Generate()
        {
            var typesByNamespace = _typeFinder.FindMultiple<ICommand>().Where(t => !_namespacesToExclude.Any(n => t.Namespace.StartsWith(n))).GroupBy(t=>t.Namespace);

            var result = new StringBuilder();

            Namespace currentNamespace;
            Namespace globalCommands = _codeGenerator.Namespace(Namespaces.COMMANDS);

            foreach (var @namespace in typesByNamespace)
            {
                if (_configuration.NamespaceMapper.CanResolveToClient(@namespace.Key))
                    currentNamespace = _codeGenerator.Namespace(_configuration.NamespaceMapper.GetClientNamespaceFrom(@namespace.Key));
                else
                    currentNamespace = globalCommands;
                
                foreach (var type in @namespace)
                {
                    if (type.GetTypeInfo().IsGenericType) continue;

                    var identifier = _applicationResources.Identify(type);
                    var identifierAsString = _applicationResourceIdentifierConverter.AsString(identifier);

                    var name = ((string)identifier.Resource.Name).ToCamelCase();
                    currentNamespace.Content.Assign(name)
                        .WithType(t =>
                            t.WithSuper("doLittle.commands.Command")
                                .Function
                                    .Body
                                        .Variant("self", v => v.WithThis())
                                        .Property("_commandType", p => p.WithString(identifierAsString))

                                        .WithObservablePropertiesFrom(type, excludePropertiesFrom: typeof(ICommand), observableVisitor: (propertyName, observable) =>
                                        {
                                            foreach (var commandPropertyExtender in _commandPropertyExtenders)
                                                commandPropertyExtender.Extend(type, propertyName, observable);
                                        }));
                }

                if (currentNamespace != globalCommands)
                    result.Append(_codeGenerator.GenerateFrom(currentNamespace));
            }
            result.Append(_codeGenerator.GenerateFrom(globalCommands));
            
            return result.ToString();
        }
    }
}
