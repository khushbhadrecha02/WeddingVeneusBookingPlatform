using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.City.Models;
using WeddingVeneus1.Areas.State.Models;

namespace WeddingVeneus1.DAL
{
    public class City_DALBase:DAL_Helpers
    {
        #region dbo.PR_City_SelectAll
        public DataTable PR_City_SelectAll()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_City_SelectAll");
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
        #region dbo.PR_State_SelectByComboBox
        public DataTable PR_State_SelectByComboBox()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_State_SelectByComboBox");
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
        #region PR_City_SelectByPK
        public DataTable PR_City_SelectByPK(int? Cityid)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_City_SelectByPK");
                db.AddInParameter(dbCMD, "CityID", SqlDbType.Int, Cityid);
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
        #region PR_City_Insert
        public void PR_City_Insert(CityModel cityModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_City_Insert");
                db.AddInParameter(dbCMD, "StateID", SqlDbType.Int, cityModel.StateID);
                db.AddInParameter(dbCMD, "CityName", SqlDbType.VarChar,cityModel.CityName);
                db.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, cityModel.PhotoPath);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
            }
        }
        #endregion
        #region PR_City_UpdateByPK
        public void PR_City_UpdateByPK(CityModel cityModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_City_UpdateByPK");
                db.AddInParameter(dbCMD, "CityID", SqlDbType.Int, cityModel.CityID);
                db.AddInParameter(dbCMD, "StateID", SqlDbType.Int, cityModel.StateID);
                db.AddInParameter(dbCMD, "CityName", SqlDbType.VarChar, cityModel.CityName);
                db.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, cityModel.PhotoPath);

                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());   
            }
        }
        #endregion
        #region PR_City_DeleteByPK
        public void PR_City_DeleteByPK(int CityID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_City_DeleteByPK");
                db.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
        #region PR_MST_City_CityWiseVenueCount
        public DataTable PR_MST_City_CityWiseVenueCount()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_City_CityWiseVenueCount");
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
    }
}
