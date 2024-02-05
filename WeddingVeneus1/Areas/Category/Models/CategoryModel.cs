namespace WeddingVeneus1.Areas.Category.Models
{
    public class CategoryModel
    {
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public IFormFile File { get; set; }
        public string CategoryPhoto { get; set; }
    }
    public class Category_DropDownModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
