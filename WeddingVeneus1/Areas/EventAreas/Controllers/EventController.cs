using Microsoft.AspNetCore.Mvc;
using System.Data;
using WeddingVeneus1.Areas.Category.Models;
using WeddingVeneus1.Areas.City.Models;
using WeddingVeneus1.Areas.EventAreas.Models;
using WeddingVeneus1.Areas.State.Models;
using WeddingVeneus1.Areas.VenueDetails.Models;
using WeddingVeneus1.DAL;

namespace WeddingVeneus1.Areas.Category.Controllers
{
    [Area("EventAreas")]
    [Route("EventAreas/{Controller}/{Action}")]
    public class EventController : Controller
    {
        #region GloblaStateDalObject
        EventAreas_DALBase dal = new EventAreas_DALBase();
        VenueDetails_DALBase dal1 = new VenueDetails_DALBase();


        #endregion
        #region Index
        public IActionResult Index(int VenueID)
        {

            
            DataTable dt = dal.PR_EventArea_SelectBYVenueID(VenueID);
            return View("Index", dt);
        }
        #endregion
        #region Create
        public IActionResult Create()
        {
            int userid = HttpContext.Session.GetInt32("UserID").Value;

            DataTable dt = dal1.PR_MST_VenueDetails_SelectByComboBox(userid);
            List<Venue_DropDown_Model> list = new List<Venue_DropDown_Model>();
            foreach (DataRow dr in dt.Rows)
            {
                Venue_DropDown_Model vlst = new Venue_DropDown_Model();
                vlst.VenueID = Convert.ToInt32(dr["VenueID"]);
                vlst.VenueName = Convert.ToString(dr["VenueName"]);
                list.Add(vlst);

            }
            ViewBag.VenueList = list;

            return View("Create");
        }
        #endregion
        #region Update
        public IActionResult Update(int Areaid)
        {


            int userid = HttpContext.Session.GetInt32("UserID").Value;

            DataTable dt1 = dal1.PR_MST_VenueDetails_SelectByComboBox(userid);
            List<Venue_DropDown_Model> list = new List<Venue_DropDown_Model>();
            foreach (DataRow dr in dt1.Rows)
            {
                Venue_DropDown_Model vlst = new Venue_DropDown_Model();
                vlst.VenueID = Convert.ToInt32(dr["VenueID"]);
                vlst.VenueName = Convert.ToString(dr["VenueName"]);
                list.Add(vlst);

            }
            ViewBag.VenueList = list;
            DataTable dt = dal.PR_EventArea_SelectByPK(Areaid);
                EventAreasModel modelEventAreas = new EventAreasModel();
            

            foreach (DataRow dr in dt.Rows)
                {

                    modelEventAreas.AreaID = Convert.ToInt32(dr["AreaID"]);
                    modelEventAreas.VenueID = Convert.ToInt32(dr["VenueID"]);
                    modelEventAreas.AreaN = Convert.ToString(dr["AreaName"]);
                    modelEventAreas.AreaType = Convert.ToString(dr["AreaType"]);
                    modelEventAreas.SittingCapacity = Convert.ToInt32(dr["SittingCapacity"]);
                    modelEventAreas.FloatingCapacity = Convert.ToInt32(dr["FloatingCapacity"]);

                }
                
                return View("Update", modelEventAreas);




            


            


        }
        #endregion
        #region delete
        public IActionResult delete(int AreaID)
        {

            dal.PR_EventArea_DeleteByPK(AreaID);
            TempData["success"] = ("Category deleted successfully");
            return RedirectToAction("Index");
        }
        #endregion
        #region Save

        public IActionResult Save(EventAreasModel eventModel)
        {
            


            if (eventModel.AreaID == null)
            {
                
                
                int count = eventModel.AreaName.Count();
                for (int i = 0; i < count; i++)
                {
                    var newEventModel = new EventAreasModel
                    {
                        VenueID = eventModel.VenueID,
                        AreaN = eventModel.AreaName[i],
                        AreaType = eventModel.AreaT[i],
                        SittingCapacity = eventModel.SittingC[i],
                        FloatingCapacity = eventModel.FloatingC[i]
                    };
                    dal.PR_EventArea_Insert(newEventModel);
                }
            }



            else
            {
                Console.WriteLine(eventModel.AreaN);
                Console.WriteLine(eventModel.VenueID);
                Console.WriteLine(eventModel.AreaID);
                dal.PR_EventArea_UpdateByPK(eventModel);
            }

            if (eventModel.AreaID == null)
            {
                TempData["Success"] = ("Area Added Successfully");

            }
            else
            {
                TempData["Success"] = ("Area Updated Successfully");

            }




            return RedirectToAction("Index", new { Venueid = eventModel.VenueID }); 

        }

    }
    #endregion
}



