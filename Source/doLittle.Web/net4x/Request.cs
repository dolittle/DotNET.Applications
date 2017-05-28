/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Web;

namespace doLittle.Web
{
    public class Request : IWebRequest
    {
        HttpRequest    _actualRequest;
        
        public Request(HttpRequest actualRequest)
        {
            _actualRequest = actualRequest;
        }
        
        public string Path { get { return _actualRequest.Path; } }
    }
}

