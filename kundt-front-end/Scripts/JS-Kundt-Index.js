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

//$("#date_bis").prop('readonly', true);
//$("#date_von").prop('readonly', true);
//$("#date_bis_mobile").prop('readonly', true);
//$("#date_von_mobile").prop('readonly', true);




$("#date_bis").datepicker({
    numberOfMonths: 1,
    dateFormat: 'dd.mm.yy',
    minDate: 'dateToday',
    currentText: 'heute', currentStatus: '',
    todayText: 'heute', todayStatus: '',
    clearText: '-', clearStatus: '',
    closeText: 'schließen', closeStatus: '',
    monthNames: ['Januar', 'Februar', 'März', 'April', 'Mai', 'Juni',
    'Juli', 'August', 'September', 'Oktober', 'November', 'Dezember'],
    monthNamesShort: ['Jan', 'Feb', 'Mär', 'Apr', 'Mai', 'Jun',
    'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dez'],
    dayNames: ['Sonntag', 'Montag', 'Dienstag', 'Mittwoch', 'Donnerstag', 'Freitag', 'Samstag'],
    dayNamesShort: ['So', 'Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa'],
    dayNamesMin: ['So', 'Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa'],
    minDate: new Date(),
    onSelect: function () {
        var dataObject_bis = $(this).datepicker('getDate');
    }
},$.datepicker.regional['de-AT']);


$("#date_von").datepicker({
    numberOfMonths: 1,
    dateFormat: 'dd.mm.yy',
    minDate: 'dateToday',
    currentText: 'heute', currentStatus: '',
    todayText: 'heute', todayStatus: '',
    clearText: '-', clearStatus: '',
    closeText: 'schließen', closeStatus: '',
    monthNames: ['Januar', 'Februar', 'März', 'April', 'Mai', 'Juni',
    'Juli', 'August', 'September', 'Oktober', 'November', 'Dezember'],
    monthNamesShort: ['Jan', 'Feb', 'Mär', 'Apr', 'Mai', 'Jun',
    'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dez'],
    dayNames: ['Sonntag', 'Montag', 'Dienstag', 'Mittwoch', 'Donnerstag', 'Freitag', 'Samstag'],
    dayNamesShort: ['So', 'Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa'],
    dayNamesMin: ['So', 'Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa'],
    onSelect: function () {
        $('#date_bis').datepicker('option', 'minDate', $("#date_von").datepicker("getDate"));
        var dataObject_von = $(this).datepicker('getDate');
    },

},$.datepicker.regional['de-AT']);


$("#date_bis_mobile").datepicker({
    dateFormat: "dd.mm.yy",
    currentText: 'heute', currentStatus: '',
    todayText: 'heute', todayStatus: '',
    clearText: '-', clearStatus: '',
    closeText: 'schließen', closeStatus: '',
    monthNames: ['Januar', 'Februar', 'März', 'April', 'Mai', 'Juni',
    'Juli', 'August', 'September', 'Oktober', 'November', 'Dezember'],
    monthNamesShort: ['Jan', 'Feb', 'Mär', 'Apr', 'Mai', 'Jun',
    'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dez'],
    dayNames: ['Sonntag', 'Montag', 'Dienstag', 'Mittwoch', 'Donnerstag', 'Freitag', 'Samstag'],
    dayNamesShort: ['So', 'Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa'],
    dayNamesMin: ['So', 'Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa'],
    minDate: new Date(),
    onSelect: function () {
        var dataObject_mobile_bis = $(this).datepicker('getDate');
    }
}, $.datepicker.regional['de-AT']);

$("#date_von_mobile").datepicker({
    dateFormat: "dd.mm.yy",
    minDate: 'dateToday',
    currentText: 'heute', currentStatus: '',
    todayText: 'heute', todayStatus: '',
    clearText: '-', clearStatus: '',
    closeText: 'schließen', closeStatus: '',
    monthNames: ['Januar', 'Februar', 'März', 'April', 'Mai', 'Juni',
    'Juli', 'August', 'September', 'Oktober', 'November', 'Dezember'],
    monthNamesShort: ['Jan', 'Feb', 'Mär', 'Apr', 'Mai', 'Jun',
    'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dez'],
    dayNames: ['Sonntag', 'Montag', 'Dienstag', 'Mittwoch', 'Donnerstag', 'Freitag', 'Samstag'],
    dayNamesShort: ['So', 'Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa'],
    dayNamesMin: ['So', 'Mo', 'Di', 'Mi', 'Do', 'Fr', 'Sa'],
    onSelect: function () {
        $('#date_bis_mobile').datepicker('option', 'minDate', $("#date_von_mobile").datepicker("getDate"));
        var dataObject_moblie_von = $(this).datepicker('getDate');
    }
}, $.datepicker.regional['de-AT']);


function getDateVon() {
    return $('#date_von').datepicker('getDate');
}

function getDateBis() {
    return $('#date_bis').datepicker('getDate');
}