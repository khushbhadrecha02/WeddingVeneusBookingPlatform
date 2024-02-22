using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
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
        #region Search
        public IActionResult _Search(MST_Category_ViewModel mST_Category_ViewModel, string? submit,bool? ISConfirmed)
        {
            if (ISConfirmed == null)
            {
                ISConfirmed = true;
            }
            ViewBag.ISConfirmed = ISConfirmed;
            if (submit != null)
            {
                mST_Category_ViewModel.SearchModel.SubmitType = submit;
            }
            DataTable dt = dal.PR_MST_State_SelectByPage(mST_Category_ViewModel.SearchModel,ISConfirmed);
            mST_Category_ViewModel.CategoryDataTable = dt;
            return View("Index", mST_Category_ViewModel);




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
        #region UpdateStateStatus
        public IActionResult ApproveCategoryStatus(int CategoryID)
        {

            dal.PR_MST_Category_ApproveCategoryStatus(CategoryID);
            //TempData["Success"] = ("State Deleted Successfully");
            return RedirectToAction("_Search");
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
                    categoryModel.UserID = HttpContext.Session.GetInt32("UserID").Value;
                    if (HttpContext.Session.GetString("Role") == "VenueOwner")
                    {
                        dal.PR_Category_Insert(categoryModel);
                        SendEmail(categoryModel);

                    }
                    else
                    {
                        dal.PR_MST_VenueCategory_InsertForAdmin(categoryModel);
                    }
                    

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




                return RedirectToAction("_Search");
            }
        #endregion
        #region SendEmail
        public void SendEmail(CategoryModel categoryModel)
        {
            Console.WriteLine(categoryModel.Email);
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("Khush Bhadrecha", "khushbhadrecha02@gmail.com"));
                email.To.Add(new MailboxAddress("Jimmy Pot", "Potj961@gmail.com"));

                email.Subject = "Request for adding new category";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = "The venueowner registered with the following mailID " + categoryModel.Email + " has requested to add the following category " + categoryModel.CategoryName + "."
                };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("khushbhadrecha02@gmail.com", "evasrxlbzwmuogsr");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }

                //return RedirectToAction("Index", "Home"); // or any other action you prefer
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //return null;
            }
        }
        #endregion
    }


}
        
 

