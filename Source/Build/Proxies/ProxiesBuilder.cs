/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using Dolittle.Logging;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// 
    /// </summary>
     public class ProxiesBuilder
    {
        readonly Type[] _artifactTypes;
        readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="artifactsTypes"></param>
        /// <param name="logger"></param>
        public ProxiesBuilder(Type[] artifactsTypes, ILogger logger)
        {
            _artifactTypes = artifactsTypes;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Build()
        {
            _logger.Information("Building proxies");
             var startTime = DateTime.UtcNow;

             var endTime = DateTime.UtcNow;
            var deltaTime = endTime.Subtract(startTime);
            _logger.Information($"Finished proxies build process. (Took {deltaTime.TotalSeconds} seconds)");
        }
    }
}