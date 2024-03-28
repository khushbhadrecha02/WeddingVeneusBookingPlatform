using System.Data;

namespace WeddingVeneus1.Areas.Category.Models
{
    public class CategoryModel
    {
        public int? CategoryID { get; set; }
        public string? CategoryName { get; set; }
        public IFormFile File { get; set; }
        public string? CategoryPhoto { get; set; }
        public string? Email { get; set; }
        public int? UserID { get; set; }
        public string? UserName { get; set; }
        public bool? flag { get; set; }
    }
    public class Category_DropDownModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class MST_Category_SearchModel
    {



        public string? CategoryName { get; set; }
        public string? SubmitType { get; set; }

    }
    public class MST_Category_ViewModel
    {
        public DataTable? CategoryDataTable { get; set; }
        public MST_Category_SearchModel SearchModel { get; set; }
    }
}
