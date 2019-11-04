/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
import { Plugin } from "@dolittle/tooling.common.plugins";
import { 
    CommandsProvider, NamespaceProvider, CommandGroupsProvider
} from "./internal";

let commandGroupsProvider = new CommandGroupsProvider([]);

let commandsProvider = new CommandsProvider([]);
let namespaceProvider = new NamespaceProvider([]);

export let plugin = new Plugin(commandsProvider, commandGroupsProvider, namespaceProvider);
