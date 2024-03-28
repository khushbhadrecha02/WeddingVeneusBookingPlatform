$(document).ready(function () {
    $(".fa-heart").click(function () {
        var $this = $(this);
        var $parentDiv = $this.closest('div[data-venueid]');
        var venueId = $parentDiv.data('venueid');
        var isFavorited = $this.hasClass("fa-solid");

        // Determine the action and URL based on whether the icon is currently filled (favorited)
        var actionUrl = isFavorited ? $parentDiv.data('remove-url') : $parentDiv.data('add-url');

        // Send an AJAX request to add or remove the favorite
        $.ajax({
            url: actionUrl,
            type: 'POST',
            data: { venueId: venueId },
            success: function (response) {
                // Assuming your server responds with a JSON object including a success property
                if (response && response.success) {
                    $this.toggleClass("fa-solid fa-regular");
                } else {
                    // Handle failure (e.g., show an error message)
                    console.log('Failed to update the favorite status.');
                }
            },
            error: function () {
                // Handle AJAX error
                console.log('Error making the request.');
            }
        });
    });
    // Prevent default scrolling on card list
    $('#scroll').css('overflow-x', 'hidden');

    // Prevent touch/pointer scrolling on cards
    $('.scrolling-container').on('touchmove pointermove', function (e) {
        e.preventDefault();
    });

    // Button click events with dynamic overflow control
    $('#scrollLeft').on('click', function () {
        $('#scroll').css('overflow-x', 'auto'); // Enable scrolling temporarily
        $('#scroll').animate({ scrollLeft: '-=100' }, 'slow', function () {
            $('#scroll').css('overflow-x', 'hidden'); // Disable scrolling after animation
        });
    });

    $('#scrollRight').on('click', function () {
        $('#scroll').css('overflow-x', 'auto'); // Enable scrolling temporarily
        $('#scroll').animate({ scrollLeft: '+=100' }, 'slow', function () {
            $('#scroll').css('overflow-x', 'hidden'); // Disable scrolling after animation
        });
    });

    // Update button positions on scroll
    $('#scroll').on('scroll', function () {
        var scrollLeft = $(this).scrollLeft();
        $('#scrollLeft').css('left', scrollLeft);
        $('#scrollRight').css('right', -scrollLeft);
    });
    $(ApproveButton).click(function (e) {
        e.preventDefault(); // Prevents the default action

        // Collect all checked state IDs
        var stateIds = $(`input[name='${inputName}']:checked`).map(function () {
            return this.value;
        }).get();
        console.log("Checked state IDs: ", stateIds);

        // Ensure there's data to send
        if (stateIds.length > 0) {
            // Display a confirmation dialog
            var confirmAction = confirm(ConfirmationMessage);
            if (confirmAction) {
                // User clicked yes, proceed with the AJAX request
                $.ajax({
                    type: "POST",
                    url: url, // Update URL to your actual endpoint
                    data: { [ExpectedParameter]: stateIds },
                    traditional: true, // This ensures jQuery encodes the array in a way that ASP.NET Core understands
                    success: function (response) {
                        console.log(response.redirect);
                        window.location.href = response.redirect;
                    },
                    error: function (error) {
                        // Handle error, e.g., show an error message
                    }
                });
            } else {
                // User clicked no, optionally handle the cancellation
            }
        } else {
            // Optionally, alert the user that no states were selected
            alert("Please select at least one state to approve.");
        }
    });
    $(DeleteButton).click(function (e) {
        e.preventDefault(); // Prevents the default action

        // Collect all checked state IDs
        var stateIds = $(`input[name='${inputName}']:checked`).map(function () {
            return this.value;
        }).get();
        console.log("Checked state IDs: ", stateIds);

        // Ensure there's data to send
        if (stateIds.length > 0) {
            // Display a confirmation dialog
            var confirmAction = confirm(ConfirmationMessage);
            if (confirmAction) {
                // User clicked yes, proceed with the AJAX request
                $.ajax({
                    type: "POST",
                    url: url1, // Update URL to your actual endpoint
                    data: { [ExpectedParameter]: stateIds },
                    traditional: true, // This ensures jQuery encodes the array in a way that ASP.NET Core understands
                    success: function (response) {
                        console.log(response.redirect);
                        window.location.href = response.redirect;
                    },
                    error: function (error) {
                        // Handle error, e.g., show an error message
                    }
                });
            } else {
                // User clicked no, optionally handle the cancellation
            }
        } else {
            // Optionally, alert the user that no states were selected
            alert("Please select at least one state to approve.");
        }
    });

    $(selectAllCheckboxes).change(function (event) {
        event.stopPropagation();

        var state = this.checked; // True if checked, false if unchecked
        // Set all row checkboxes to the same state
        $('.row-checkbox').prop('checked', state);
    });

    $(selectAllCheckboxes).parent().on('click', function (event) {
        // This prevents the click on the checkbox's parent from propagating further up
        event.stopPropagation();
    });
});
function fnOnStateChange(generalSelectorForState, generalSelectorForCity) {
    var StateID = $(generalSelectorForState).val();
    // var StateID = $(".StateID").val();
    console.log("Stateid" + StateID);
    if (StateID !== "") {
        $(generalSelectorForCity).empty();
        $(generalSelectorForCity).append($("<option></option>").val("").html("Select City"));
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: dropdownByStateUrl + "?StateID=" + StateID,
            data: {},
            dataType: "json",
            success: function (Result) {
                $.each(Result, function (key, value) {
                    $(generalSelectorForCity).append($("<option></option>").val(value.cityID).html(value.cityName));
                });
            },
            error: function (r) {
                alert("Error while loading combobox");
            }
        });
    } else {
        $(generalSelectorForCity).empty();
        $(generalSelectorForCity).append($("<option></option>").val("").html("Select City"));
    }
    
}