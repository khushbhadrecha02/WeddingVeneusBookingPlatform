namespace WeddingVeneus1.Areas.EventAreas.Models
{
    public class EventAreasModel
    {
        
          public int? AreaID { get; set; }
          public int VenueID { get; set; }
          public List<string> AreaName { get; set; }
        public List<string> AreaT { get; set; }
        public List<int> SittingC { get; set; }
        public List<int> FloatingC { get; set; }
        public string AreaN { get; set; }
        public string AreaType { get; set; }
        public int SittingCapacity { get; set; }   
        public int FloatingCapacity { get; set; }

    }
}
