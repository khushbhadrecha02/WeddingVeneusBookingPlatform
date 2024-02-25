using Humanizer.Localisation.TimeToClockNotation;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MimeKit;
using NuGet.ProjectModel;
using System.Data;
using WeddingVeneus1.Areas.City.Models;
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
                    HttpContext.Session.SetString("ContactNo", Convert.ToString(dr["ContactNo"]));
                    HttpContext.Session.SetString("Email", Convert.ToString(dr["Email"]));

                    HttpContext.Session.SetInt32("UserID",Convert.ToInt32(dr["UserID"]));
                    HttpContext.Session.SetInt32("ISApprovedAdmin", Convert.ToInt32(dr["ISAdminApproved"]));
                    HttpContext.Session.SetString("Photo", Convert.ToString(dr["Photopath"]));


                }

                if (HttpContext.Session.GetString("Role") == "VenueOwner")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if(HttpContext.Session.GetString("Role") == "User")
                {
                    return RedirectToAction("VenueSearch", "VenueDetails", new { area = "VenueDetails" });
                }
                else
                {
                    if (HttpContext.Session.GetInt32("ISApprovedAdmin") == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return View("Login");
                    }
                        


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
        #region RegisterPost
        [HttpPost]
        public IActionResult Register(LoginModel loginModel)
        {
            Console.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
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
                if(loginModel.RoleID == 3)
                {
                    dal.PR_MST_User_AdminInsert(loginModel);
                    SendEmail(loginModel);
                }
                else
                {
                    dal.PR_MST_User_Insert(loginModel);
                }
                

                return RedirectToAction("Login");
            }
            else 
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
                return View("Register");
            }
        }
        #endregion
        #region SendEmail
        public void SendEmail(LoginModel loginModel)
        {
            Console.WriteLine(loginModel.Email);
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("Khush Bhadrecha", "khushbhadrecha02@gmail.com"));
                email.To.Add(new MailboxAddress("Jimmy Pot", "Potj961@gmail.com"));

                email.Subject = "Request for adding new state";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                {
                    Text = "The user registered with the following mailID " + loginModel.Email + " has requested for the admin rights. "
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
        public IActionResult Index()
        {
            DataTable dt = dal.PR_MST_User_SelectAdminRequestList();
            return View("Index", dt);
        }
        public IActionResult Profile()
        {

            LoginViewModel loginViewModel = new LoginViewModel()
            {
                loginModelForDisplay = new LoginModelForDisplay(),
                updatePassword = new UpdatePassword(),
                updatePhoto = new UpdatePhoto(),
                updateEmail = new UpdateEmail(),   
            };
            int UserID = HttpContext.Session.GetInt32("UserID").Value;
            DataTable dt = dal.PR_MST_User_SelectByPK(UserID);
            foreach (DataRow dr  in dt.Rows) 
            {
                loginViewModel.loginModelForDisplay.UserName = Convert.ToString(dr["UserName"]);
                loginViewModel.loginModelForDisplay.Email = Convert.ToString(dr["Email"]);
                loginViewModel.updateEmail.Email = Convert.ToString(dr["Email"]);
                loginViewModel.loginModelForDisplay.ContactNO = Convert.ToString(dr["ContactNo"]);
                loginViewModel.updatePhoto.PhotoPath = Convert.ToString(dr["PhotoPath"]);
                loginViewModel.loginModelForDisplay.RoleName = Convert.ToString(dr["Role"]);
                   
            }
                

            return View("Profile",loginViewModel);
        }
        public IActionResult ApproveAdminAccess(int UserID)
        {
            dal.PR_MST_User_ApproveAdminAccess(UserID);
            return View("Index");
        }
        #region UpdatePhoto
        public IActionResult UpdatePhoto(LoginViewModel loginViewModel) 
        {
            if (loginViewModel.updatePhoto.File != null)
            {
                String FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, loginViewModel.updatePhoto.File.FileName);
                loginViewModel.updatePhoto.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + loginViewModel.updatePhoto.File.FileName;
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    loginViewModel.updatePhoto.File.CopyTo(stream);
                }
                loginViewModel.updatePhoto.UserID = HttpContext.Session.GetInt32("UserID").Value;
                dal.PR_MSt_User_UpdateProfilePhoto(loginViewModel.updatePhoto);
                 
            }
            string resolvedPath = Url.Content(loginViewModel.updatePhoto.PhotoPath);
            HttpContext.Session.SetString("Photo", resolvedPath);
            return Json(new { success = true, data = resolvedPath });
        }
        #endregion

        #region DeletePhoto
        public IActionResult DeletePhoto()
        {
                int UserID = HttpContext.Session.GetInt32("UserID").Value;
            dal.PR_Mst_User_DeleteUserPhotoByUserID(UserID);
            string resolvedPath = Url.Content("~/assets/img/Profile.jpg");
            HttpContext.Session.SetString("Photo", resolvedPath);
            return Json(new { success = true, data = resolvedPath });
        }
        #endregion

        #region UpdateUserDetails
        public IActionResult UpdateUserDetails(LoginViewModel loginViewModel)
        {
            Console.WriteLine(loginViewModel.loginModelForDisplay.UserName);
            var keysToIgnore = new List<string> { "updatePhoto", "updatePassword", "updateEmail" };

            foreach (var keyToIgnore in keysToIgnore)
            {
                // Find and remove ModelState entries for the specified keys
                foreach (var key in ModelState.Keys.Where(k => k.StartsWith(keyToIgnore)).ToList())
                {
                    ModelState.Remove(key);
                }
            }
            loginViewModel.loginModelForDisplay.UserID = HttpContext.Session.GetInt32("UserID").Value;
            if (ModelState.IsValid)
            {


                dal.PR_MST_User_UpdateUserDetails(loginViewModel.loginModelForDisplay);
                return Json(new { success = true, data = new { userName = loginViewModel.loginModelForDisplay.UserName, contactNO = loginViewModel.loginModelForDisplay.ContactNO } });
                
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

                return Json(new { success = false, error = errors });
            }

        }
        #endregion
        #region ChangePassword
        public IActionResult ChangePassword(LoginViewModel loginViewModel)
        {
            
            var keysToIgnore = new List<string> { "updatePhoto", "loginModelForDisplay","updateEmail" };

            foreach (var keyToIgnore in keysToIgnore)
            {
                // Find and remove ModelState entries for the specified keys
                foreach (var key in ModelState.Keys.Where(k => k.StartsWith(keyToIgnore)).ToList())
                {
                    ModelState.Remove(key);
                }
            }
            loginViewModel.updatePassword.UserID = HttpContext.Session.GetInt32("UserID").Value;
            if (ModelState.IsValid)
            {


                dal.PR_MST_User_ChangePassword(loginViewModel.updatePassword);
                if (loginViewModel.updatePassword.Status == "Updated")
                {
                    return Json(new { success = true, });
                }
                else
                {
                    return Json(new { success = false, error = "Current Password is wrong" });
                }

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

                return Json(new { success = false, error = errors });
            }

        }
        #endregion
        #region ChangeEmail
        public IActionResult ChangeEmail(LoginViewModel loginViewModel)
        {

            var keysToIgnore = new List<string> { "updatePhoto", "loginModelForDisplay" , "updatePassword" };

            foreach (var keyToIgnore in keysToIgnore)
            {
                // Find and remove ModelState entries for the specified keys
                foreach (var key in ModelState.Keys.Where(k => k.StartsWith(keyToIgnore)).ToList())
                {
                    ModelState.Remove(key);
                }
            }
            loginViewModel.updateEmail.UserID = HttpContext.Session.GetInt32("UserID").Value;

            if (ModelState.IsValid)
            {


                dal.PR_MST_User_UpdateEmail(loginViewModel.updateEmail);
                
                    return Json(new { success = true, data = new { email = loginViewModel.updateEmail.UpdatedEmail, } });
                
                

            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

                return Json(new { success = false, error = errors });
            }

        }
        #endregion

    }
}
