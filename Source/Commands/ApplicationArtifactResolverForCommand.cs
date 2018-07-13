using System;
using System.Collections.Generic;
using System.Linq;
using Dolittle.Applications;
using Dolittle.Artifacts;
using Dolittle.Logging;
using Dolittle.Types;

namespace Dolittle.Commands
{
    /// <inheritdoc/>
    public class ApplicationArtifactResolverForCommand : ApplicationArtifactResolverFor<CommandArtifactType>
    {
        /// <summary>
        /// Initialize a new instance of <see cref="ApplicationArtifactResolverForCommand"/>
        /// </summary>
        /// <param name="applicationArtifacts"></param>
        /// <param name="typeFinder"></param>
        /// <param name="logger"></param>
        public ApplicationArtifactResolverForCommand(
            IApplicationArtifacts applicationArtifacts,
            ITypeFinder typeFinder,
            ILogger logger
        ) : base(applicationArtifacts, typeFinder, logger)
        { }
    }
}