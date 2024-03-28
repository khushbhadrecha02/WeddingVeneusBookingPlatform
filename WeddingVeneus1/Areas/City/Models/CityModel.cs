using System.Data;
using WeddingVeneus1.Areas.State.Models;

namespace WeddingVeneus1.Areas.City.Models
{
    public class CityModel
    {
        public int? CityID { get; set; }
        public string CityName { get; set; }
        public int? StateID{ get; set; }
        public string? PhotoPath { get; set; }
        public IFormFile File { get; set; }
        public int? UserID { get; set; }
        public string ? Email { get; set; }
        public bool? flag { get; set; }
        public string? UserName { get; set; }
    }
    public class City_DropDownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }

    }
    public class MST_City_SearchModel
    {



        public string? CityName { get; set; }
        public int? StateID { get; set; }
        public string? SubmitType { get; set; }

    }
    public class MST_City_ViewModel
    {
        public DataTable? CityDataTable { get; set; }
        public MST_City_SearchModel SearchModel { get; set; }
    }
}
