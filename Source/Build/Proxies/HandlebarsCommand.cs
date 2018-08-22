using System.Collections.Generic;

namespace Dolittle.Build.Proxies
{
    public class HandlebarsCommand
    {
        public string CommandName {get; set;}
        public string ArtifactId {get; set;}
        public IList<ProxyProperty> Properties {get; set;} = new List<ProxyProperty>();
    }
}