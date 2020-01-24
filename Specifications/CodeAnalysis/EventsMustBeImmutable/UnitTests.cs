// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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

            var expected = new DiagnosticResult
            {
                Id = Analyzer.Rule.Id,
                Message = (string)Analyzer.Rule.MessageFormat,
                Severity = Analyzer.Rule.DefaultSeverity,
                Locations = new[]
                {
                    new DiagnosticResultLocation("Test0.cs", 10, 29)
                }
            };

            VerifyCSharpDiagnostic(content, expected);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new Analyzer();
        }
    }
}