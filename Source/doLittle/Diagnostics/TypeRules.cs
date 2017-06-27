/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Reflection;
using doLittle.Execution;
using doLittle.DependencyInversion;
using doLittle.Types;

namespace doLittle.Diagnostics
{
    /// <summary>
    /// Represents an implementation of <see cref="ITypeRules"/>
    /// </summary>
    public class TypeRules : ITypeRules
    {
        ITypeFinder _typeFinder;
        IContainer _container;
        IProblemsFactory _problemsFactory;
        IProblemsReporter _problemsReporter;

        /// <summary>
        /// Initializes a new instance of <see cref="TypeRules"/>
        /// </summary>
        /// <param name="typeFinder"><see cref="ITypeFinder"/> used for discovering rules</param>
        /// <param name="container"><see cref="IContainer"/> used for getting instances</param>
        /// <param name="problemsFactory"><see cref="IProblemsFactory"/> used for creating problems</param>
        /// <param name="problemsReporter"><see cref="IProblemsReporter">Reporter</see> to use for reporting back any problems</param>
        public TypeRules(
                    ITypeFinder typeFinder, 
                    IContainer container, 
                    IProblemsFactory problemsFactory, 
                    IProblemsReporter problemsReporter)
        {
            _typeFinder = typeFinder;
            _container = container;
            _problemsFactory = problemsFactory;
            _problemsReporter = problemsReporter;
        }

        /// <inheritdoc/>
        public void ValidateAll()
        {
            var ruleTypes = _typeFinder.FindMultiple(typeof(ITypeRuleFor<>));
            foreach (var ruleType in ruleTypes)
            {
                var rule = (dynamic)_container.Get(ruleType);

                var typeForRule = ruleType.GetTypeInfo().GetInterface(typeof(ITypeRuleFor<>).Name).GetTypeInfo().GetGenericArguments()[0];
                var types = _typeFinder.FindMultiple(typeForRule);
                foreach (var type in types)
                {
                    var problems = _problemsFactory.Create();
                    rule.Validate(type, problems);

                    if (problems.Any)
                        _problemsReporter.Report(problems);
                }
            }
        }
    }
}
