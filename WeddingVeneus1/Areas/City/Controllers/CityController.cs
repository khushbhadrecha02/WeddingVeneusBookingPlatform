using Microsoft.AspNetCore.Mvc;
using System.Data;
using WeddingVeneus1.Areas.City.Models;
using WeddingVeneus1.Areas.Photos.Models;
using WeddingVeneus1.Areas.State.Models;
using WeddingVeneus1.DAL;

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
                    dal.PR_City_Insert(cityModel);

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




                return RedirectToAction("Index");
            }
            
        
        #endregion
    }
}
