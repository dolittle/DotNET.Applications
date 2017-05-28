doLittle.namespace("doLittle.read", {
    PagingInfo: doLittle.Type.extend(function (size, number) {
        this.size = size;
        this.number = number;
    })
});