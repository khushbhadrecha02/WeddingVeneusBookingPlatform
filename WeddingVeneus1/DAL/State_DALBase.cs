using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.State.Models;

namespace WeddingVeneus1.DAL
{
    public class State_DALBase:DAL_Helpers
    {
        #region dbo.PR_State_SelectAll
        public DataTable PR_State_SelectAll()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_State_SelectAll");
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
        #region PR_State_Insert
        public void PR_State_Insert(StateModel stateModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_State_Insert");
                db.AddInParameter(dbCMD, "StateName", SqlDbType.VarChar, stateModel.StateName);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion
        #region PR_State_SelectByPK
        public DataTable PR_State_SelectByPK(int? Stateid)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_State_SelectByPK");
                db.AddInParameter(dbCMD, "StateID", SqlDbType.Int,Stateid);
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
        

        #region PR_State_UpdateByPK
        public void PR_State_UpdateByPK(StateModel stateModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_State_UpdateByPK");
                db.AddInParameter(dbCMD, "StateID", SqlDbType.Int, stateModel.StateID);
                db.AddInParameter(dbCMD, "StateName", SqlDbType.VarChar, stateModel.StateName);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region PR_State_DeleteByPK
        public void PR_State_DeleteByPK(int StateID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_State_DeleteByPK");
                db.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
