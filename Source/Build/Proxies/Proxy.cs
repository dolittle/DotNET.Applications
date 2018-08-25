
namespace Dolittle.Build.Proxies
{
    /// <summary>
    /// Represents a class that contains all the information needed to create a proxy file
    /// </summary>
    public class Proxy
    {
        /// <summary>
        /// Gets and sets the EcmaScript (Javascript) content of the proxy
        /// </summary>
        /// <value></value>
        public string Content {get; set;}
        
        /// <summary>
        /// Gets and sets the full filepath of the proxy file
        /// </summary>
        /// <value></value>
        public string FullFilePath {get; set;}

    }
}