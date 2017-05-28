using doLittle.Mapping;

namespace doLittle.Specs.Mapping.for_Map
{
    public class MapWithOneOfTwoPropertiesMapped : Map<SourceWithTwoProperties, Target>
    {
        public MapWithOneOfTwoPropertiesMapped()
        {
            Property(m => m.FirstProperty).To(t => t.SomeOtherProperty);
        }
    }
}
