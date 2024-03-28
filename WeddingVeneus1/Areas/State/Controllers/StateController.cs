using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Data;
using WeddingVeneus1.Areas.State.Models;
using WeddingVeneus1.DAL;
using WeddingVeneus1.Services;

namespace WeddingVeneus1.Areas.Country.Controllers
{
    [Area("State")]
    [Route("State/{Controller}/{Action}")]
    public class StateController : Controller
    {
        #region GloblaStateDalObject
        State_DALBase dal = new State_DALBase();
        #endregion
  //      #region Index
  //      public IActionResult Index()
  //      {
  //          DataTable dt = dal.PR_State_SelectAll();
  //          MST_State_ViewModel mST_State_ViewModel = new MST_State_ViewModel();
  //          mST_State_ViewModel.StateDataTable = dt;
            
  //          return View("Index", mST_State_ViewModel);
  //      }
		//#endregion
		#region Search
		public IActionResult _Search(MST_State_ViewModel mST_State_ViewModel,string? submit,bool? ISConfirmed)
		{
           if(ISConfirmed == null)
            {
                ISConfirmed = true;
            }
           ViewBag.ISConfirmed = ISConfirmed;
            
            if(submit != null)
            {
                mST_State_ViewModel.SearchModel.SubmitType = submit;
            }
			DataTable dt = dal.PR_MST_State_SelectByPage(mST_State_ViewModel.SearchModel,ISConfirmed);
            mST_State_ViewModel.StateDataTable = dt;
            return View("Index", mST_State_ViewModel);

			
			
			
		}
		#endregion
		#region Delete
		public IActionResult Delete(int StateID)
        {
            
            dal.PR_State_DeleteByPK(StateID);
            TempData["Success"] = ("State Deleted Successfully");
            return RedirectToAction("Index");
        }
        #endregion
        #region UpdateStateStatus
        [HttpPost]
        public IActionResult ApproveStateStatus(int[] stateIds)
        {
            foreach (var stateId in stateIds)
            {
                dal.PR_MST_State_ApproveStateStatus(stateId);
                DataTable dt = dal.PR_MST_State_SelectUserIDByStateID(stateId);
                foreach(DataRow dr in dt.Rows) 
                {
                    StateModel stateModel = new StateModel();
                    stateModel.flag = true;
                    stateModel.Email = Convert.ToString(dr["Email"]);
                    stateModel.UserName = Convert.ToString(dr["UserName"]);
                    stateModel.StateName = Convert.ToString(dr["StateName"]);
                    SendEmail(stateModel);
                }

            }
            TempData["Success"] = "States Approved Successfully";
            var redirectUrl = Url.Action("Index", "Admin", new { area = "Login" });

            // Return success message and URL in JSON
            return Json(new { success = true, redirect = redirectUrl });
        }
        #endregion
        //#region UpdateStateStatus
        //[HttpPost]
        //public IActionResult RejectState(int[] stateIds)
        //{
        //    foreach (var stateId in stateIds)
        //    {
        //        dal.PR_MST_STATE_RejectState(stateId);
        //        DataTable dt = dal.PR_MST_State_SelectUserIDByStateID(stateId);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            StateModel stateModel = new StateModel();
        //            stateModel.flag = true;
        //            stateModel.Email = Convert.ToString(dr["Email"]);
        //            stateModel.UserName = Convert.ToString(dr["UserName"]);
        //            stateModel.StateName = Convert.ToString(dr["StateName"]);
        //            SendEmail(stateModel);
        //        }

        //    }
        //    TempData["Success"] = "States Approved Successfully";
        //    var redirectUrl = Url.Action("Index", "Admin", new { area = "Login" });

        //    // Return success message and URL in JSON
        //    return Json(new { success = true, redirect = redirectUrl });
        //}
        //#endregion
        #region Create
        public IActionResult Create(int? stateid)
        {
            if (stateid != null)
            {
                
                
                DataTable dt = dal.PR_State_SelectByPK(stateid);
                StateModel modelState = new StateModel();
                foreach (DataRow dr in dt.Rows)
                {

                    modelState.StateID = Convert.ToInt32(dr["StateID"]);
                    modelState.StateName = Convert.ToString(dr["StateName"]);
                    
                }
                return View("Create", modelState);




            }


            return View("Create");


        }
        #endregion
        #region Save
        [HttpPost]
        public IActionResult Save(StateModel stateModel)
        {
            Console.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
            {
                if (stateModel.StateID == null)
                {
                    stateModel.UserID = HttpContext.Session.GetInt32("UserID").Value;
                    if (HttpContext.Session.GetString("Role") == "VenueOwner")
                    {
                        dal.PR_State_Insert(stateModel);
                        SendEmail(stateModel);
                    }
                    else
                    {
                        dal.PR_MST_State_InsertForAdmin(stateModel);
                    }
                        
                    



                }
                else
                {
                    dal.PR_State_UpdateByPK(stateModel);

                }

                if (stateModel.StateID == null)
                {
                    TempData["Success"] = ("State Added Successfully");

                }
                else
                {
                    TempData["Success"] = ("State Updated Successfully");

                }




                return RedirectToAction("_Search");
            }
            else
            {
                return View("Create");
            }
        }
        #endregion
        public void SendEmail(StateModel stateModel)
        {
            
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("Khush Bhadrecha", "khushbhadrecha02@gmail.com"));
                if(stateModel.flag == true) 
                {
                    email.To.Add(new MailboxAddress("Jimmy Pot", stateModel.Email));
                    email.Subject = "Your request for adding new has been approved.";
                    email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                    {
                        Text = "Hey " + stateModel.UserName + "your request for adding state named " + stateModel.StateName + " had been approved by Mandap.com. " 
                    };
                }
                else
                {
                    email.To.Add(new MailboxAddress("Jimmy Pot", "Potj961@gmail.com"));
                    email.Subject = "Request for adding new state.";
                    email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                    {
                        Text = "The venueowner registered with the following mailID " + stateModel.Email + " has requested to add the following state " + stateModel.StateName + "."
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
        public IActionResult RejectState(int[] stateIds)
        {
            EntityService entityService = new EntityService();
            entityService.RejectEntities<State_DALBase>(stateIds);
            TempData["Success"] = "States Approved Successfully";
            var redirectUrl = Url.Action("Index", "Admin", new { area = "Login" });

            // Return success message and URL in JSON
            return Json(new { success = true, redirect = redirectUrl });
        }
    }
}
