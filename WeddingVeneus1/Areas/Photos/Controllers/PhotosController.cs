using Microsoft.AspNetCore.Mvc;
using System.Data;
using WeddingVeneus1.Areas.EventAreas.Models;
using WeddingVeneus1.Areas.Photos.Models;
using WeddingVeneus1.Areas.VenueDetails.Models;
using WeddingVeneus1.DAL;

namespace WeddingVeneus1.Areas.Photos.Controllers
{

    [Area("Photos")]
    [Route("Photos/{Controller}/{Action}")]
    public class PhotosController : Controller
    {
        #region GloblaStateDalObject
        Photos_DALBase dal = new Photos_DALBase();
        VenueDetails_DALBase dal1 = new VenueDetails_DALBase();

        #endregion
        #region Index
        public IActionResult Index(int venueID)
        {


            DataTable dt = dal.PR_Photos_SelectByVenueID(venueID);
            return View("Index", dt);
        }
        #region Create
        public IActionResult Create()
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


            return View("Create");
        }
        #endregion
        #region Update
        public IActionResult Update(int PhotoID)
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

            DataTable dt = dal.PR_Photos_SelectByPK(PhotoID);
            PhotosModel photosModel = new PhotosModel();


            foreach (DataRow dr in dt.Rows)
            {

                photosModel.PhotoID = Convert.ToInt32(dr["PhotoID"]);
                photosModel.PhotoPath = Convert.ToString(dr["PhotoPath"]);
                photosModel.VenueID = Convert.ToInt32(dr["VenueID"]);


            }

            return View("Update", photosModel);










        }
        #endregion
        #region Save
        [HttpPost]
        public IActionResult Save(PhotosModel photosModel)
        {
            


            if (photosModel.PhotoID == null)
            {
                

                int count = photosModel.FilePath.Count();
                
                for (int i = 0; i < count; i++)
                {
                    photosModel.File = photosModel.FilePath[i];
                    if (photosModel.File != null)
                    {
                        String FilePath = "wwwroot\\Upload";
                        string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileNameWithPath = Path.Combine(path, photosModel.File.FileName);
                        photosModel.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + photosModel.File.FileName;
                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            photosModel.File.CopyTo(stream);
                        }
                    }
                    var newPhotosModel = new PhotosModel
                    {
                        VenueID = photosModel.VenueID,
                        PhotoPath = photosModel.PhotoPath

                    };
                    Console.WriteLine(newPhotosModel.PhotoPath);
                    dal.PR_Photos_Insert(newPhotosModel);
                }
            }



            else
            {
                if (photosModel.File != null)
                {
                    String FilePath = "wwwroot\\Upload";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileNameWithPath = Path.Combine(path, photosModel.File.FileName);
                    photosModel.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + photosModel.File.FileName;
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        photosModel.File.CopyTo(stream);
                    }
                }

                    dal.PR_Photos_Update(photosModel);
            }

            if (photosModel.PhotoID == null)
            {
                TempData["Success"] = ("Photo Added Successfully");

            }
            else
            {
                TempData["Success"] = ("Photos Updated Successfully");

            }




            return RedirectToAction("Index", new { venueID = photosModel.VenueID });

        }

    }
    #endregion
}
#endregion
