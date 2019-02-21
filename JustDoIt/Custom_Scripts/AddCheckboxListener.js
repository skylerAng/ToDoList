
// Check the documents loaded
// Always start every jQuery with this, if document isn't added it isn't going to add the elements you want 
$(document).ready(function () {

    // Search for all the elements in ActiveCheck
    // Fires off the function if value of this entity changes
    $('.ActiveCheck').change(function () {

        // Get the reference to the actual element
        // (this) keyword is the thing that fires the function above
        var self = $(this);

        // Gets the attrbute id
        var id = self.attr('id');

        // Get vaue of property which is a checkbox
        var value = self.prop("checked");

        // Ajax request to update the database
        $.ajax({
            url: '/ToDoes/AjaxEdit',
            data: {
                id: id,
                value: value
            },
            type: 'POST',
            success: function (result) {
                $('#tableDiv').html(result);
            }
        })
    })


})