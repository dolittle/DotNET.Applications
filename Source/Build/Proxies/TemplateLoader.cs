using System;
using System.IO;
using System.Reflection;
using Dolittle.Execution;
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
        const string ResourcePrefix = "Build.";
        const string CommandTemplateName = "command_template";

        /// <summary>
        /// Handlebars template for command proxies
        /// </summary>
        /// <value></value>
        public Func<object, string> CommandProxyTemplate {get; }

        /// <summary>
        /// Instantiates the singleton instance of <see cref="TemplateLoader"/>. Templates are read and compiled at construction
        /// </summary>
        public TemplateLoader()
        {
            var resourceNames = Assembly.GetManifestResourceNames();
            foreach (var name in resourceNames)
                Console.WriteLine(name);
                
            CommandProxyTemplate = Handlebars.Compile(ReadTemplate(CommandTemplateName));
        }

        string ReadTemplate(string templateName)
        {
            var stream = Assembly.GetManifestResourceStream(ResourcePrefix + templateName);
            return new StreamReader(stream).ReadToEnd();
        }
    }
}