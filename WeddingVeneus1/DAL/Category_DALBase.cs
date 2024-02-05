using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.City.Models;
using WeddingVeneus1.Areas.Category.Models;

namespace WeddingVeneus1.DAL
{
    public class Category_DALBase:DAL_Helpers
    {
        #region dbo.PR_Category_SelectAll
        public DataTable PR_Category_SelectAll()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueCategory_SelectAll");
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
        #region PR_MST_VenueCategory_CategoryWiseVenueCount
        public DataTable PR_MST_VenueCategory_CategoryWiseVenueCount()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueCategory_CategoryWiseVenueCount");
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
        #region PR_Category_SelectByPK
        public DataTable PR_Category_SelectByPK(int? Categoryid)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueCategory_SelectByPK");
                db.AddInParameter(dbCMD, "CategoryID", SqlDbType.Int, Categoryid);
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
        #region PR_Category_Insert
        public void PR_Category_Insert(CategoryModel categoryModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueCategory_Insert");
                db.AddInParameter(dbCMD, "CategoryPhoto", SqlDbType.VarChar, categoryModel.CategoryPhoto);
                db.AddInParameter(dbCMD, "CategoryName", SqlDbType.VarChar, categoryModel.CategoryName);
                db.ExecuteNonQuery(dbCMD);
                
            }   
            catch (Exception ex)
            {
                
            }
        }
        #endregion
        #region PR_Category_UpdateByPK
        public void PR_Category_UpdateByPK(CategoryModel categoryModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueCategory_UpdateByPK");
                db.AddInParameter(dbCMD, "CategoryID", SqlDbType.Int, categoryModel.CategoryID);
                db.AddInParameter(dbCMD, "CategoryPhoto", SqlDbType.VarChar, categoryModel.CategoryPhoto);
                db.AddInParameter(dbCMD, "CategoryName", SqlDbType.VarChar, categoryModel.CategoryName);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region PR_Category_DeleteByPK
        public void PR_Category_DeleteByPK(int CategoryID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueCategory_DeleteByPK");
                db.AddInParameter(dbCMD, "CategoryID", SqlDbType.Int, CategoryID);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {

            }
        }
        #endregion

    }
}
