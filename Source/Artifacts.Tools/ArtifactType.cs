using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Dolittle.Artifacts.Configuration;

namespace Dolittle.Artifacts.Tools
{
    class ArtifactType
    {
        public Type Type { get; set; }
        public string TypeName { get; set; } 
        public Expression<Func<ArtifactsByTypeDefinition, IEnumerable<ArtifactDefinition>>> TargetPropertyExpression { get; set; }
    }
}