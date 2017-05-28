doLittle.namespace("doLittle", {
    isArray : function(o) {
        return Object.prototype.toString.call(o) === '[object Array]';
    }
});