$.widget("datepicker", {
    _init: function () {
        var $el = this.element;
        $el.datepicker(this.options);

        if (this.options && this.options.trigger) {
            $(this.options.trigger).bind("click", function () {
                $el.datepicker("show");
            });
        }
    }
});

var $j = jQuery.noConflict();
$j("#date").datepicker({
    trigger: "#button"
});


$j("#date2").datepicker({
    trigger: "#button2"
});

