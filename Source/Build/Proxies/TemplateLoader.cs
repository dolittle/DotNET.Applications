/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Reflection;
using Dolittle.Lifecycle;
using HandlebarsDotNet;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Loads the templates for proxies using the Handlebars .Net template engine template format
    /// </summary>
    [Singleton]
    public class TemplateLoader
    {
        static Assembly Assembly = typeof(Program).Assembly;
        const string ResourcePrefix = "Build.Proxies.templates.";
        const string CommandTemplateName = "command_template.js";
        const string QueryTemplateName = "query_template.js";
        const string ReadModelTemplateName = "readmodel_template.js";

        /// <summary>
        /// Handlebars template for command proxies
        /// </summary>
        public Func<object, string> CommandProxyTemplate {get; }
        /// <summary>
        /// Handlebars template for query proxies
        /// </summary>
        public Func<object, string> QueryProxyTemplate {get; }
        /// <summary>
        /// Handlebars template for read model proxies
        /// </summary>
        public Func<object, string> ReadModelProxyTemplate {get;}

        /// <summary>
        /// Instantiates the singleton instance of <see cref="TemplateLoader"/>. Templates are read and compiled at construction
        /// </summary>
        public TemplateLoader()
        {
            CommandProxyTemplate = Handlebars.Compile(ReadTemplate(CommandTemplateName));
            QueryProxyTemplate = Handlebars.Compile(ReadTemplate(QueryTemplateName));
            ReadModelProxyTemplate = Handlebars.Compile(ReadTemplate(ReadModelTemplateName));
        }

        string ReadTemplate(string templateName)
        {
            var stream = Assembly.GetManifestResourceStream(ResourcePrefix + templateName);
            return new StreamReader(stream).ReadToEnd();
        }
    }
}