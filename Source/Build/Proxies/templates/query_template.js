/*---------------------------------------------------------------------------------------------
 *  This file is an automatically generated Query Proxy
 *  
 *--------------------------------------------------------------------------------------------*/
import { Query } from  '@dolittle/queries';

export class {{QueryName}} extends Query
{
    constructor() {
        super();
        this.nameOfQuery = '{{QueryName}}';
        this.generatedFrom = '{{ClrType}}';

        {{#each Properties}}
        this.{{PropertyName}} = {{PropertyDefaultValue}};
        {{/each}}
    }
}