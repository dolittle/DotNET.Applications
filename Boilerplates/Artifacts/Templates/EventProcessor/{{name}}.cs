using Dolittle.Events.Processing;
{{#if readModels}}
using Dolittle.ReadModels;
{{/if}}
{{{addUniqueCSharpNamespace events}}}

namespace {{namespace}}
{
    public class {{name}} : ICanProcessEvents
    {
        {{#if readModels}}
        {{#each readModels}}
        readonly IReadModelRepositoryFor<{{this.value}}> _repositoryFor{{this.value}};
        {{/each}}

        public {{name}}(
            {{#each readModels}}{{#if @first}}IReadModelRepositoryFor<{{this.value}}> repositoryFor{{this.value}}{{#unless @last}},{{/unless}}{{/if}}            {{#if @last}}{{#unless @first}}IReadModelRepositoryFor<{{this.value}}> repositoryFor{{this.value}}{{/unless}}{{/if}}
            {{/each}}
        )
        {
            {{#each readModels}}
            _repositoryFor{{this.value}} = repositoryFor{{this.value}};
            {{/each}}
        }
        {{/if}}
        
        {{#each events}}
        [EventProcessor("{{createGuid}}")]
        public void Process({{this.value}} @event)
        { 
            
        }
        
        {{/each}}
    }
}
