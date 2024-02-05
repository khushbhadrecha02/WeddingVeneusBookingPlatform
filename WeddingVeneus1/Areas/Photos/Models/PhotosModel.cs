namespace WeddingVeneus1.Areas.Photos.Models
{
    public class PhotosModel
    {
        public int? PhotoID { get; set; }
        public string PhotoPath { get; set; }


        public int VenueID { get; set; }
        public IFormFile File { get; set; }
        public List<IFormFile> FilePath { get; set; }
    }
}
