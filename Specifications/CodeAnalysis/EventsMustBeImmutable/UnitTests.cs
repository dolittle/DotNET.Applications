// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Dolittle.Events;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dolittle.SDK.CodeAnalysis.EventsMustBeImmutable
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
        public void ImmutableEvent()
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
        public void MutableEvent()
        {
            const string content = @"
                using Dolittle.Events;

                namespace MyNamespace
                {
                    public class MyEvent : IEvent
                    {
                        public int First { get; set; }

                        public string Second { get; set; }
                    }
                }
            ";

            var firstProperty = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 8, 25)
                }
            };

            var secondProperty = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 10, 25)
                }
            };

            VerifyCSharpDiagnostic(content, firstProperty, secondProperty);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new Analyzer();
        }

        protected override IEnumerable<string> AdditionalAssemblyReferences => new[] { typeof(IEvent).Assembly.FullName };
    }
}