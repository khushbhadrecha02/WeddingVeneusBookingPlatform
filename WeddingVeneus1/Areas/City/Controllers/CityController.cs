using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Data;
using WeddingVeneus1.Areas.Category.Models;
using WeddingVeneus1.Areas.City.Models;
using WeddingVeneus1.Areas.Photos.Models;
using WeddingVeneus1.Areas.State.Models;
using WeddingVeneus1.DAL;
using WeddingVeneus1.Services;

namespace WeddingVeneus1.Areas.City.Controllers
{
    [Area("City")]
    [Route("City/{Controller}/{Action}")]
    public class CityController : Controller
    {
        #region GloblaStateDalObject
        City_DALBase dal = new City_DALBase();
        #endregion
        #region Index
        public IActionResult Index()
        {


            DataTable dt = dal.PR_City_SelectAll();
            return View("Index", dt);
        }
        #endregion
        #region PopulateDropdownlist
        public void PopulateDropdownLists()
        {
            DataTable dt = dal.PR_State_SelectByComboBox();
            List<State_DropDown_Model> list = new List<State_DropDown_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                State_DropDown_Model vlst = new State_DropDown_Model();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = Convert.ToString(dr["StateName"]);
                list.Add(vlst);

            }
            ViewBag.StateList = list;
        }
        #endregion
        #region UpdateCityStatus
        public IActionResult ApproveCityStatus(int[] cityIDs)
        {

            foreach (var cityId in cityIDs)
            {
                dal.PR_MST_City_ApproveCityStatus(cityId);
                DataTable dt = dal.PR_MST_City_SelectUserIDByCityID(cityId);
                foreach (DataRow dr in dt.Rows)
                {
                    CityModel cityModel = new CityModel();
                    cityModel.flag = true;
                    cityModel.Email = Convert.ToString(dr["Email"]);
                    cityModel.UserName = Convert.ToString(dr["UserName"]);
                    cityModel.CityName = Convert.ToString(dr["CityName"]);
                    SendEmail(cityModel);
                }

            }
            TempData["Success"] = "States Approved Successfully";
            var redirectUrl = Url.Action("Index", "Admin", new { area = "Login" });

            // Return success message and URL in JSON
            return Json(new { success = true, redirect = redirectUrl });
        }
        #endregion
        #region Search
        public IActionResult _Search(MST_City_ViewModel mST_City_ViewModel, string? submit,bool? ISConfirmed)
        {
            if (ISConfirmed == null)
            {
                ISConfirmed = true;
            }
            ViewBag.ISConfirmed = ISConfirmed;
            PopulateDropdownLists();
            if (submit != null)
            {
                mST_City_ViewModel.SearchModel.SubmitType = submit;
            }
            DataTable dt = dal.PR_MST_City_SelectByPage(mST_City_ViewModel.SearchModel,ISConfirmed);
            mST_City_ViewModel.CityDataTable = dt;
            return View("Index", mST_City_ViewModel);




        }
        #endregion
        #region Delete
        public IActionResult Delete(int CityID)
        {

            dal.PR_City_DeleteByPK(CityID);
            TempData["Success"] = ("City Deleted Successfully");
            return RedirectToAction("Index");
        }
        #endregion
        #region Create
        public IActionResult Create(int? CityID)
        {
            #region ComboBox

            

            DataTable dt1 = dal.PR_State_SelectByComboBox();
            List<State_DropDown_Model> list = new List<State_DropDown_Model>();
            foreach (DataRow dr in dt1.Rows)
            {
                State_DropDown_Model vlst = new State_DropDown_Model();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = Convert.ToString(dr["StateName"]);
                list.Add(vlst);

            }
            ViewBag.StateList = list;

            #endregion

            #region SelectBYPK
            if (CityID != null)
            {
                

                DataTable dt = dal.PR_City_SelectByPK(CityID);

                CityModel modelCity = new CityModel();
                foreach (DataRow dr in dt.Rows)
                {

                    modelCity.StateID = Convert.ToInt32(dr["StateID"]);
                    modelCity.CityID = Convert.ToInt32(dr["CityID"]);
                    modelCity.CityName = Convert.ToString(dr["CityName"]);
                    
                }
                return View("Create", modelCity);
            }
            return View("Create");
            #endregion
        }
        #endregion
        
        #region Save
        [HttpPost]
        public IActionResult Save(CityModel cityModel)
        {


            
            
                if (cityModel.File != null)
                {
                    String FilePath = "wwwroot\\Upload";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileNameWithPath = Path.Combine(path, cityModel.File.FileName);
                    cityModel.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + cityModel.File.FileName;
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        cityModel.File.CopyTo(stream);
                    }
                }
                if (cityModel.CityID == null)
                {
                cityModel.UserID = HttpContext.Session.GetInt32("UserID").Value;
                if (HttpContext.Session.GetString("Role") == "VenueOwner")
                {
                    dal.PR_City_Insert(cityModel);
                    SendEmail(cityModel);

                }
                else
                {
                    dal.PR_MST_City_InsertForAdmin(cityModel);
                }
                    

                }
                else
                {
                    dal.PR_City_UpdateByPK(cityModel);

                }

                if (cityModel.StateID == null)
                {
                    TempData["Success"] = ("City Added Successfully");

                }
                else
                {
                    TempData["Success"] = ("City Updated Successfully");

                }




                return RedirectToAction("_Search");
            }


        #endregion
        #region SendEmail
        public void SendEmail(CityModel cityModel)
        {

            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("Khush Bhadrecha", "khushbhadrecha02@gmail.com"));
                if (cityModel.flag == true)
                {
                    email.To.Add(new MailboxAddress("Jimmy Pot", cityModel.Email));
                    email.Subject = "Your request for adding new city has been approved.";
                    email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                    {
                        Text = "Hey " + cityModel.UserName + "your request for adding city named " + cityModel.CityName + " had been approved by Mandap.com. "
                    };
                }
                else
                {
                    email.To.Add(new MailboxAddress("Jimmy Pot", "Potj961@gmail.com"));
                    email.Subject = "Request for adding new city.";
                    email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                    {
                        Text = "The venueowner registered with the following mailID " + cityModel.Email + " has requested to add the following state " + cityModel.CityName + "."
                    };
                }





                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("khushbhadrecha02@gmail.com", "evasrxlbzwmuogsr");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //return null;
            }
        }
        #endregion
        public IActionResult RejectCity(int[] cityIDs)
        {
            EntityService entityService = new EntityService();
            entityService.RejectEntities<City_DALBase>(cityIDs);
            TempData["Success"] = "Cities Rejected Successfully";
            var redirectUrl = Url.Action("Index", "Admin", new { area = "Login" });

            // Return success message and URL in JSON
            return Json(new { success = true, redirect = redirectUrl });
        }
    }
}
