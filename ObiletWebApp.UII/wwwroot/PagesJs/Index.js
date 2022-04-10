var date = new Date();
var Index = {
    init: function () {
        Index.DateChange();
        Index.LocationChanges();
        Index.SelectPickerChange();
        Index.OriginSearch();
        Index.DestinationSearch();
        Index.DatePicker();
    },
    DateChange:function() {
        $('#today').on('click', function () {

            $('.datepicker').datepicker('setDate', date);
        });
        $('#tomorrow').on('click', function () {
            $('.datepicker').datepicker('setDate', new Date(date.getFullYear(), date.getMonth(), (date.getDate() + 1)));
        });
    },
    LocationChanges:function() {
        $('#locationChange').on('click', function () {
            var originval = $("#origin").val();
            var origintext = $("#origin").find("option:selected").text();
            var destinationval = $("#destination").val();
            var destinationtext = $("#destination").find("option:selected").text();

            if ($("#origin option[value=" + destinationval + "]").length == 0) {
                $('#origin').append('<option value=' + destinationval + '>' + destinationtext + '</option>');
                $('#origin').selectpicker('refresh');
            }
            $('select[name=OriginId]').val(destinationval);
            $('#origin').selectpicker('refresh');


            if ($("#destination option[value=" + originval + "]").length == 0) {
                $('#destination').append('<option value=' + originval + '>' + origintext + '</option>');
                $('#destination').selectpicker('refresh');
            }
            $('select[name=DestinationId]').val(originval);
            $('#destination').selectpicker('refresh');
        });
    },
    SelectPickerChange: function() {

        $('.selectpicker').on('change', function () {

            if ($("#origin").val() == $("#destination").val()) {
                notif({
                    type: "error",
                    msg: "Varış ve Kalkış lokasyonları aynı olamaz",
                    width: 300,
                    height: 20,
                    position: "center"
                });
                $('#BiletiniBul').attr('disabled', 'disabled');
            }
            else {
                $('#BiletiniBul').removeAttr('disabled');
            }


        });
    },
    OriginSearch :function() {

        $('#origin').on('loaded.bs.select', function (e, clickedIndex, isSelected, previousValue) {
            $(".origin .bs-searchbox input").on('keyup', function () {


                var arama = $(this).val();

                console.log(arama);

                $.ajax({
                    url: 'Home/BusLocationSearch',
                    type: 'POST',
                    dataType: 'json',
                    data: { 'Search': arama },
                    success: function (data) {


                        $('#origin').html(data.object).selectpicker('refresh');
                    }
                });
            });
        });
    },
    DestinationSearch:function() {
        $('#destination').on('loaded.bs.select', function (e, clickedIndex, isSelected, previousValue) {
            $(".destination .bs-searchbox input").on('keyup', function () {
                var arama = $(this).val();
                $.ajax({
                    url: 'Home/BusLocationSearch',
                    type: 'POST',
                    dataType: 'json',
                    data: { 'Search': arama },
                    success: function (data) {

                        $('#destination').html(data.object).selectpicker('refresh');
                    }
                });
            });
        });
    },
    DatePicker:function() {

        $('.datepicker').datepicker({
            clearBtn: false,
            format: "dd.mm.yyyy",
            startDate: new Date()
        });
    }

}; $(document).ready(function () {
    Index.init();
});