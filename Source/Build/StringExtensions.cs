/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.IO;

namespace Dolittle.Build
{
    /// <summary>
    /// Extensions for the <see cref="string"/> type
    /// </summary>
    public static class StringExtensions
    {
        const string _colorPrepend = "\x1b[";
        const string _redCode = "31m";
        const string _yellowCode = "33m";
        const string _whiteCode = "37m";
        const string _resetCode = "0m";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToRedString(this string str)
        {
            return _colorPrepend + _redCode + str + _colorPrepend + _resetCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToYellowString(this string str)
        {
            return _colorPrepend + _yellowCode + str + _colorPrepend + _resetCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToWhiteString(this string str)
        {
            return _colorPrepend + _whiteCode + str + _colorPrepend + _resetCode;
        }
    }
}