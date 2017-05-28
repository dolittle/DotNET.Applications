doLittle.namespace("doLittle.markup", {
    BindingContext: doLittle.Type.extend(function () {
        this.parent = null;
        this.current = null;

        this.changed = doLittle.Event.create();
    })
});