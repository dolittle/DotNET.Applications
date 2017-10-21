using doLittle.Commands;

namespace doLittle.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class CommandWithConcept : ICommand
    {
        public ConceptAsString StringConcept { get; set; }
        public ConceptAsLong LongConcept { get; set; }
        public object NonConceptObject { get; set; }
    }
}
