using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.EventAreas.Models;
using WeddingVeneus1.Areas.Photos.Models;

namespace WeddingVeneus1.DAL
{
    public class Photos_DALBase: DAL_Helpers
    {
        #region PR_Photos_SelectByVenueID
        public DataTable  PR_Photos_SelectByVenueID(int venueID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Photos_SelectByVenueID");
                db.AddInParameter(dbCMD, "VenueId", SqlDbType.Int, venueID);

                //db.AddInParameter(dbCMD,"CountryName",SqlDbType.VarChar,countryname);
                DataTable dt = new DataTable();
                dt.Columns.Add();
                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region PR_Photos_SelectByPK
        public DataTable PR_Photos_SelectByPK(int Photoid)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Photos_SelectByPK");
                db.AddInParameter(dbCMD, "PhotoId", SqlDbType.Int, Photoid);
                DataTable dt = new DataTable();
                dt.Columns.Add();
                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region PR_Photos_Insert
        public void PR_Photos_Insert(PhotosModel photosModel)
        {
            try
            {

                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Photos_Insert");
                db.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, photosModel.PhotoPath);
                
                db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, photosModel.VenueID);
                
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
        #region PR_Photos_Update
        public void PR_Photos_Update(PhotosModel photosModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Photos_Update");
                db.AddInParameter(dbCMD, "PhotoID", SqlDbType.Int, photosModel.PhotoID);
                db.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, photosModel.PhotoPath);
               
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region PR_EventArea_DeleteByPK
        public void PR_EventArea_DeleteByPK(int AreaID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_EventArea_DeleteByPK");
                db.AddInParameter(dbCMD, "AreaID", SqlDbType.Int, AreaID);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region PR_MST_STATE_RejectState
        public override void RejectEntity(int StateID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_STATE_RejectState");
                db.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
        #region PR_MST_State_SelectUserIDByStateID
        public override DataTable SelectUserIDByEntityID(int StateID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_State_SelectUserIDByStateID");
                db.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                DataTable dt = new DataTable();
                dt.Columns.Add();
                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        #endregion
    }
}
