/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 doLittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using doLittle.Configuration;
using doLittle.Logging;
using doLittle.Web.Assets;
using doLittle.Web.Proxies;
using Newtonsoft.Json;

namespace doLittle.Web.Configuration
{
    public class ConfigurationAsJavaScript
    {
        ILogger _logger;

        WebConfiguration _webConfiguration;

        string _configurationAsString;

        public ConfigurationAsJavaScript(WebConfiguration webConfiguration, ILogger logger)
        {
            _webConfiguration = webConfiguration;
            _logger = logger;
        }

        string GetResource(string name)
        {
            try
            {
                var stream = typeof(ConfigurationAsJavaScript).GetTypeInfo().Assembly.GetManifestResourceStream(name);
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                var content = Encoding.UTF8.GetString(bytes);
                return content;
            }
            catch( Exception ex )
            {
                _logger.Error(ex,$"Couldn't get the resource '{name}'");
                throw ex;
            }
        }

        public string AsString
        {
            get
            {
                if (string.IsNullOrEmpty(_configurationAsString)) Initialize();

                return _configurationAsString;
            }
        }

        

        public void Initialize()
        {
            var proxies = Configure.Instance.Container.Get<GeneratedProxies>();

            var assetsManager = Configure.Instance.Container.Get<IAssetsManager>();
            assetsManager.Initialize();

            var builder = new StringBuilder();

            if (_webConfiguration.ScriptsToInclude.JQuery)
                builder.Append(GetResource("doLittle.Web.Scripts.jquery-2.1.3.min.js"));

            if (_webConfiguration.ScriptsToInclude.Knockout)
                builder.Append(GetResource("doLittle.Web.Scripts.knockout-3.2.0.debug.js"));

            if (_webConfiguration.ScriptsToInclude.SignalR)
                builder.Append(GetResource("doLittle.Web.Scripts.jquery.signalR-2.2.0.js"));

            if (_webConfiguration.ScriptsToInclude.JQueryHistory)
                builder.Append(GetResource("doLittle.Web.Scripts.jquery.history.js"));

            if (_webConfiguration.ScriptsToInclude.Require)
            {
                builder.Append(GetResource("doLittle.Web.Scripts.require.js"));
                builder.Append(GetResource("doLittle.Web.Scripts.order.js"));
                builder.Append(GetResource("doLittle.Web.Scripts.text.js"));
                builder.Append(GetResource("doLittle.Web.Scripts.domReady.js"));
                builder.Append(GetResource("doLittle.Web.Scripts.noext.js"));
            }

            if (_webConfiguration.ScriptsToInclude.doLittle)
                builder.Append(GetResource("doLittle.Web.Scripts.doLittle.debug.js"));

            builder.Append(proxies.All);

            var files = assetsManager.GetFilesForExtension("js");
            var serialized = JsonConvert.SerializeObject(files);
            builder.AppendFormat("doLittle.assetsManager.initializeFromAssets({0});", serialized);
            _configurationAsString = builder.ToString();
        }

    }
}
