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
        readonly Dictionary<IApplicationArtifactIdentifier, Type> _AAIToCommand;

        readonly IApplicationArtifacts _applicationArtifacts;
        readonly ITypeFinder _typeFinder;
        readonly ILogger _logger;

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
        )
        {
            _applicationArtifacts = applicationArtifacts;
            _typeFinder = typeFinder;
            _logger = logger;

            _AAIToCommand = _typeFinder.FindMultiple<ICommand>().ToDictionary(c => _applicationArtifacts.Identify(c), c => c);
        }

        /// <inheritdoc/>
        public override Type Resolve(IApplicationArtifactIdentifier identifier)
        {
            _logger.Trace($"Resolving an {typeof(IApplicationArtifactIdentifier)} for the {typeof(IArtifactType)} {typeof(CommandArtifactType)}");

            ThrowIfCommandNotFound(identifier);

            var matchedType = _AAIToCommand[identifier];

            _logger.Trace($"Successfully resolved the {typeof(IApplicationArtifactIdentifier)} to {matchedType.AssemblyQualifiedName}");

            return matchedType;
        }

        void ThrowIfCommandNotFound(IApplicationArtifactIdentifier identifier)
        {
            if (_AAIToCommand.ContainsKey(identifier))
                throw new CommandNotFound(identifier);
        }
    }
}