namespace WeddingVeneus1.Areas.City.Models
{
    public class CityModel
    {
        public int? CityID { get; set; }
        public string CityName { get; set; }
        public int StateID{ get; set; }
        public string PhotoPath { get; set; }
        public IFormFile File { get; set; }

    }
    public class City_DropDownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }

    }
}
