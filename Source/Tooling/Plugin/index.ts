/*---------------------------------------------------------------------------------------------
*  Copyright (c) Dolittle. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/
import { Plugin } from '@dolittle/tooling.common.plugins';
import {
    CommandsProvider, NamespaceProvider, CommandGroupsProvider
} from './internal';

const commandGroupsProvider = new CommandGroupsProvider([]);

const commandsProvider = new CommandsProvider([]);
const namespaceProvider = new NamespaceProvider([]);

export let plugin = new Plugin(commandsProvider, commandGroupsProvider, namespaceProvider);
