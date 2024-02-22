using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using WeddingVeneus1.Areas.VenueDetails.Models;
using WeddingVeneus1.DAL;
using WeddingVeneus1.Models;

namespace WeddingVeneus1.Controllers
{
    [Area("Login")]
    [Route("Login/{Controller}/{Action}")]
    public class HomeController : Controller
    {
        



        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                VenueDetails_DALBase dal = new VenueDetails_DALBase();
                bool? ISConfirmed = true;
                int UserID1 = HttpContext.Session.GetInt32("UserID").Value;
                Venue_Search_Model venue_Search_Model = new Venue_Search_Model();
                DataTable dt = dal.PR_MST_VenueDetails_SelectByPage(venue_Search_Model, UserID1,ISConfirmed);
                return View("Index", dt);


                
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "Login" });

            }
        }

        

        
    }
}