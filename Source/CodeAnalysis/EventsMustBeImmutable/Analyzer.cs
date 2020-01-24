// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Dolittle.SDK.CodeAnalysis.EventsMustBeImmutable
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
             id: "DL1001",
             title: "EventsMustBeImmutable",
             messageFormat: "Events must be immutable. Public properties with setters not allowed.",
             category: "Style",
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
            if (owningClass != default)
            {
                while (!System.Diagnostics.Debugger.IsAttached) System.Threading.Thread.Sleep(10);
                if (owningClass.ImplementsAnEvent(context.SemanticModel))
                {
                    var diagnostic = Diagnostic.Create(Rule, owningClass.GetLocation());
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
