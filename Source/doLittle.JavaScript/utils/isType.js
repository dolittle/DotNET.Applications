doLittle.namespace("doLittle", {
    isType: function (o) {
        if (doLittle.isNullOrUndefined(o)) {
            return false;
        }
		return typeof o._typeId !== "undefined";
	}
});