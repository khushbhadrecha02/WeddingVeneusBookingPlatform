using System.ComponentModel.DataAnnotations;
using System.Data;
using WeddingVeneus1.Areas.Booking.Models.Validation;

namespace WeddingVeneus1.Areas.Booking.Models
{
    public class CheckBooking
    {
        [Required]
        [BookingStartDate]
        public DateTime? BookingStartDate { get; set; }

        [Required]
        [BookingEndDate]
        public DateTime? BookingEndDate { get; set; }

        public int? VenueID { get; set; }
        public int? UserID { get; set; }
        public string? VenueName { get; set; }
    }
    public class Booking_ViewModel
    {

        public CheckBooking bookingModel { get; set; }
        public DataTable? CheckStatus { get; set; }


    }
}
