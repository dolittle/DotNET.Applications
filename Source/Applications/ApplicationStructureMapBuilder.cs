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
        readonly IDictionary<ApplicationArea, IEnumerable<IStringFormat>> _structureFormats;


        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureMapBuilder"/>
        /// </summary>
        public ApplicationStructureMapBuilder()
        {
            _structureFormats = new Dictionary<ApplicationArea, IEnumerable<IStringFormat>>();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationStructureMapBuilder"/>
        /// </summary>
        /// <param name="structureFormats">Current structure formats</param>
        public ApplicationStructureMapBuilder(IDictionary<ApplicationArea, IEnumerable<IStringFormat>> structureFormats)
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
        public IApplicationStructureMapBuilder Include(ApplicationArea area, string format)
        {
            Logger.Internal.Trace($"Include '{format}' for '{area}'");

            if ( !format.StartsWith("[")) format = $"[.]{format}";
            var formatsByArea = new Dictionary<ApplicationArea, IEnumerable<IStringFormat>>(_structureFormats);
            var parser = new StringFormatParser();
            var stringFormat = parser.Parse(format);

            List<IStringFormat> formats;
            if (formatsByArea.ContainsKey(area))
                formats = new List<IStringFormat>(formatsByArea[area]);
            else
                formats = new List<IStringFormat>();

            formats.Add(stringFormat);
            formatsByArea[area] = formats;

            var builder = new ApplicationStructureMapBuilder(formatsByArea);
            return builder;
        }
    }
}