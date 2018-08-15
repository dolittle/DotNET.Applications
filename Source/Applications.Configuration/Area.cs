using Dolittle.Concepts;

namespace Dolittle.Applications.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public class Area : ConceptAs<string>
    {
        /// <summary>
        /// 
        /// </summary>
        public static implicit operator Area(string area)
        {
            return new Area(){Value = area};
        }
    }
}