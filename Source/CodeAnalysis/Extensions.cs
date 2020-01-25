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
        /// Check if a <see cref="ClassDeclarationSyntax"/> implements a specific interface.
        /// </summary>
        /// <param name="classDeclaration"><see cref="ClassDeclarationSyntax"/> to check.</param>
        /// <param name="namespace">The namespace the type sits in.</param>
        /// <param name="type">The name of the type.</param>
        /// <param name="model"><see cref="SemanticModel"/> to use.</param>
        /// <returns>true if it inherits, false if not.</returns>
        public static bool ImplementsInterface(this ClassDeclarationSyntax classDeclaration, string @namespace, string type, SemanticModel model)
        {
            var classSymbol = model.GetDeclaredSymbol(classDeclaration);
            var interfaceDeclaration = classSymbol.AllInterfaces.FirstOrDefault(_ =>
                _.ContainingNamespace.ToDisplayString().Equals(@namespace, StringComparison.InvariantCulture) &&
                _.Name.Equals(type, StringComparison.InvariantCulture));

            return interfaceDeclaration != default;
        }
    }
}