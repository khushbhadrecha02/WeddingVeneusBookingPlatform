using Microsoft.AspNetCore.Mvc;
using System.Data;
using WeddingVeneus1.Areas.Login.Models;
using WeddingVeneus1.Areas.State.Models;
using WeddingVeneus1.DAL;

namespace WeddingVeneus1.Areas.Login.Controllers
{
    [Area("Login")]
    [Route("{Controller}/{Action}")]
    public class LoginController : Controller
    {
        #region GloblaStateDalObject
        Login_DALBase dal = new Login_DALBase();
        
        #endregion
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Register()
        {
            DataTable dt = dal.PR_MST_Role_SelectByComboBox();
            List<RoleModel> list = new List<RoleModel>();
            foreach (DataRow dr in dt.Rows)
            {
                RoleModel vlst = new RoleModel();
                vlst.RoleID = Convert.ToInt32(dr["RoleID"]);
                vlst.RoleName = Convert.ToString(dr["Role"]);
                list.Add(vlst);

            }
            ViewBag.RoleList = list;
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel modelLogin)
        {
            
            
            DataTable dt = dal.PR_Login_SelectByEmailAndpassword(modelLogin);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HttpContext.Session.SetString("UserSession", Convert.ToString(dr["UserName"]));
                    HttpContext.Session.SetString("Role", Convert.ToString(dr["Role"]));
                        HttpContext.Session.SetInt32("UserID",Convert.ToInt32(dr["UserID"]));
                    HttpContext.Session.SetString("Photo", Convert.ToString(dr["Photopath"]));


                }

                if (HttpContext.Session.GetString("Role") == "VenueOwner")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("VenueSearch", "VenueDetails", new { area = "VenueDetails" });
                }
            }
            else
            {
                ViewBag.Message = "Login Fail....";
                return RedirectToAction("Login");
            }


        }
        public IActionResult LogOut()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Register(LoginModel loginModel)
        {
            
            
            if (loginModel.File != null)
            {
                String FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, loginModel.File.FileName);
                loginModel.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + loginModel.File.FileName;
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    loginModel.File.CopyTo(stream);
                }
            }
            dal.PR_MST_User_Insert(loginModel);

            return RedirectToAction("Login");
            }
    }
}
