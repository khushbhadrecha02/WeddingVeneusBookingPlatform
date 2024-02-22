using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.State.Models;
using WeddingVeneus1.Areas.Booking.Models;
using WeddingVeneus1.Areas.Category.Models;

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
		#region PR_MST_State_SelectByPage
		public DataTable PR_MST_State_SelectByPage(MST_State_SearchModel mst_State_SearchModel,bool? ISConfirmed)
		{
			try
			{
                
                    SqlDatabase db = new SqlDatabase(ConnString);
                    DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_State_SelectByPage");
                    
                    if (mst_State_SearchModel == null)
                    {
                        db.AddInParameter(dbCMD, "StateName", SqlDbType.VarChar, string.Empty);
                        db.AddInParameter(dbCMD, "ISConfirmed", SqlDbType.Bit, ISConfirmed);
                    }
                    else
                    {
                        if(mst_State_SearchModel.SubmitType == "list")
                        {
                            db.AddInParameter(dbCMD, "StateName", SqlDbType.VarChar, string.Empty);
                            db.AddInParameter(dbCMD, "ISConfirmed", SqlDbType.Bit, ISConfirmed);
                    }
                        else
                        {
                            db.AddInParameter(dbCMD, "StateName", SqlDbType.VarChar, mst_State_SearchModel.StateName);
                            db.AddInParameter(dbCMD, "ISConfirmed", SqlDbType.Bit, ISConfirmed);
                    }

                    }

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
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, stateModel.UserID);

                //db.ExecuteNonQuery(dbCMD);
                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    if (dr.Read())
                    {
                        // Read the BookingID from the result set
                        stateModel.StateID = Convert.ToInt32(dr["LastInsertedID"]);
                        stateModel.Email = Convert.ToString(dr["Email"]);
                        stateModel.StateName = Convert.ToString(dr["StateName"]);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
        #region PR_MST_State_InsertForAdmin
        public void PR_MST_State_InsertForAdmin(StateModel stateModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_State_InsertForAdmin");
                db.AddInParameter(dbCMD, "StateName", SqlDbType.VarChar, stateModel.StateName);
                db.AddInParameter(dbCMD, "UserID", SqlDbType.VarChar, stateModel.UserID);
                

                db.ExecuteNonQuery(dbCMD);
                

            }
            catch (Exception ex)
            {

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
        #region PR_MST_State_ApproveStateStatus
        public void PR_MST_State_ApproveStateStatus(int StateID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_State_ApproveStateStatus");
                db.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
    }
}
