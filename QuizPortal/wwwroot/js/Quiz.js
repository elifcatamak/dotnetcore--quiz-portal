$(document).ready(function () {
    $('#articleDropDown').change(function () {

        $('#articleDesc').empty();

        var selectedGuid = $(this).val();

        if (selectedGuid == "")
            return;

        var desc = $('#' + selectedGuid).text();

        console.log(desc);

        $('#articleDesc').append("<p class='border' style='padding:30px'>" + desc + "</p>");
    });
});