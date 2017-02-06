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


// Hier evtl Variablen anlegen für das Datum von/bis zur weitergabe für Step 1
var $j = jQuery.noConflict();
$j("#date_von").datepicker({    
});


$j("#date_bis").datepicker({    
});




