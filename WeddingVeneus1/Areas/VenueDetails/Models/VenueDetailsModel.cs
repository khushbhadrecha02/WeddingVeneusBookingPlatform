using Humanizer.Localisation.TimeToClockNotation;
using System.Data;
using System.Drawing;

namespace WeddingVeneus1.Areas.VenueDetails.Models
{
    public class VenueDetailsModel
    {
        public int? VenueID { get; set; }
        public string VenueName { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }
        public int CategoryID { get; set; }
        public int GuestCapacity { get; set; }
        public string Address { get; set; }
        public string ContactNO { get; set; }

        public int RentPerDay { get; set; }

        public int UserID { get; set; }
        public string AirConditioned { get; set; }
        public string Cuisines { get; set; }
        public string AdvancePayment { get; set; }
        public string PaymentAfterEvent { get; set; }
        public string DJPolicy { get; set; }
        public string GuestRooms { get; set; }
        public string ACRooms { get; set; }
        public string BikeParking { get; set; }
        public string CarParking { get; set; }
        public string ValetParking { get; set; }
        public string OutsideDecoration { get; set; }
        public string AlcoholPolicy { get; set; }
        public string VenueDescription { get; set; }
        public string CancellationPolicy { get; set; }
        public string? Email { get; set; }
    }
    public class VenueDetails_ViewModel
    {
        public DataTable Photos { get; set; }
        public DataTable VenueDetails { get; set; }
        public DataTable EventAreas { get; set; }
        public DataTable VenueDetailByCityID { get; set; }

        public DataTable City { get; set; }
        public DataTable Category { get; set; }
        public Venue_Search_Model venue_Search_Model { get; set; }
         public Venue_Based_On_City venue_Based_On_City { get; set; }
    }
    public class Venue_DropDown_Model
    {
        public int VenueID { get; set; }
        public string VenueName { get; set; }
    }
    public class Venue_Search_Model
    {
        public int? StateID { get; set; }
        public int? CityID { get; set; }
        public int? CategoryID { get; set; }
        public int? RentPerDay { get; set; }
        public int? GuestCapacity { get; set; }
        public int? UserID   { get; set; }
        public string? VenueName { get; set; }  
        public string? SubmitType { get; set; }
    }
    public class Venue_Based_On_City
    {
        
        public int CityID { get; set; }
        public int VenueID { get; set; }

    }
    public class VenueDropdownModel
    {

        public int VenueID { get; set; }
        public string VenueName { get; set; }

    }
}
