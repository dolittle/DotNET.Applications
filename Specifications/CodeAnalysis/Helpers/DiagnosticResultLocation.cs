// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

#pragma warning disable SA1629, SA1116, SA1117, CA2000, SA1124, SA1515, SA1202, SA1513, CA1825, SA1005, SA1202, SA1028, SA1202, CA1829, CA1815

namespace Dolittle.SDK.CodeAnalysis
{
    /// <summary>
    /// Location where the diagnostic appears, as determined by path, line number, and column number.
    /// </summary>
    public struct DiagnosticResultLocation
    {
        public DiagnosticResultLocation(string path, int line, int column)
        {
            if (line < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(line), "line must be >= -1");
            }

            if (column < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(column), "column must be >= -1");
            }

            Path = path;
            Line = line;
            Column = column;
        }

        public string Path { get; }

        public int Line { get; }

        public int Column { get; }
    }
}
