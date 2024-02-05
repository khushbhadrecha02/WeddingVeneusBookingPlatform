using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login", new { area = "Login" });

            }
        }

        

        
    }
}