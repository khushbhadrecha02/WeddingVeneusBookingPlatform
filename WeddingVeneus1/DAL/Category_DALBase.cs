using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.City.Models;
using WeddingVeneus1.Areas.Category.Models;
using WeddingVeneus1.Areas.State.Models;

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
        #region PR_MST_Category_SelectByPage
        public DataTable PR_MST_State_SelectByPage(MST_Category_SearchModel mst_Category_SearchModel,bool? ISConfirmed)
        {
            try
            {

                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_Category_SelectByPage");

                if (mst_Category_SearchModel == null)
                {
                    db.AddInParameter(dbCMD, "CategoryName", SqlDbType.VarChar, string.Empty);
                    db.AddInParameter(dbCMD, "ISConfirmed", SqlDbType.Bit, ISConfirmed);

                }
                else
                {
                    if (mst_Category_SearchModel.SubmitType == "list")
                    {
                        db.AddInParameter(dbCMD, "CategoryName", SqlDbType.VarChar, string.Empty);
                        db.AddInParameter(dbCMD, "ISConfirmed", SqlDbType.Bit, ISConfirmed);


                    }
                    else
                    {
                        db.AddInParameter(dbCMD, "CategoryName", SqlDbType.VarChar, mst_Category_SearchModel.CategoryName);
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
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, categoryModel.UserID);

                
                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    if (dr.Read())
                    {
                        // Read the BookingID from the result set
                        //categoryModel.CategoryID = Convert.ToInt32(dr["LastInsertedID"]);
                        categoryModel.Email = Convert.ToString(dr["Email"]);
                        categoryModel.CategoryName = Convert.ToString(dr["CategoryName"]);
                    }
                }

            }   
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());   
            }
        }
        #endregion
        #region PR_MST_VenueCategory_InsertForAdmin
        public void PR_MST_VenueCategory_InsertForAdmin(CategoryModel categoryModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueCategory_InsertForAdmin");
                db.AddInParameter(dbCMD, "CategoryPhoto", SqlDbType.VarChar, categoryModel.CategoryPhoto);
                db.AddInParameter(dbCMD, "CategoryName", SqlDbType.VarChar, categoryModel.CategoryName);
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, categoryModel.UserID);

                db.ExecuteNonQuery(dbCMD);
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
        #region PR_MST_Category_ApproveCategoryStatus
        public void PR_MST_Category_ApproveCategoryStatus(int CategoryID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_Category_ApproveCategoryStatus");
                db.AddInParameter(dbCMD, "CategoryID", SqlDbType.Int, CategoryID);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
        #region PR_MST_Category_SelectCategoryIDByCategoryID
        public DataTable PR_MST_Category_SelectCategoryIDByCategoryID(int? Categoryid)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_Category_SelectCategoryIDByCategoryID");
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
        #region PR_MST_STATE_RejectState
        public override void RejectEntity(int CategoryID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueCategory_RejectState");
                db.AddInParameter(dbCMD, "CategoryID", SqlDbType.Int, CategoryID);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
        #region PR_MST_State_SelectUserIDByStateID
        public override DataTable SelectUserIDByEntityID(int Categoryid)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_Category_SelectCategoryIDByCategoryID");
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
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        #endregion

    }
}
