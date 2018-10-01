/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Linq;
using Dolittle.Security;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Dolittle.Runtime.Commands.Security.for_CommandSecurityExtensions
{
    public class when_specifying_a_specific_command_type
    {
        static CommandSecurityTarget target;
        static TypeSecurable securable;

        Establish context = () =>
        {
            target = new CommandSecurityTarget();
        };

        Because of = () => target.InstanceOf<SimpleCommand>(t => securable = t);

        It should_add_a_type_securable = () => target.Securables.First().ShouldBeOfExactType<TypeSecurable>();
        It should_set_the_type_on_the_securable = () => ((TypeSecurable)target.Securables.First()).Type.ShouldEqual(typeof(SimpleCommand));
        It should_continue_the_fluent_interface_with_type_securable_builder = () => securable.ShouldNotBeNull();
    }
}
