/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Dolittle.Artifacts.Build
{
    /// <summary>
    /// Represents a task for consolidating code artifacts with its configured artifacts in the
    /// mapping file
    /// </summary>
    public class ConsolidateArtifacts : ToolTask
    {
        /// <summary>
        /// Gets or sets the base assembly file used. This is typically the output assembly
        /// </summary>
        [Required]
        public ITaskItem AssemblyFile { get; set; }

        /// <inheritdoc/>
        protected override string ToolName => "Dolittle.SDK.Artifacts.Tools.dll";

        /// <inheritdoc/>
        protected override string GenerateFullPathToTool()
        {
            return "dotnet";
        }

        /// <inheritdoc/>
        protected override string GenerateCommandLineCommands()
        {
            var assemblyPath = typeof(ConsolidateArtifacts).Assembly.Location;
            var fileInfo = new FileInfo(assemblyPath);
            var directory = fileInfo.Directory;
            var path = Path.Combine(directory.FullName,$"{ToolName}");
            var targetAssemblyPath = AssemblyFile.GetMetadata("FullPath");
            var commandLine = $"{path} {targetAssemblyPath}";
            return commandLine;
        }


        /// <inheritdoc/>
        protected override MessageImportance StandardOutputLoggingImportance { get; } = MessageImportance.High;

        /// <inheritdoc/>
        protected override MessageImportance StandardErrorLoggingImportance { get; } = MessageImportance.High;        
   }
}