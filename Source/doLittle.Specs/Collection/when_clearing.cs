using System.Collections.Generic;
using doLittle.Collections;
using Machine.Specifications;

namespace doLittle.Specs.Collection
{
    public class when_clearing : given.empty_collection
    {
        static IObservableCollection<object> collection_cleared;

        Establish context = () => 
        {
            collection.Add(new object());

            collection.Cleared += (c) => 
            {
                collection_cleared = c;
            };

        };

        Because of = () => collection.Clear();

        It should_pass_the_right_collection_to_the_cleared_event_handler = () => collection_cleared.ShouldEqual(collection);
        It should_be_empty = () => collection.ShouldBeEmpty();
    }
}
