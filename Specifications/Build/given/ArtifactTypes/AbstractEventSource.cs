// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dolittle.Events;
using Dolittle.Rules;
using Dolittle.Runtime.Events;

namespace Specs.Feature
{
    public abstract class AbstractEventSource : IEventSource
    {
        public EventSourceId EventSourceId => throw new System.NotImplementedException();

        public EventSourceVersion Version => throw new System.NotImplementedException();

        public UncommittedEvents UncommittedEvents => throw new System.NotImplementedException();

        public IEnumerable<BrokenRule> BrokenRules => throw new System.NotImplementedException();

        public IEnumerable<RuleSetEvaluation> RuleSetEvaluations => throw new System.NotImplementedException();

        public RuleSetEvaluation Evaluate(params IRule[] rules)
        {
            throw new System.NotImplementedException();
        }

        public RuleSetEvaluation Evaluate(params Expression<Func<RuleEvaluationResult>>[] rules)
        {
            throw new System.NotImplementedException();
        }

        public void Apply(IEvent @event)
        {
            throw new System.NotImplementedException();
        }

        public void Commit()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void FastForward(EventSourceVersion version)
        {
            throw new System.NotImplementedException();
        }

        public void ReApply(CommittedEvents eventStream)
        {
            throw new System.NotImplementedException();
        }

        public void Rollback()
        {
            throw new System.NotImplementedException();
        }
    }
}