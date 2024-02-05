using System.ComponentModel.DataAnnotations;

namespace WeddingVeneus1.Areas.Booking.Models.Validation
{
    public class BookingStartDate : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime? message = Convert.ToDateTime(value);
                if (message >= DateTime.Now.Date)
                {
                    return ValidationResult.Success;
                }

            }
            return new ValidationResult("Booking Start Date should be greater than today's date");
        }
    }
    public class BookingEndDate : ValidationAttribute
    {


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var model = (CheckBooking)validationContext.ObjectInstance;
            DateTime? startDate = model.BookingStartDate;
            Console.WriteLine(startDate);
            if (value != null)
            {
                Console.WriteLine("Bookingenddate" + value);

                DateTime? message = Convert.ToDateTime(value);
                if (message >= startDate)
                {
                    return ValidationResult.Success;
                }

            }
            return new ValidationResult("Date should be greater than booking start date");
        }
    }
    public class AmountValidation : ValidationAttribute
    {


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var model = (BookingModel)validationContext.ObjectInstance;
            decimal? amount = model.Amount;
            decimal? MinPer = model.DefaultAdvancePayPer;
            decimal? per = (MinPer * amount) / 100;
            Console.WriteLine(MinPer);
            if (value != null)
            {
                Console.WriteLine("Bookingenddate" + value);

                decimal? message = Convert.ToDecimal(value);
                decimal? FinalAmount = (message / amount) * 100;
                if (FinalAmount >= MinPer)
                {
                    return ValidationResult.Success;
                }

            }
            return new ValidationResult("Minimum Advance Payment should be greater than or equal to minimum advance amount " +  MinPer  + " Percent of total Amount which is equal to " + per );
        }
    }
}
