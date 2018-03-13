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
        const string DolittleJson = "dolittle.json";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Config  GetConfig()
        {
            if( !File.Exists(DolittleJson) ) return new Config();

            var json = File.ReadAllText(DolittleJson);
            
            var config = JsonConvert.DeserializeObject(json, typeof(Config), new ConceptConverter()) as Config;
            return config;
        }
    }
}