// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Reflection;
using Dolittle.Lifecycle;
using HandlebarsDotNet;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Loads the templates for proxies using the Handlebars .Net template engine template format.
    /// </summary>
    [Singleton]
    public class TemplateLoader
    {
        const string ResourcePrefix = "Build.Proxies.templates.";
        const string CommandTemplateName = "command_template.js";
        const string QueryTemplateName = "query_template.js";
        const string ReadModelTemplateName = "readmodel_template.js";
        static readonly Assembly Assembly = typeof(TemplateLoader).Assembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateLoader"/> class.
        /// </summary>
        public TemplateLoader()
        {
            CommandProxyTemplate = Handlebars.Compile(ReadTemplate(CommandTemplateName));
            QueryProxyTemplate = Handlebars.Compile(ReadTemplate(QueryTemplateName));
            ReadModelProxyTemplate = Handlebars.Compile(ReadTemplate(ReadModelTemplateName));
        }

        /// <summary>
        /// Gets handlebars template for command proxies.
        /// </summary>
        public Func<object, string> CommandProxyTemplate { get; }

        /// <summary>
        /// Gets handlebars template for query proxies.
        /// </summary>
        public Func<object, string> QueryProxyTemplate { get; }

        /// <summary>
        /// Gets handlebars template for read model proxies.
        /// </summary>
        public Func<object, string> ReadModelProxyTemplate { get; }

        string ReadTemplate(string templateName)
        {
            using (var stream = Assembly.GetManifestResourceStream(ResourcePrefix + templateName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}