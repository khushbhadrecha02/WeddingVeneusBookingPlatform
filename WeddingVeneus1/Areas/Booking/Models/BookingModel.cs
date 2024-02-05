using System.ComponentModel.DataAnnotations;
using System.Data;
using WeddingVeneus1.Areas.Booking.Models.Validation;

namespace WeddingVeneus1.Areas.Booking.Models
{
    public class BookingModel
    {
        public int? BookingID { get; set; }
        public int UserID { get; set; }
        public int? PaymentID { get; set; }
        
        public bool? ISBooked { get; set; }
        
        public DateTime? BookingStartDate { get; set; }
        public DateTime? BookingEndDate { get; set;}
        
        

        
        public int? NumOfDays { get; set; }

        public string? UserName { get; set; }
        public string? VenueName { get; set; }
        public int? VenueID { get; set; }
        public decimal Amount { get; set; }
        public decimal RentPerDay { get; set; }
        [Required]
        [AmountValidation]
        public decimal? AdvancePayment { get; set; }
        public decimal? PaymentAfterEvent { get; set; }

        public decimal? AdvancePaymentPer { get; set; }
        public decimal? DefaultAdvancePayPer { get; set; }
        public decimal PaymentAfterEventPer { get; set;}
        public string? Remarks { get; set; }
        public string? PaymentStatus { get; set; }
        public int? flag { get; set; }

        public string?   ContactNO { get; set; }
        public string? Email { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }


    }
    
    
    
    







}
