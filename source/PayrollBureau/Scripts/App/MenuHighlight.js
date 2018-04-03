if ($) {
    $.expr[':'].containsexactly = function (obj, index, meta, stack) {
        return $(obj).text() === meta[3];
    };
}
(function (jquery) {
    var url = location.pathname;
    var anchor;
    var selectorText = jquery(".breadcrumb li:last").attr('data-menu');
    if (selectorText)
        anchor = jquery('.nav li[data-id="'+selectorText+'"] a');
    else
        anchor = jquery('.nav a[href=\'' + url + '\']');
    anchor.css({ "color": "#ffffff" });
    anchor.closest('li').css({ "background-color": "#e30000", "color": "#e30000" });
})(window.$)