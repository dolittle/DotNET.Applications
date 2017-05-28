var doLittle = doLittle || {};
(function(global, undefined) {
    doLittle.extend = function extend(destination, source) {
        return $.extend(destination, source);
    };
})(window);