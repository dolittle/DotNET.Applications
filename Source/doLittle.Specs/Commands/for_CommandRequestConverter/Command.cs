using Bifrost.Commands;

namespace Bifrost.Specs.Commands.for_CommandRequestConverter
{
    public class Command : ICommand
    {
        public string   StringProperty { get; set; }
        public int IntProperty { get; set; }
    }
}
