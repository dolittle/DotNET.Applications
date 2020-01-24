// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Dolittle.SDK.CodeAnalysis
{
    /// <summary>
    /// General extension methods for code analysis.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Check if a <see cref="ClassDeclarationSyntax"/> implements an event.
        /// </summary>
        /// <param name="classDeclaration"><see cref="ClassDeclarationSyntax"/> to check.</param>
        /// <param name="model"><see cref="SemanticModel"/> to use.</param>
        /// <returns>true if it inherits, false if not.</returns>
        public static bool ImplementsAnEvent(this ClassDeclarationSyntax classDeclaration, SemanticModel model)
        {
            var classSymbol = model.GetDeclaredSymbol(classDeclaration);
            var interfaces = classDeclaration
                .SyntaxTree
                .GetRoot()
                .DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>();
            return classSymbol.BaseType.ContainingNamespace.Name.Equals("Dolittle.Events", StringComparison.InvariantCulture) &&
                classSymbol.BaseType.Name.EndsWith("IEvent", StringComparison.InvariantCulture);
        }
    }
}