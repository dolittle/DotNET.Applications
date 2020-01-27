// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Events;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dolittle.SDK.CodeAnalysis.EventsMustHaveMatchingConstructorArguments
{
    [TestClass]
    public class UnitTests : CodeFixVerifier
    {
        [TestMethod]
        public void EventWithoutProperties()
        {
            const string content = @"
                using Dolittle.Events;

                namespace MyNamespace
                {
                    public class MyEvent : IEvent
                    {
                    }
                }
            ";

            VerifyCSharpDiagnostic(content);
        }

        [TestMethod]
        public void EventWithMatchingConstructorArguments()
        {
            const string content = @"
                using Dolittle.Events;

                namespace MyNamespace
                {
                    public class MyEvent : IEvent
                    {
                        public MyEvent(int first, string second)
                        {
                            First = first;
                            Second = second;
                        }

                        public int First { get; }

                        public string Second { get; }
                    }
                }
            ";

            VerifyCSharpDiagnostic(content);
        }

        [TestMethod]
        public void EventWithAPropertyAndNoConstructorArguments()
        {
            const string content = @"
                using Dolittle.Events;

                namespace MyNamespace
                {
                    public class MyEvent : IEvent
                    {
                        public int First { get; }
                    }
                }
            ";

            var firstProperty = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = string.Format(null, Analyzer.Rule.MessageFormat.ToString(), "First", "first"),
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 8, 25)
                }
            };

            VerifyCSharpDiagnostic(content, firstProperty);
        }

        [TestMethod]
        public void EventWithTooFewConstructorArguments()
        {
            const string content = @"
                using Dolittle.Events;

                namespace MyNamespace
                {
                    public class MyEvent : IEvent
                    {
                        public MyEvent(int first)
                        {
                            First = first;
                            Second = \""something\"";
                        }

                        public int First { get; }

                        public string Second { get; }
                    }
                }
            ";
            var secondProperty = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = string.Format(null, Analyzer.Rule.MessageFormat.ToString(), "Second", "second"),
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 16, 25)
                }
            };
            VerifyCSharpDiagnostic(content, secondProperty);
        }

        [TestMethod]
        public void EventWithMisMatchingConstructorArguments()
        {
            const string content = @"
                using Dolittle.Events;

                namespace MyNamespace
                {
                    public class MyEvent : IEvent
                    {
                        public MyEvent(int firstSomething, string secondSomething)
                        {
                            First = firstSomething;
                            Second = secondSomething;
                        }

                        public int First { get; }

                        public string Second { get; }
                    }
                }
            ";
            var firstProperty = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = string.Format(null, Analyzer.Rule.MessageFormat.ToString(), "First", "first"),
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 14, 25)
                }
            };

            var secondProperty = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = string.Format(null, Analyzer.Rule.MessageFormat.ToString(), "Second", "second"),
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 16, 25)
                }
            };

            // while (!System.Diagnostics.Debugger.IsAttached) System.Threading.Thread.Sleep(100);
            VerifyCSharpDiagnostic(content, firstProperty, secondProperty);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new Analyzer();
        }

        protected override IEnumerable<string> AdditionalAssemblyReferences => new[] { typeof(IEvent).Assembly.FullName };
    }
}