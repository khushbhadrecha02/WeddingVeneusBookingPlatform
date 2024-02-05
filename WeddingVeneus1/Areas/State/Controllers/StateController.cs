using Microsoft.AspNetCore.Mvc;
using System.Data;
using WeddingVeneus1.Areas.State.Models;
using WeddingVeneus1.DAL;

namespace WeddingVeneus1.Areas.Country.Controllers
{
    [Area("State")]
    [Route("State/{Controller}/{Action}")]
    public class StateController : Controller
    {
        #region GloblaStateDalObject
        State_DALBase dal = new State_DALBase();
        #endregion
        #region Index
        public IActionResult Index()
        {
            
            
            DataTable dt = dal.PR_State_SelectAll();
            return View("Index", dt);
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

            if (ModelState.IsValid)
            {
                if (stateModel.StateID == null)
                {
                    dal.PR_State_Insert(stateModel);

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




                return RedirectToAction("Index");
            }
            else
            {
                return View("Create");
            }
        }
        #endregion
    }
}
