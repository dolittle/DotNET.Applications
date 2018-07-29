/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using Dolittle.Logging;
using Dolittle.Strings;

namespace Dolittle.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationStructureMapBuilder"/>
    /// </summary>
    public class ApplicationStructureMapBuilder : IApplicationStructureMapBuilder
    {
        readonly IEnumerable<IStringFormat> _structureFormats;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureMapBuilder"/>
        /// </summary>
        public ApplicationStructureMapBuilder()
        {
            _structureFormats = new IStringFormat[0];
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureMapBuilder"/>
        /// </summary>
        /// <param name="structureFormats">Current structure formats</param>
        public ApplicationStructureMapBuilder(IEnumerable<IStringFormat> structureFormats)
        {
            _structureFormats = structureFormats;
        }


        /// <inheritdoc/>
        public IApplicationStructureMap Build(IApplication application)
        {
            var applicationStructure = new ApplicationStructureMap(application, _structureFormats);
            return applicationStructure;
        }

        /// <inheritdoc/>
        public IApplicationStructureMapBuilder Include(string format)
        {
            Logger.Internal.Trace($"Include '{format}'");

            if ( !format.StartsWith("[")) format = $"[.]{format}";
            var parser = new StringFormatParser();
            var stringFormat = parser.Parse(format);

            var formats = new List<IStringFormat>(_structureFormats);
            formats.Add(stringFormat);

            var builder = new ApplicationStructureMapBuilder(formats);
            return builder;
        }
    }
}