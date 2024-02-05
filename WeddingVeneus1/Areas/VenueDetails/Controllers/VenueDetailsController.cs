using Microsoft.AspNetCore.Mvc;
using System.Data;
using WeddingVeneus1.Areas.Category.Models;
using WeddingVeneus1.Areas.City.Models;
using WeddingVeneus1.Areas.State.Models;
using WeddingVeneus1.Areas.VenueDetails.Models;
using WeddingVeneus1.DAL;

namespace WeddingVeneus1.Areas.VenueDetails.Controllers
{
    [Area("VenueDetails")]
    //[Route("VenueDetails/{Controller}/{Action}")]
    public class VenueDetailsController : Controller
    {
        #region GloblaStateDalObject
        VenueDetails_DALBase dal = new VenueDetails_DALBase();
        City_DALBase dal1 = new City_DALBase();
        EventAreas_DALBase dal2 = new EventAreas_DALBase();
        Photos_DALBase dal3 = new Photos_DALBase();
        Category_DALBase dal4 = new Category_DALBase();

        #endregion
        #region Index
        public IActionResult Index()
        {

            int userid = HttpContext.Session.GetInt32("UserID").Value;
            DataTable dt = dal.PR_MST_VenueDetails_SelectByUserID(userid);
               
                return View("Index", dt);
        }
        #endregion
        #region Create
        public IActionResult Create(int? VenueID)
        {

            PopulateDropdownLists();

            #region SelectByPK
            if (VenueID != null)
            {
                
                DataTable dt2 = dal.PR_VenueDetails_SelectByPK(VenueID);
                VenueDetailsModel venueDetailsModel = new VenueDetailsModel();
                foreach (DataRow dr in dt2.Rows)
                {
                    
                    venueDetailsModel.VenueID = Convert.ToInt32(dr["VenueID"]);
                    venueDetailsModel.StateID = Convert.ToInt32(dr["StateID"]);
                    
                    venueDetailsModel.CityID = Convert.ToInt32(dr["CityID"]);
                    venueDetailsModel.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                    venueDetailsModel.GuestCapacity = Convert.ToInt32(dr["GuestCapacity"]);
                    venueDetailsModel.ContactNO = Convert.ToString(dr["ContactNO"]);
                    venueDetailsModel.RentPerDay = Convert.ToInt32(dr["RentPerDay"]);
                    venueDetailsModel.Address = Convert.ToString(dr["Address"]);
                    venueDetailsModel.VenueName = Convert.ToString(dr["VenueName"]);
                    
                    venueDetailsModel.AirConditioned = Convert.ToString(dr["AirConditioned"]);
                    venueDetailsModel.Cuisines = Convert.ToString(dr["Cuisines"]);
                    venueDetailsModel.AdvancePayment = Convert.ToString(dr["AdvancePayment"]);
                    venueDetailsModel.PaymentAfterEvent = Convert.ToString(dr["PaymentAfterEvent"]);
                    venueDetailsModel.DJPolicy = Convert.ToString(dr["DJPolicy"]);
                    venueDetailsModel.GuestRooms = Convert.ToString(dr["GuestRooms"]);
                    venueDetailsModel.ACRooms = Convert.ToString(dr["ACRooms"]);
                    venueDetailsModel.BikeParking = Convert.ToString(dr["BikeParking"]);
                    venueDetailsModel.CarParking = Convert.ToString(dr["CarParking"]);
                    venueDetailsModel.ValetParking = Convert.ToString(dr["ValetParking"]);
                    venueDetailsModel.OutsideDecoration = Convert.ToString(dr["OutsideDecoration"]);
                    venueDetailsModel.AlcoholPolicy = Convert.ToString(dr["AlcoholPolicy"]);
                    venueDetailsModel.VenueDescription = Convert.ToString(dr["VenueDescription"]);
                    venueDetailsModel.CancellationPolicy = Convert.ToString(dr["CancellationPolicy"]);


                }
                return View("Create", venueDetailsModel);
            }
            return View("Create");

            #endregion
        }
        #endregion
        #region ComboBox
        public void PopulateDropdownLists()
        {
            DataTable dt = dal1.PR_State_SelectByComboBox();
            List<State_DropDown_Model> list = new List<State_DropDown_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                State_DropDown_Model vlst = new State_DropDown_Model();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = Convert.ToString(dr["StateName"]);
                list.Add(vlst);

            }
            ViewBag.StateList = list;
            List<City_DropDownModel> list1 = new List<City_DropDownModel>();
            ViewBag.CityList = list1;
            DataTable dt1 = dal.PR_Category_SelectByComboBox();
            List<Category_DropDownModel> list2 = new List<Category_DropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                Category_DropDownModel vlst = new Category_DropDownModel();
                vlst.CategoryID = Convert.ToInt32(dr["CategoryID"]);
                vlst.CategoryName = Convert.ToString(dr["CategoryName"]);
                list2.Add(vlst);

            }
            ViewBag.CategoryList = list2;
        }
    #endregion
    #region delete
    public IActionResult delete(int VenueID)
        {

            dal.PR_VenueDetails_DeleteByPK(VenueID);
            TempData["success"] = ("Venue deleted successfully");
            return RedirectToAction("Index");
        }
        #endregion
        #region DropdownByState
        [HttpPost]
        public IActionResult DropdownByState(int StateID)
        {


            DataTable dt = dal.PR_City_SelectByComboBox(StateID);
            List<City_DropDownModel> list = new List<City_DropDownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                City_DropDownModel vlst = new City_DropDownModel();
                vlst.CityID = Convert.ToInt32(dr["CityID"]);
                vlst.CityName = Convert.ToString(dr["CityName"]);
                list.Add(vlst);
            }
            var vModel = list;
            return Json(vModel);

        }
        #endregion

        #region Save

        public IActionResult Save(VenueDetailsModel venueDetailsModel)
        {
            Console.WriteLine(venueDetailsModel.AirConditioned);
            int userid = HttpContext.Session.GetInt32("UserID").Value;
            venueDetailsModel.UserID = userid;

            

            if (venueDetailsModel.VenueID == null)
            {

                dal.PR_VenueDetails_Insert(venueDetailsModel);


            }
            else
            {
                dal.PR_VenueDetails_Update(venueDetailsModel);
            }

            if (venueDetailsModel.CategoryID == null)
            {
                TempData["Success"] = ("Venue Added Successfully");

            }
            else
            {
                TempData["Success"] = ("Venue Updated Successfully");

            }




            return RedirectToAction("Index", new { Userid = userid });
        }
        #endregion
        #region VenueDetail
        public IActionResult VenueDetail(int venueID,int CityID)
        {
            DataTable dt = dal.PR_VenueDetails_SelectByPK(venueID);
            DataTable dt1 = dal2.PR_EventArea_SelectBYVenueID(venueID);
            DataTable dt2 = dal3.PR_Photos_SelectByVenueID(venueID);
            DataTable dt3 = dal.PR_MST_VenueDetails_SelectByCityID(venueID, CityID);
            var viewModel = new VenueDetails_ViewModel()
            {
                VenueDetails = dt,
                EventAreas = dt1,
                Photos = dt2,
                VenueDetailByCityID = dt3
            };
            return View("VenueDetail", viewModel);

        }
        #endregion
        #region VenueDetail
        public IActionResult VenueSearch()
        {
            DataTable dt = dal1.PR_MST_City_CityWiseVenueCount();
            DataTable dt4 = dal4.PR_MST_VenueCategory_CategoryWiseVenueCount();

            var viewModel = new VenueDetails_ViewModel()
            {
                Category = dt4,
                City = dt,
                venue_Search_Model = new Venue_Search_Model()
            };
            PopulateDropdownLists();



            return View("VenueSearch",viewModel);

        }
        #endregion
        #region Search
        public IActionResult Search(VenueDetails_ViewModel venueDetails_View)
        {
            Console.WriteLine(venueDetails_View.venue_Search_Model.CategoryID);
            Console.WriteLine(venueDetails_View.venue_Search_Model.StateID);
            Console.WriteLine(venueDetails_View.venue_Search_Model.CityID);
            Console.WriteLine(venueDetails_View.venue_Search_Model.GuestCapacity);
            Console.WriteLine(venueDetails_View.venue_Search_Model.RentPerDay);
            DataTable dt = dal.PR_MST_VenueDetails_SelectByPage(venueDetails_View.venue_Search_Model);
            var viewModel = new VenueDetails_ViewModel()
            {

                VenueDetails = dt,
                venue_Search_Model = new Venue_Search_Model(),
            };
            PopulateDropdownLists();

            return View("SearchResult",viewModel );
        }
        #endregion



    }


}

