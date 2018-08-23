import { Command } from  '@dolittle/commands/Command';

export class {{CommandName}} extends Command
{
    constructor() {
        super();
        this.type = '{{ArtifactId}}';

        {{#each Properties}}
        this._{{PropertyName}} = {{PropertyDefaultValue}}
        {{/each}}
    }
    
    {{#each Properties}}
    get {{PropertyName}}() {
        return this._{{PropertyName}}
    }
    
    set {{PropertyName}}(value) {
        this._{{PropertyName}} = value;
    }

    {{/each}}
}