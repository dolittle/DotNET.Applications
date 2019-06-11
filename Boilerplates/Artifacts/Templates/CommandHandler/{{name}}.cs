using Dolittle.Commands.Handling;
{{#if aggregateRoots}}
using Dolittle.Domain;
{{/if}}

namespace {{namespace}}
{
    public class {{name}} : ICanHandleCommands
    {
        {{#if aggregateRoots}}
        {{#each aggregateRoots}}
        readonly IAggregateRootRepositoryFor<{{this.value}}>  _aggregateRootRepoFor{{this.value}};
        {{/each}}

        public {{name}}(
            {{#each aggregateRoots}}{{#if @first}}IAggregateRootRepositoryFor<{{this.value}}>  aggregateRootRepoFor{{this.value}}{{#unless @last}},{{/unless}}{{/if}}            {{#if @last}}{{#unless @first}}IAggregateRootRepositoryFor<{{this.value}}>  aggregateRootRepoFor{{this.value}}{{/unless}}{{/if}}
            {{/each}}
        )
        {
            {{#each aggregateRoots}}
             _aggregateRootRepoFor{{this.value}} =  aggregateRootRepoFor{{this.value}};
            {{/each}}
        }
        {{/if}}

        {{#each commands}}
        public void Handle({{this.value}} cmd)
        {

        }
        
        {{/each}}
    }
}
