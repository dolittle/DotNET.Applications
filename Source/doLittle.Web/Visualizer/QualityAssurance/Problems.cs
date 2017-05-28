/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using doLittle.Diagnostics;
using doLittle.Read;

namespace doLittle.Web.Visualizer.QualityAssurance
{
    public class Problems : IReadModel
    {
        public IEnumerable<Problem> All { get; set; }
    }
}
