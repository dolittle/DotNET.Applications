/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using doLittle.Configuration;
using doLittle.Diagnostics;
using doLittle.Read;

namespace doLittle.Web.Visualizer.QualityAssurance
{


    public class AllProblems : IQueryFor<Problems>
    {
        IProblemsReporter _reporter;

        public AllProblems(IProblemsReporter reporter)
        {
            _reporter = reporter;
            reporter.Clear();
            Configure.Instance.QualityAssurance.Validate();
        }

        public IQueryable<Problems> Query
        {
            get
            {
                return _reporter.All.Select(p => new Problems
                {
                    All = p.Select(pr => new Problem
                    {
                        Type = pr.Type,
                        Data = pr.Data
                    })
                }).AsQueryable();
            }
        }
    }
}
