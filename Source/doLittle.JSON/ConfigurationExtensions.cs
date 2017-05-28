/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using doLittle.JSON.Serialization;
using doLittle.Serialization;

namespace doLittle.Configuration
{
    /// <summary>
    /// Extensions for configuring <see cref="ISerializer"/>
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Configure using Newtonsoft.Json
        /// </summary>
        /// <param name="serializationConfiguration"><see cref="ISerializationConfiguration">Configuration</see> to configure</param>
        /// <returns>Chainged instance of <see cref="IConfigure"/></returns>
        public static IConfigure UsingJson(this ISerializationConfiguration serializationConfiguration) 
        {
            serializationConfiguration.SerializerType = typeof(Serializer);
            return Configure.Instance;
        }
    }
}
