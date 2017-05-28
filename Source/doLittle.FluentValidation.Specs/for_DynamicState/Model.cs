using System.Reflection;

namespace doLittle.FluentValidation.Specs.for_DynamicState
{
    public class Model
    {
        public static PropertyInfo TheStringProperty = typeof(Model).GetProperty("TheString");

        public bool TheStringGetCalled = false;
        public bool TheStringSetCalled = false;

        string _theString;
        public string TheString 
        {
            get
            {
                TheStringGetCalled = true;
                return _theString;
            }
            set
            {
                TheStringSetCalled = true;
                _theString = value;
            }
        }
    }
}
