/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace doLittle.Web
{
    public interface IPipe
    {
        void Before(IWebContext webContext);
        void After(IWebContext webContext);
    }
}

