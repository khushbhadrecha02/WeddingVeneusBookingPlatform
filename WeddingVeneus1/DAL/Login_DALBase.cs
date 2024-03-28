using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.Login.Models;
using WeddingVeneus1.Areas.Category.Models;
using WeddingVeneus1.Areas.Booking.Models;

namespace WeddingVeneus1.DAL
{
    public class Login_DALBase:DAL_Helpers
    {
        #region PR_Login_SelectByEmailAndPassword
        public DataTable PR_Login_SelectByEmailAndpassword(LoginModel loginModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_User_SelectByEmailAndPassword");
                db.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, loginModel.Email);
                db.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, loginModel.Password);
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
        #region PR_MST_User_SelectByPK
        public DataTable PR_MST_User_SelectByPK(int UserID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_User_SelectByPK");
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int,UserID);
                
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
        #region PR_Login_CheckUniqueConstraint
        public DataTable PR_Login_CheckUniqueConstraint(string Email)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Login_CheckUniqueConstraint");
                db.AddInParameter(dbCMD, "Email", SqlDbType.VarChar,Email);
                
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
        #region dbo.PR_MST_Role_SelectByComboBox
        public DataTable PR_MST_Role_SelectByComboBox()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_Role_SelectByComboBox");



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
        #region PR_MST_User_Insert
        public void PR_MST_User_Insert(LoginModel loginModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_User_Insert");
                db.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, loginModel.UserName);
                db.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, loginModel.Email);
                db.AddInParameter(dbCMD, "RoleID", SqlDbType.Int, loginModel.RoleID);
                db.AddInParameter(dbCMD, "Photopath", SqlDbType.VarChar, loginModel.PhotoPath);
                db.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, loginModel.Password);
                db.AddInParameter(dbCMD, "ContactNO", SqlDbType.VarChar, loginModel.ContactNO);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region PR_MST_User_AdminInsert
        public void PR_MST_User_AdminInsert(LoginModel loginModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_User_AdminInsert");
                db.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, loginModel.UserName);
                db.AddInParameter(dbCMD, "Email", SqlDbType.VarChar, loginModel.Email);
                db.AddInParameter(dbCMD, "RoleID", SqlDbType.Int, loginModel.RoleID);
                db.AddInParameter(dbCMD, "Photopath", SqlDbType.VarChar, loginModel.PhotoPath);
                db.AddInParameter(dbCMD, "Password", SqlDbType.VarChar, loginModel.Password);
                db.AddInParameter(dbCMD, "ContactNO", SqlDbType.VarChar, loginModel.ContactNO);
                db.ExecuteNonQuery(dbCMD);



            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region PR_MST_User_SelectAdminRequestList
        public DataTable PR_MST_User_SelectAdminRequestList()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_User_SelectAdminRequestList");
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
        #region PR_MST_User_ApproveAdminAccess
        public void PR_MST_User_ApproveAdminAccess(int UserID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_User_ApproveAdminAccess");
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region PR_MSt_User_UpdateProfilePhoto
        public void PR_MSt_User_UpdateProfilePhoto(UpdatePhoto updatePhoto)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MSt_User_UpdateProfilePhoto");
                
                db.AddInParameter(dbCMD, "Photopath", SqlDbType.VarChar, updatePhoto.PhotoPath);
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, updatePhoto.UserID);

                db.ExecuteNonQuery(dbCMD);



            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region PR_Mst_User_DeleteUserPhotoByUserID
        public void PR_Mst_User_DeleteUserPhotoByUserID(int UserID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Mst_User_DeleteUserPhotoByUserID");

                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);

                db.ExecuteNonQuery(dbCMD);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }
        }
        #endregion
        #region PR_MST_User_UpdateUserDetails
        public void PR_MST_User_UpdateUserDetails(LoginModelForDisplay loginModelForDisplay)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_User_UpdateUserDetails");

                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, loginModelForDisplay.UserID);
                db.AddInParameter(dbCMD, "UserName", SqlDbType.VarChar, loginModelForDisplay.UserName);
                
                db.AddInParameter(dbCMD, "ContactNO", SqlDbType.VarChar, loginModelForDisplay.ContactNO);

                db.ExecuteNonQuery(dbCMD);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #region PR_MST_User_UpdateEmail
        public void PR_MST_User_UpdateEmail(UpdateEmail updateEmail)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_User_UpdateEmail");

                
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, updateEmail.UserID);

                db.AddInParameter(dbCMD, "NewEmail", SqlDbType.VarChar, updateEmail.UpdatedEmail);

                db.ExecuteNonQuery(dbCMD);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #region PR_MST_User_ChangePassword
        public void PR_MST_User_ChangePassword(UpdatePassword updatePassword)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_User_ChangePassword");

                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, updatePassword.UserID);
                db.AddInParameter(dbCMD, "CurrentPassword", SqlDbType.VarChar, updatePassword.OldPassword);

                db.AddInParameter(dbCMD, "NewPassword", SqlDbType.VarChar, updatePassword.ReEnterNewPassword);

                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    if (dr.Read())
                    {
                        // Read the BookingID from the result set
                        updatePassword.Status = Convert.ToString(dr["Status"]);
                    }
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
