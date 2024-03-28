$(document).ready(function () {
    $("#openFileBtn").click(function (event) {
        event.preventDefault(); // Prevent the default form submission
        console.log("Button clicked, default action prevented.");
        $("#hiddenFileInput").click(); // Trigger click on hidden file input


    });
    $("#hiddenFileInput").change(function () {
        // Create FormData object from the form
        var formData = new FormData($('#updatephotoform')[0]);

        $.ajax({
            url: UpdatePhoto, // Make sure to replace YourControllerName with the actual controller name
            type: 'POST',
            data: formData,
            processData: false, // Tell jQuery not to process the data
            contentType: false, // Tell jQuery not to set contentType
            success: function (response) {
                if (response.success) {
                    console.log("Photo updated successfully: ", response.data);
                    // Here you can update the UI as needed, for example, show the uploaded photo
                    $('.ProfilePhoto').attr('src', response.data);

                } else {
                    // Handle failure
                    console.error("An error occurred.");
                }
            },
            error: function (xhr, status, error) {
                // Handle AJAX error
                console.error("AJAX error: ", status, error);
            }
        });
    });
    $('#deleteUserPhotoBtn').click(function () {
        // Show confirmation dialog
        if (confirm('Are you sure you want to delete your photo?')) {


            // User clicked 'Yes', make AJAX call to your action method
            $.ajax({
                url: DeletePhoto, // Update YourControllerName accordingly
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.success) {
                        // Handle success, maybe update UI to reflect the photo is deleted

                        alert('Photo deleted successfully.');
                        $('.ProfilePhoto').attr('src', response.data);
                    } else {
                        // Handle failure
                        alert('Failed to delete photo.');
                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    alert('An error occurred.');
                }
            });
        } else {
            // User clicked 'No', do nothing
        }
    });
    $('#UpdateUserDetail').click(function () {
        // Show confirmation dialog
        if (confirm('Are you sure you want to update your details?')) {
            var formData = new FormData($('#updateUserform')[0]);
            for (var pair of formData.entries()) {
                console.log(pair[0] + ', ' + pair[1]);
            }

            // User clicked 'Yes', make AJAX call to your action method
            $.ajax({
                url: UpdateUserDetails, // Update YourControllerName accordingly
                type: 'POST',
                data: formData,
                processData: false, // Tell jQuery not to process the data
                contentType: false,
                success: function (response) {
                    if (response.success) {
                        // Handle success, maybe update UI to reflect the photo is deleted
                        console.log(response.data.userName)
                        console.log(response.data.contactNO)
                        console.log($('.fullName')); // Check if it selects the expected input
                        console.log($('.ContactNO')); // Check if it selects the expected input

                        alert('Details updated successfully.');
                        // $('#fullName').val(response.data.userName);
                        $('.fullName').text(response.data.userName);


                        $('.ContactNO').text(response.data.contactNO);


                    } else {
                        // Handle failure
                        var errorMessages = response.error.join('\n');
                        alert('Failed to update details:\n' + errorMessages);
                        // Optionally, display the errors in a specific part of your page
                        // Clear previous errors
                        $('#validationErrorContainer').html('');

                        // Create an unordered list to hold the errors
                        var $errorList = $('<ul>').css('color', 'red');

                        // Iterate over the errors and add each to the list
                        response.error.forEach(function (error) {
                            var $li = $('<li>').text(error);
                            $errorList.append($li);
                        });

                        // Append the list to the validation error container
                        $('#validationErrorContainer').append($errorList);

                    }
                },
                error: function (xhr, status, error) {
                    // Handle AJAX error
                    alert('An error occurred.');
                }
            });
        } else {
            // User clicked 'No', do nothing
        }
    });
    $('#ChangePassword').click(function () {
        console.log("click event happened");
        var formData = new FormData($('#ChangePasswordForm')[0]); // Ensure the form's ID is correct
        for (var pair of formData.entries()) {
            console.log(pair[0] + ', ' + pair[1]);
        }

        // Make AJAX call to your action method
        $.ajax({
            url: ChangePassword, // Make sure this is dynamically replaced with the correct URL if using Razor syntax or set the correct static URL
            type: 'POST',
            data: formData,
            processData: false, // Tell jQuery not to process the data
            contentType: false, // Tell jQuery not to set contentType
            success: function (response) {
                if (response.success) {
                    alert('Password changed successfully.');
                } else {
                    $('#validationError').html(''); // Clear previous errors
                    var $errorList = $('<ul>').css('color', 'red'); // Create an unordered list to hold the errors

                    if (Array.isArray(response.error)) {
                        response.error.forEach(function (error) {
                            var $li = $('<li>').text(error);
                            $errorList.append($li);
                        });
                    } else {
                        var $li = $('<li>').text(response.error);
                        $errorList.append($li);
                    }

                    $('#validationError').append($errorList);
                }
            },
            error: function (xhr, status, error) {
                alert('An error occurred.');
            }
        });
    });
    $('#ChangeEmail').click(function () {
        console.log("click event happened");
        var formData = new FormData($('#ChangeEmailForm')[0]); // Ensure the form's ID is correct
        for (var pair of formData.entries()) {
            console.log(pair[0] + ', ' + pair[1]);
        }

        // Make AJAX call to your action method
        $.ajax({
            url: ChangeEmail, // Make sure this is dynamically replaced with the correct URL if using Razor syntax or set the correct static URL
            type: 'POST',
            data: formData,
            processData: false, // Tell jQuery not to process the data
            contentType: false, // Tell jQuery not to set contentType
            success: function (response) {
                if (response.success) {
                    alert('Email updated successfully.');
                    $('#oldEmail').val(response.data.email);
                    $('#Email1').text(response.data.email);
                    $('#newEmail').val('');


                } else {
                    $('#validation').html(''); // Clear previous errors
                    var $errorList = $('<ul>').css('color', 'red'); // Create an unordered list to hold the errors

                    if (Array.isArray(response.error)) {
                        response.error.forEach(function (error) {
                            var $li = $('<li>').text(error);
                            $errorList.append($li);
                        });
                    } else {
                        var $li = $('<li>').text(response.error);
                        $errorList.append($li);
                    }

                    $('#validation').append($errorList);
                }
            },
            error: function (xhr, status, error) {
                alert('An error occurred.');
            }
        });
    });
})