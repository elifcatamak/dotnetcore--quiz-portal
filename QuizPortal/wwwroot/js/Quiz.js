$(document).ready(function () {
    $('#articleDropDown').change(function () {

        $('#articleDesc').empty();

        var selectedGuid = $(this).val();

        if (selectedGuid == "")
            return;

        var desc = $('#' + selectedGuid).text();

        console.log(desc);

        $('#articleDesc').append("<p class='border bg-light' style='padding:30px; border-radius:20px;'>" + desc + "</p>");
    });
});