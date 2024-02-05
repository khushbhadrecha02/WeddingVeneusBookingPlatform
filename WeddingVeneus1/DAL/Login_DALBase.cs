using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.Login.Models;

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
        #region PR_Branch_Insert
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
    }
}
