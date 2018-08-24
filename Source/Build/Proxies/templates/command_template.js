/*---------------------------------------------------------------------------------------------
 *  This file is an automatically generated Command Proxy
 *  
 *--------------------------------------------------------------------------------------------*/
import { Command } from  '@dolittle/commands';

export class {{CommandName}} extends Command
{
    constructor() {
        super();
        this.type = '{{ArtifactId}}';

        {{#each Properties}}
        this.{{PropertyName}} = {{PropertyDefaultValue}};
        {{/each}}
    }
}