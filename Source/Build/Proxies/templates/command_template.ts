import { Command } from  '@dolittle/commands/Command';

export class {{CommandName}} extends Command
{
    {{#each Properties}}
    _{{PropertyName}}: {{PropertyDefaultValue}}
    {{/each}}

    constructor() {
        super();
        this.type = '{{ArtifactId}}';
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