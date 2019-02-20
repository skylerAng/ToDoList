$(document).ready(function () {

    // Request from url, put result back to table
    // Inserts table into the div
    $.ajax({
        url: '/ToDoes/BuildToDoTable',
        success: function (result) {
            $('#tableDiv').html(result);
        }
    })
})