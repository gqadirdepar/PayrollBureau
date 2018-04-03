
(function (jQuery) {

    jQuery(function () {
        jQuery(".navbar a").each(function () {
            var link = jQuery(this),
                linkHref = link.attr("href").toLowerCase();

            if (window.location.pathname.toLowerCase() === linkHref) {
                link.parent("li").addClass("active");
            }
        });
    });


})(window.jQuery);