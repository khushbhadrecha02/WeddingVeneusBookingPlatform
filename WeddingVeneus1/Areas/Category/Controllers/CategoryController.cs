using Microsoft.AspNetCore.Mvc;
using System.Data;
using WeddingVeneus1.Areas.Category.Models;
using WeddingVeneus1.Areas.City.Models;
using WeddingVeneus1.Areas.State.Models;
using WeddingVeneus1.DAL;

namespace WeddingVeneus1.Areas.Category.Controllers
{
    [Area("Category")]
    [Route("Category/{Controller}/{Action}")]
    public class CategoryController : Controller
    {
        #region GloblaStateDalObject
        Category_DALBase dal = new Category_DALBase();
        #endregion
        #region Index
        public IActionResult Index()
        {


            DataTable dt = dal.PR_Category_SelectAll();
            return View("Index", dt);
        }
        #endregion
        #region Create
        public IActionResult Create(int? categoryid)
        {
            if (categoryid != null)
            {


                DataTable dt = dal.PR_Category_SelectByPK(categoryid);
                CategoryModel modelCategory = new CategoryModel();
                foreach (DataRow dr in dt.Rows)
                {

                    modelCategory.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    modelCategory.CategoryName = Convert.ToString(dr["CategoryName"]);
                    modelCategory.CategoryPhoto = Convert.ToString(dr["CategoryPhoto"]);

                }
                return View("Create", modelCategory);




            }


            return View("Create");


        }
        #endregion
        #region Delete
        public IActionResult Delete(int CategoryID)
        {

            dal.PR_Category_DeleteByPK(CategoryID);
            TempData["Success"] = ("Category Deleted Successfully");
            return RedirectToAction("Index");
        }
        #endregion
        #region Save

        public IActionResult Save(CategoryModel categoryModel)
            {


                if (categoryModel.File != null)
                {
                    String FilePath = "wwwroot\\Upload";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileNameWithPath = Path.Combine(path, categoryModel.File.FileName);
                    categoryModel.CategoryPhoto = "~" + FilePath.Replace("wwwroot\\", "/") + "/" +categoryModel.File.FileName;
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        categoryModel.File.CopyTo(stream);
                    }
                }
            
                if (categoryModel.CategoryID == null)
                {
                    dal.PR_Category_Insert(categoryModel);

                }
                else
                {
                    dal.PR_Category_UpdateByPK(categoryModel);
                }

                if (categoryModel.CategoryID == null)
                {
                    TempData["Success"] = ("Category Added Successfully");

                }
                else
                {
                    TempData["Success"] = ("Category Updated Successfully");

                }




                return RedirectToAction("Index");
            }
            
        }
            #endregion
     }
        
 

