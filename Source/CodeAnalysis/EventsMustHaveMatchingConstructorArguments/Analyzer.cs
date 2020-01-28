// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Dolittle.SDK.CodeAnalysis.EventsMustHaveMatchingConstructorArguments
{
    /// <summary>
    /// Represents a <see cref="DiagnosticAnalyzer"/> that does not allow the use of 'private' keyword.
    /// </summary>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class Analyzer : DiagnosticAnalyzer
    {
        /// <summary>
        /// Represents the <see cref="DiagnosticDescriptor">rule</see> for the analyzer.
        /// </summary>
        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
             id: "DL1002",
             title: "EventsMustHaveMatchingConstructorArguments",
             messageFormat: "Events must have matching constructor arguments. Property '{0}' does not have a matching constructor parameter '{1}'",
             category: "Events",
             defaultSeverity: DiagnosticSeverity.Error,
             isEnabledByDefault: true,
             description: null,
             helpLinkUri: $"",
             customTags: Array.Empty<string>());

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(
                HandlePropertyDeclaration,
                ImmutableArray.Create(SyntaxKind.PropertyDeclaration));
        }

        void HandlePropertyDeclaration(SyntaxNodeAnalysisContext context)
        {
            var owningClass = context.Node.FirstAncestorOrSelf<ClassDeclarationSyntax>();
            if (owningClass != default && owningClass.ImplementsInterface("Dolittle.Events", "IEvent", context.SemanticModel))
            {
                var property = context.Node as PropertyDeclarationSyntax;
                var constructor = owningClass.Members.OfType<ConstructorDeclarationSyntax>().FirstOrDefault();
                var propertyName = property.Identifier.ValueText;
                var constructorArguments = constructor != default ? constructor.ParameterList.Parameters.Select(_ => _.Identifier.ValueText) : Array.Empty<string>();
                if (!constructorArguments.Any(arg => AsPascalCase(arg).Equals(propertyName, StringComparison.InvariantCulture)))
                {
                    var diagnostic = Diagnostic.Create(Rule, property.GetLocation(), propertyName, AsCamelCase(propertyName));
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        string AsPascalCase(string str) => str[0].ToString(CultureInfo.InvariantCulture).ToUpperInvariant() + str.Substring(1);

#pragma warning disable CA1308
        string AsCamelCase(string str) => str[0].ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture) + str.Substring(1);
    }
}
