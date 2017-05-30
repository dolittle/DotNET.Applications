/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using doLittle.Applications;
using doLittle.CodeGeneration;
using doLittle.CodeGeneration.JavaScript;
using doLittle.Commands;
using doLittle.Execution;
using doLittle.Extensions;
using doLittle.Lifecycle;
using doLittle.Types;
using doLittle.Web.Configuration;
using doLittle.Web.Proxies;

namespace doLittle.Web.Commands
{
    public class CommandSecurityProxies : IProxyGenerator
    {
        readonly ITypeDiscoverer _typeDiscoverer;
        readonly ICodeGenerator _codeGenerator;
        readonly ICommandSecurityManager _commandSecurityManager;
        readonly IApplicationResources _applicationResources;
        readonly WebConfiguration _configuration;

        public CommandSecurityProxies(
            ITypeDiscoverer typeDiscoverer,
            ICodeGenerator codeGenerator,
            ICommandSecurityManager commandSecurityManager,
            IApplicationResources applicationResources,
            WebConfiguration configuration)
        {
            _typeDiscoverer = typeDiscoverer;
            _codeGenerator = codeGenerator;
            _configuration = configuration;
            _applicationResources = applicationResources;
            _commandSecurityManager = commandSecurityManager;
        }

        public string Generate()
        {
            var typesByNamespace = _typeDiscoverer
                .FindMultiple<ICommand>()
                .Where(t => !CommandProxies._namespacesToExclude.Any(n => t.Namespace.StartsWith(n)))
                .GroupBy(t => t.Namespace);

            var result = new StringBuilder();
            var globalCommands = _codeGenerator.Namespace(Namespaces.COMMANDS);

            foreach (var @namespace in typesByNamespace)
            {
                Namespace currentNamespace;
                if (_configuration.NamespaceMapper.CanResolveToClient(@namespace.Key))
                {
                    currentNamespace = _codeGenerator.Namespace(_configuration.NamespaceMapper.GetClientNamespaceFrom(@namespace.Key));
                }
                else
                {
                    currentNamespace = globalCommands;
                }

                foreach (var type in @namespace)
                {
                    if (type.GetTypeInfo().IsGenericType) continue;
                    
                    var identifier = _applicationResources.Identify(type);
                    var command = new CommandRequest(TransactionCorrelationId.NotSet, identifier, new Dictionary<string,object>());

                    var authorizationResult = _commandSecurityManager.Authorize(command);
                    var name = $"{type.Name.ToCamelCase()}SecurityContext";
                    currentNamespace.Content.Assign(name)
                        .WithType(t => t
                            .WithSuper("doLittle.commands.CommandSecurityContext")
                            .Function
                            .Body
                            .Variant("self", v => v.WithThis())
                            .Access("this", a => a
                                .WithFunctionCall(f => f
                                    .WithName("isAuthorized")
                                    .WithParameters(authorizationResult.IsAuthorized.ToString().ToCamelCase())
                                )
                            )
                        );
                }

                if (currentNamespace != globalCommands)
                {
                    result.Append(_codeGenerator.GenerateFrom(currentNamespace));
                }
            }

            result.Append(_codeGenerator.GenerateFrom(globalCommands));
            return result.ToString();
        }
    }
}
