using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.EventAreas.Models;
using Microsoft.VisualBasic;

namespace WeddingVeneus1.DAL
{
    public class EventAreas_DALBase:DAL_Helpers
    {
        #region dbo.PR_EventArea_SelectALL
        public DataTable PR_EventArea_SelectBYVenueID(int venueID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_EventArea_SelectBYVenueID");
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
        #region PR_EventArea_SelectByPK
        public DataTable  PR_EventArea_SelectByPK (int? Areaid)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_EventArea_SelectByPK");
                db.AddInParameter(dbCMD, "AreaId", SqlDbType.Int, Areaid);
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
        #region PR_EventArea_Insert
        public void PR_EventArea_Insert(EventAreasModel eventAreasModel)
        {
            try
            {
                
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_EventArea_Insert");
                db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, eventAreasModel.VenueID);
                db.AddInParameter(dbCMD, "AreaName", SqlDbType.VarChar, eventAreasModel.AreaN);
                db.AddInParameter(dbCMD, "AreaType", SqlDbType.VarChar, eventAreasModel.AreaType);
                db.AddInParameter(dbCMD, "SittingCapacity", SqlDbType.Int, eventAreasModel.SittingCapacity);
                db.AddInParameter(dbCMD, "FloatingCapacity", SqlDbType.Int, eventAreasModel.FloatingCapacity);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
        #region PR_EventArea_UpdateByPK
        public void PR_EventArea_UpdateByPK(EventAreasModel eventAreasModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_EventArea_UpdateByPK");
                db.AddInParameter(dbCMD, "AreaID", SqlDbType.Int, eventAreasModel.AreaID);
                db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, eventAreasModel.VenueID);
                db.AddInParameter(dbCMD, "AreaName", SqlDbType.VarChar, eventAreasModel.AreaN);
                db.AddInParameter(dbCMD, "AreaType", SqlDbType.VarChar, eventAreasModel.AreaType);
                db.AddInParameter(dbCMD, "SittingCapacity", SqlDbType.Int, eventAreasModel.SittingCapacity);
                db.AddInParameter(dbCMD, "FloatingCapacity", SqlDbType.Int, eventAreasModel.FloatingCapacity);
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
    }
}
