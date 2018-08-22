using System.IO;
using System.Reflection;
using Dolittle.Execution;

namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Loads and caches the templates for proxies using the Handlebars .Net template engine template format
    /// </summary>
    [Singleton]
    public class TemplateLoader
    {
        const string ResourcePrefix = "Build.";
        const string CommandTemplateName = "command_template";

        /// <summary>
        /// Template for command proxies
        /// </summary>
        /// <value></value>
        public string CommandProxyTemplate {get; }

        /// <summary>
        /// Instantiates the singleton instance of <see cref="TemplateLoader"/>. Templates are read at construction
        /// </summary>
        public TemplateLoader()
        {
            var asm = typeof(Program).Assembly;
            CommandProxyTemplate = ReadTemplate(asm, CommandTemplateName);
        }

        string ReadTemplate(Assembly asm, string templateName)
        {
            var stream = asm.GetManifestResourceStream(ResourcePrefix + templateName);
            return new StreamReader(stream).ReadToEnd();
        }
    }
}