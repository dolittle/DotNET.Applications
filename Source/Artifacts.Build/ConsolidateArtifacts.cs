/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Dolittle.SDK.Artifacts.Build
{
    /// <summary>
    /// Represents a task for consolidating code artifacts with its configured artifacts in the
    /// mapping file
    /// </summary>
    public class ConsolidateArtifacts : Task
    {
        /// <summary>
        /// Gets or sets the base assembly file used. This is typically the output assembly
        /// </summary>
        [Required]
        public ITaskItem AssemblyFile { get; set; }

        /// <inheritdoc/>
        public override bool Execute()
        {
            System.Console.WriteLine($"Consolidate for : {AssemblyFile.ItemSpec}");
            return true;
        }
    }
}