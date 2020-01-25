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
        /// <typeparam name="T">The type of interface to check for.</typeparam>
        /// <param name="classDeclaration"><see cref="ClassDeclarationSyntax"/> to check.</param>
        /// <param name="model"><see cref="SemanticModel"/> to use.</param>
        /// <returns>true if it inherits, false if not.</returns>
        public static bool ImplementsInterfaceOf<T>(this ClassDeclarationSyntax classDeclaration, SemanticModel model)
            where T : class
        {
            var @interface = typeof(T);
            if (!@interface.IsInterface)
            {
                throw new TypeMustBeAnInterface(@interface);
            }

            var classSymbol = model.GetDeclaredSymbol(classDeclaration);
            var interfaceDeclaration = classSymbol.AllInterfaces.FirstOrDefault(_ =>
                _.ContainingAssembly.Name.Equals(@interface.Assembly.GetName().Name, StringComparison.InvariantCulture) &&
                _.ContainingNamespace.ToDisplayString().Equals(@interface.Namespace, StringComparison.InvariantCulture) &&
                _.Name.Equals(@interface.Name, StringComparison.InvariantCulture));

            return interfaceDeclaration != default;
        }
    }
}