doLittle.namespace("doLittle.interaction", {
    VisualStateManagerElementVisitor: doLittle.markup.ElementVisitor.extend(function () {
        var visualStateActionTypes = doLittle.interaction.VisualStateAction.getExtenders();

        

        function parseActions(namingRoot, stateElement, state) {
            function parseAction(type) {
                if (type._name.toLowerCase() === child.localName) {
                    var action = type.create();

                    for (var attributeIndex = 0; attributeIndex < child.attributes.length; attributeIndex++) {
                        var name = child.attributes[attributeIndex].localName;
                        var value = child.attributes[attributeIndex].value;
                        if (action.hasOwnProperty(name)) {
                            action[name] = value;
                        }
                    }
                    action.initialize(namingRoot);
                    state.addAction(action);
                }
            }

            if (stateElement.hasChildNodes()) {
                var child = stateElement.firstChild;
                while (child) {
                    visualStateActionTypes.forEach(parseAction);
                    child = child.nextSibling;
                }
            }
        }

        function parseStates(namingRoot, groupElement, group) {
            if( groupElement.hasChildNodes() ) {
                var child = groupElement.firstChild;
                while( child ) {
                    if( child.localName === "visualstate" ) {
                        var state = doLittle.interaction.VisualState.create();
                        state.name = child.getAttribute("name");
                        group.addState(state);
                        parseActions(namingRoot, child, state);
                    }
                    child = child.nextSibling;
                }
            }
        }


        this.visit = function (element, actions) {
            if (element.localName === "visualstatemanager") {
                var visualStateManager = doLittle.interaction.VisualStateManager.create();
                var namingRoot = element.parentElement.namingRoot;
                element.parentElement.visualStateManager = visualStateManager;

                if (element.hasChildNodes()) {
                    var child = element.firstChild;
                    while (child) {
                        if (child.localName === "visualstategroup") {
                            var group = doLittle.interaction.VisualStateGroup.create();
                            visualStateManager.addGroup(group);

                            var duration = child.getAttribute("duration");
                            if (!doLittle.isNullOrUndefined(duration)) {
                                duration = parseFloat(duration);
                                if (!isNaN(duration)) {
                                    duration = duration * 1000;
                                    var timespan = doLittle.TimeSpan.fromMilliseconds(duration);
                                    group.defaultDuration = timespan;
                                }
                            }

                            parseStates(namingRoot, child, group);
                        }
                        child = child.nextSibling;
                    }
                }
            }
        };

    })
});