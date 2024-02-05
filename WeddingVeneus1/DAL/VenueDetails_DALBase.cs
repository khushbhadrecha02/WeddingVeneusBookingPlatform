﻿using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.VenueDetails.Models;

namespace WeddingVeneus1.DAL
{
    public class VenueDetails_DALBase:DAL_Helpers
    {
        #region dbo.PR_MST_VenueDetails_SelectByUserID
        public DataTable PR_MST_VenueDetails_SelectByUserID(int UserID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueDetails_SelectByUserID");
                db.AddInParameter(dbCMD,"UserID",SqlDbType.Int,UserID);
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
        #region dbo.PR_City_SelectByComboBox
        public DataTable PR_City_SelectByComboBox(int StateID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_City_SelectByComboBox");
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
                return null;
            }
        }
        #endregion
        #region dbo.PR_Category_SelectByComboBox
        public DataTable PR_Category_SelectByComboBox()
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueCategory_SelectByComboBox");
                


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
        #region PR_VenueDetails_SelectByPK
        public DataTable PR_VenueDetails_SelectByPK(int? Venueid)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueDetails_SelectByPK");
                db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, Venueid);
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
        #region PR_VenueDetails_Insert
        public void PR_VenueDetails_Insert(VenueDetailsModel venueDetailsModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueDetails_Insert");
                db.AddInParameter(dbCMD, "VenueName", SqlDbType.VarChar, venueDetailsModel.VenueName);
                db.AddInParameter(dbCMD, "StateID", SqlDbType.Int, venueDetailsModel.StateID);
                db.AddInParameter(dbCMD, "CityID", SqlDbType.Int, venueDetailsModel.CityID);
                db.AddInParameter(dbCMD, "CategoryID", SqlDbType.Int, venueDetailsModel.CategoryID);
                db.AddInParameter(dbCMD, "ContactNO", SqlDbType.Int, venueDetailsModel.ContactNO);
                db.AddInParameter(dbCMD, "GuestCapacity", SqlDbType.Int, venueDetailsModel.GuestCapacity);
                db.AddInParameter(dbCMD, "RentPerDay", SqlDbType.Int, venueDetailsModel.RentPerDay);
                db.AddInParameter(dbCMD, "Address", SqlDbType.VarChar, venueDetailsModel.Address);
                
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, venueDetailsModel.UserID);
                db.AddInParameter(dbCMD, "AirConditioned", SqlDbType.Bit, venueDetailsModel.AirConditioned);
                db.AddInParameter(dbCMD, "Cuisines", SqlDbType.NVarChar, venueDetailsModel.Cuisines);
                db.AddInParameter(dbCMD, "AdvancePayment", SqlDbType.NVarChar, venueDetailsModel.AdvancePayment);
                db.AddInParameter(dbCMD, "PaymentAfterEvent", SqlDbType.NVarChar, venueDetailsModel.PaymentAfterEvent);
                db.AddInParameter(dbCMD, "DJPolicy", SqlDbType.NVarChar, venueDetailsModel.DJPolicy);
                db.AddInParameter(dbCMD, "GuestRooms", SqlDbType.NVarChar, venueDetailsModel.GuestRooms);
                db.AddInParameter(dbCMD, "ACRooms", SqlDbType.NVarChar, venueDetailsModel.ACRooms);
                db.AddInParameter(dbCMD, "BikeParking", SqlDbType.NVarChar, venueDetailsModel.BikeParking);
                db.AddInParameter(dbCMD, "CarParking", SqlDbType.NVarChar, venueDetailsModel.CarParking);
                db.AddInParameter(dbCMD, "ValetParking", SqlDbType.NVarChar, venueDetailsModel.ValetParking);
                db.AddInParameter(dbCMD, "OutsideDecoration", SqlDbType.NVarChar, venueDetailsModel.OutsideDecoration);
                db.AddInParameter(dbCMD, "AlcoholPolicy", SqlDbType.NVarChar, venueDetailsModel.AlcoholPolicy);
                db.AddInParameter(dbCMD, "VenueDescription", SqlDbType.NVarChar, venueDetailsModel.VenueDescription);
                db.AddInParameter(dbCMD, "CancellationPolicy", SqlDbType.NVarChar, venueDetailsModel.CancellationPolicy);

                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
        #region PR_VenueDetails_Update
        public void PR_VenueDetails_Update(VenueDetailsModel venueDetailsModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueDetails_UpdateByPK");
                db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, venueDetailsModel.VenueID);
                db.AddInParameter(dbCMD, "VenueName", SqlDbType.VarChar, venueDetailsModel.VenueName);
                db.AddInParameter(dbCMD, "StateID", SqlDbType.Int, venueDetailsModel.StateID);
                db.AddInParameter(dbCMD, "CityID", SqlDbType.Int, venueDetailsModel.CityID);
                db.AddInParameter(dbCMD, "CategoryID", SqlDbType.Int, venueDetailsModel.CategoryID);
                db.AddInParameter(dbCMD, "ContactNO", SqlDbType.Int, venueDetailsModel.ContactNO);
                db.AddInParameter(dbCMD, "GuestCapacity", SqlDbType.Int, venueDetailsModel.GuestCapacity);
                db.AddInParameter(dbCMD, "RentPerDay", SqlDbType.Int, venueDetailsModel.RentPerDay);
                db.AddInParameter(dbCMD, "Address", SqlDbType.VarChar, venueDetailsModel.Address);
                
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, venueDetailsModel.UserID);
                db.AddInParameter(dbCMD, "AirConditioned", SqlDbType.VarChar, venueDetailsModel.AirConditioned);
                db.AddInParameter(dbCMD, "Cuisines", SqlDbType.NVarChar, venueDetailsModel.Cuisines);
                db.AddInParameter(dbCMD, "AdvancePayment", SqlDbType.NVarChar, venueDetailsModel.AdvancePayment);
                db.AddInParameter(dbCMD, "PaymentAfterEvent", SqlDbType.NVarChar, venueDetailsModel.PaymentAfterEvent);
                db.AddInParameter(dbCMD, "DJPolicy", SqlDbType.NVarChar, venueDetailsModel.DJPolicy);
                db.AddInParameter(dbCMD, "GuestRooms", SqlDbType.NVarChar, venueDetailsModel.GuestRooms);
                db.AddInParameter(dbCMD, "ACRooms", SqlDbType.NVarChar, venueDetailsModel.ACRooms);
                db.AddInParameter(dbCMD, "BikeParking", SqlDbType.NVarChar, venueDetailsModel.BikeParking);
                db.AddInParameter(dbCMD, "CarParking", SqlDbType.NVarChar, venueDetailsModel.CarParking);
                db.AddInParameter(dbCMD, "ValetParking", SqlDbType.NVarChar, venueDetailsModel.ValetParking);
                db.AddInParameter(dbCMD, "OutsideDecoration", SqlDbType.NVarChar, venueDetailsModel.OutsideDecoration);
                db.AddInParameter(dbCMD, "AlcoholPolicy", SqlDbType.NVarChar, venueDetailsModel.AlcoholPolicy);
                db.AddInParameter(dbCMD, "VenueDescription", SqlDbType.NVarChar, venueDetailsModel.VenueDescription);
                db.AddInParameter(dbCMD, "CancellationPolicy", SqlDbType.NVarChar, venueDetailsModel.CancellationPolicy);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
        #region PR_State_DeleteByPK
        public void PR_VenueDetails_DeleteByPK(int VenueID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueDetails_DeleteByPK");
                db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, VenueID);
                db.ExecuteNonQuery(dbCMD);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
        #region PR_State_DeleteByPK

        public DataTable PR_MST_VenueDetails_SelectByComboBox(int UserID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueDetails_SelectByComboBox");
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID);
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
        #region PR_MST_VenueDetails_SelectByPage
        public DataTable PR_MST_VenueDetails_SelectByPage(Venue_Search_Model venue_Search_Model)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueDetails_SelectByPage");
                db.AddInParameter(dbCMD, "StateID", SqlDbType.Int, venue_Search_Model.StateID);
                db.AddInParameter(dbCMD, "CityID", SqlDbType.Int, venue_Search_Model.CityID);
                db.AddInParameter(dbCMD, "CategoryID", SqlDbType.Int, venue_Search_Model.CategoryID);
                db.AddInParameter(dbCMD, "RentPerDay", SqlDbType.Int, venue_Search_Model.RentPerDay);
                db.AddInParameter(dbCMD, "GuestCapacity", SqlDbType.Int, venue_Search_Model.GuestCapacity);
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
        #region [PR_MST_VenueDetails_SelectByCityID
        public DataTable PR_MST_VenueDetails_SelectByCityID(int VenueID,int CityID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_VenueDetails_SelectByCityID");
                db.AddInParameter(dbCMD, "CityID", SqlDbType.Int, CityID);
                db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, VenueID);
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