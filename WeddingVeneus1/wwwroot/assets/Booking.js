$(document).ready(function () {
    $('#numberOfDays').change(function () {
        var rentPerDay = $('#rentPerDay').val();
        console.log(rentPerDay);

        var numberOfDays = $('#numberOfDays').val();
        console.log(numberOfDays);

        $.ajax({
            url: CalculateAmount,
            type: 'GET',
            data: { NumberOfDays: numberOfDays, RentPerDay: rentPerDay },
            success: function (result) {
                if (result.success === true) {
                    $("#amount").val(result.data).trigger('change');

                } else {
                    console.error(result.error);
                }
            },
            error: function () {
                console.log("An error occurred while calculating booking amount.");
            }
        });

    });
    $('#advancePayment').change(function () {
        var advancePayment = $('#advancePayment').val();
        console.log(advancePaymentPer);


        var amount = $('#amount').val();
        console.log(amount);

        $.ajax({
            url: CalculateAdvancePaymentPer,
            type: 'GET',
            data: { AdvancePayment: advancePayment, Amount: amount },
            success: function (result) {
                if (result.success === true) {
                    $("#advancePaymentPer").val(result.advancePaymentPer);
                    console.log(result.advancePayment);
                    $("#paymentAfterEventPer").val(result.paymentAfterEventPer);
                    console.log(result.paymentAfterEventPer);
                    $("#paymentAfterEvent").val(result.paymentAfterEvent);
                    console.log(result.paymentAfterEvent);

                } else {
                    console.error(result.error);
                }
            },
            error: function () {
                console.log("An error occurred while calculating Advance amount.");
            }
        });
    });
    $('#amount').change(function () {
        var advancePaymentPer = $('#advancePaymentPer').val();
        console.log(advancePaymentPer);


        var amount = $('#amount').val();
        console.log(amount);

        $.ajax({
            url: CalculateAdvancePayment, 
            type: 'GET',
            data: { AdvancePaymentPer: advancePaymentPer, Amount: amount },
            success: function (result) {
                if (result.success === true) {
                    $("#advancePayment").val(result.advancePayment);
                    console.log(result.advancePayment);
                    $("#paymentAfterEvent").val(result.paymentAfterEvent);
                    console.log(result.paymentAfterEventPer);


                } else {
                    console.error(result.error);
                }
            },
            error: function () {
                console.log("An error occurred while calculating Advance amount.");
            }
        });
    });
    function calculateAndSetNumberOfDays() {
        // Get the date values as strings from the input fields
        var startDateStr = $('#inputStartDate').val();
        var endDateStr = $('#inputEndDate').val();

        console.log("Start Date: " + startDateStr);
        console.log("End Date: " + endDateStr);

        // Split the date strings into arrays [day, month, year]
        var startDateParts = startDateStr.split('-');
        var endDateParts = endDateStr.split('-');

        // Parse day, month, and year directly from the strings
        var startDay = parseInt(startDateParts[0]);
        var startMonth = parseInt(startDateParts[1]);
        var startYear = parseInt(startDateParts[2]);

        var endDay = parseInt(endDateParts[0]);
        var endMonth = parseInt(endDateParts[1]);
        var endYear = parseInt(endDateParts[2]);

        console.log("Start Date (parsed): " + startDay + "-" + startMonth + "-" + startYear);
        console.log("End Date (parsed): " + endDay + "-" + endMonth + "-" + endYear);

        // Check if all parts are valid integers
        if (!isNaN(startDay) && !isNaN(startMonth) && !isNaN(startYear) &&
            !isNaN(endDay) && !isNaN(endMonth) && !isNaN(endYear)) {

            // Calculate the actual number of days, considering both start and end dates as full days
            var actualNumberOfDays = Math.abs((endDay - startDay) + 1);

            console.log("Actual Days: " + actualNumberOfDays);

            // Update NumberOfDays if it's different



            $('#numberOfDays').val(actualNumberOfDays);

            // Trigger the change event to recalculate amounts
            $('#numberOfDays').trigger('change');

        } else {
            console.log("Invalid date format.");
        }
    }

    // Initial calculation on page load
    calculateAndSetNumberOfDays();
});