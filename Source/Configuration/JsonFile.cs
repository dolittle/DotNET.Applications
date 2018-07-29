/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Dolittle.Concepts.Serialization.Json;
using Newtonsoft.Json;

namespace Dolittle.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonFile
    {
        /// <summary>
        /// The filename of the dolittle configuration file
        /// </summary>
        public static string DolittleJsonFileName = "dolittle.json";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Config  GetConfig()
        {
            if( !File.Exists(DolittleJsonFileName) ) return new Config();

            var json = File.ReadAllText(DolittleJsonFileName);
            
            var config = JsonConvert.DeserializeObject(json, typeof(Config), new ConceptConverter()) as Config;
            return config;
        }
    }
}