using Microsoft.AspNetCore.Mvc;
using System.Data;
using WeddingVeneus1.Areas.Login.Models;
using WeddingVeneus1.DAL;

namespace WeddingVeneus1.Areas.Login.Controllers
{
    [Area("Login")]
    [Route("{Controller}/{Action}")]
    public class AdminController : Controller
    {
        #region GlobalObject
         Admin_DALBase dal = new Admin_DALBase();
        #endregion
        public IActionResult Index()
        {
            DataTable dt = dal.PR_MST_State_Count();
            DataTable dt1 = dal.PR_City_Count();
            DataTable dt2 = dal.PR_Category_Count();
            DataTable dt3 = dal.PR_MST_Venue_Count();
            DataTable dt4 = dal.PR_State_UnconfirmedStatecount();
            DataTable dt5 = dal.PR_City_UnconfirmedCitycount();
            DataTable dt6 = dal.PR_Category_UnconfirmedCategorycount();
            DataTable dt7 = dal.PR_Venue_UnconfirmedVenuecount();
            DataTable dt8 = dal.PR_MST_User_AdminRequestCount();
            AdminDashboardViewModel adminDashboardViewModel = new AdminDashboardViewModel()
            {
                StateCount = dt,
                CityCount = dt1,
                CategoryCount = dt2,
                VenueCount = dt3,
                UnconfirmedStateCount = dt4,
                UnconfirmedCityCount = dt5,
                UnconfirmedCategoryCount = dt6,
                UnconfirmedVenueCount = dt7,
                ApproveAdminAccessCount = dt8,
            };

            return View("Index", adminDashboardViewModel);
        }
    }
}
