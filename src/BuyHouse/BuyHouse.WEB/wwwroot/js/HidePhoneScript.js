jQuery(document).ready(function ($) {
    $.fn.textToggle = function (cls, str) {
        return this.each(function (i) {
            $(this).click(function () {
                var c = 0, el = $(cls).eq(i), arr = [str, el.text()];
                return function () {
                    el.text(arr[c++ % arr.length]);
                }
            }());
        })
    };
    $(function () {
        $('.sh_nmr').textToggle(".sh_nmr", "").click();
        $('.sh_nmr').textToggle(".num_hide", "ХХХХХХХXX").click();
    });
});