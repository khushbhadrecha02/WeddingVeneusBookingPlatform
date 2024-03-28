using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.Booking.Models;
using WeddingVeneus1.Areas.VenueDetails.Models;

namespace WeddingVeneus1.DAL
{
    public class Booking_DALBase : DAL_Helpers
    {
        #region PR_User_SelectUserNameByUserID
        public DataTable PR_User_SelectUserNameByUserID(int Userid)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_User_SelectUserNameByUserID");
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, Userid);
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
        #region PR_Booking_CheckBookingStatus
        public DataTable PR_Booking_CheckBookingStatus(CheckBooking checkBooking)
        {
            try
            {
                
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("GetBookingsCountAndDetails");
                db.AddInParameter(dbCMD, "BookingStartDate", SqlDbType.DateTime, checkBooking.BookingStartDate);
                db.AddInParameter(dbCMD, "BookingEndDate", SqlDbType.DateTime, checkBooking.BookingEndDate);
                db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, checkBooking.VenueID);
                DataTable dt = new DataTable();
                dt.Columns.Add();
                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                Console.WriteLine(checkBooking.BookingStartDate);
                Console.WriteLine(checkBooking.BookingEndDate);
                Console.WriteLine(checkBooking.VenueID);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
                 
            }
        }
        #endregion
        #region PR_Booking_CalculateAmount
        public DataTable PR_Booking_CalculateAmount(BookingModel bookingModel)
        {
            try
            {

                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Booking_CalculateAmount");
                db.AddInParameter(dbCMD, "NumberOfDays", SqlDbType.Int, bookingModel.NumOfDays);
                db.AddInParameter(dbCMD, "RentPerDay", SqlDbType.Decimal, bookingModel.RentPerDay);
                DataTable dt = new DataTable();
                dt.Columns.Add();
                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                //Console.WriteLine(bookingModel.Amount);
                
                Console.WriteLine(bookingModel.RentPerDay);
                
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }
        #endregion
        #region PR_Booking_CalculateAdvanceAndPaymentPer
        public DataTable PR_Booking_CalculateAdvanceAndPaymentPer(BookingModel bookingModel)
        {
            try
            {

                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Booking_CalculateAdvanceAndPaymentPer");
                db.AddInParameter(dbCMD, "AdvancePayment", SqlDbType.Decimal, bookingModel.AdvancePayment);
                
                db.AddInParameter(dbCMD, "Amount", SqlDbType.Decimal, bookingModel.Amount);
                DataTable dt = new DataTable();
                dt.Columns.Add();
                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                //Console.WriteLine(bookingModel.Amount);
                Console.WriteLine(bookingModel.AdvancePaymentPer);
                Console.WriteLine(bookingModel.PaymentAfterEventPer);

                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }
        #endregion
        #region InsertBooking
        public void InsertBooking(BookingModel bookingModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("InsertBooking");
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, bookingModel.UserID);
                db.AddInParameter(dbCMD, "Amount", SqlDbType.Decimal, bookingModel.Amount);
                db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, bookingModel.VenueID);

                
                db.AddInParameter(dbCMD, "BookingStartDate", SqlDbType.DateTime, bookingModel.BookingStartDate);
                db.AddInParameter(dbCMD, "BookingEndDate", SqlDbType.DateTime, bookingModel.BookingEndDate);
                db.AddInParameter(dbCMD, "NumberOfDays", SqlDbType.Int, bookingModel.NumOfDays);
                
                //db.AddOutParameter(dbCMD, "BookingID", SqlDbType.Int, 0);


                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    if (dr.Read())
                    {
                        // Read the BookingID from the result set
                        bookingModel.BookingID = Convert.ToInt32(dr["BookingID"]);
                    }
                }
                //bookingModel.BookingID = Convert.ToInt32(db.GetParameterValue(dbCMD, "BookingID"));

            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #region InsertaCancelBookingRequest
        public void InsertCancelBookingRequest(CancelModel bookingModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("InsertCancelBookingRequest");
                db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, bookingModel.UserID);
                db.AddInParameter(dbCMD, "BookingID", SqlDbType.Int, bookingModel.BookingID);
                db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, bookingModel.VenueID);

                db.AddInParameter(dbCMD, "Reason", SqlDbType.NVarChar, bookingModel.Reason);




                //db.AddOutParameter(dbCMD, "BookingID", SqlDbType.Int, 0);


                db.ExecuteNonQuery(dbCMD);

                //bookingModel.BookingID = Convert.ToInt32(db.GetParameterValue(dbCMD, "BookingID"));

            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #region PR_Booking_CalculateAdvanceAndPaymentAfterEvent
        public DataTable PR_Booking_CalculateAdvanceAndPaymentAfterEvent(BookingModel bookingModel)
        {
            try
            {

                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Booking_CalculateAdvanceAndPaymentAfterEvent");
                db.AddInParameter(dbCMD, "AdvancePaymentPer", SqlDbType.Decimal, bookingModel.AdvancePaymentPer);

                db.AddInParameter(dbCMD, "Amount", SqlDbType.Decimal, bookingModel.Amount);
                DataTable dt = new DataTable();
                dt.Columns.Add();
                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                //Console.WriteLine(bookingModel.Amount);
                Console.WriteLine(bookingModel.AdvancePaymentPer);
                Console.WriteLine(bookingModel.PaymentAfterEventPer);

                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }
        #endregion
        #region InsertPayment
        public void InsertPayment(BookingModel bookingModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("InsertPayment");
                db.AddInParameter(dbCMD, "Amount", SqlDbType.Decimal, bookingModel.AdvancePayment);
                db.AddInParameter(dbCMD, "UserID", SqlDbType.NVarChar, bookingModel.UserID);
                db.AddInParameter(dbCMD, "Remarks", SqlDbType.NVarChar, bookingModel.Remarks);
                db.AddInParameter(dbCMD, "BookingID", SqlDbType.Int, bookingModel.BookingID);



                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    if (dr.Read())
                    {
                        // Read the BookingID from the result set
                        bookingModel.PaymentID = Convert.ToInt32(dr["PaymentID"]);
                        bookingModel.PaymentAmount = Convert.ToDecimal(dr["Amount"]);
                        bookingModel.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
        #region PR_Booking_SelectByPK
        public DataTable PR_Booking_SelectByPK(int? Bookingid)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Booking_SelectByPK");
                db.AddInParameter(dbCMD, "BookingID", SqlDbType.Int, Bookingid);
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
        #region PR_Payment_SelectByPK
        public DataTable PR_Payment_SelectByPK(int? Paymentid)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Payment_SelectByPK");
                db.AddInParameter(dbCMD, "PaymentID", SqlDbType.Int, Paymentid);
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
        #region PR_Booking_SelectByPK
        public DataTable ValidateDateRange(BookingModel bookingModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("ValidateDateRange");
                db.AddInParameter(dbCMD, "BookingStartDate", SqlDbType.DateTime, bookingModel.BookingStartDate);
                db.AddInParameter(dbCMD, "BookingEndDate", SqlDbType.DateTime, bookingModel.BookingEndDate);

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
                Console.WriteLine (ex.Message); 
                return null;
            }
        }
        #endregion
        #region InsertBooking
        public void PR_MST_Booking_MoveUnconfirmedBookings(int BookingID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_MST_Booking_MoveUnconfirmedBookings");
                db.AddInParameter(dbCMD, "BookingID", SqlDbType.Int, BookingID);
                
                //db.AddOutParameter(dbCMD, "BookingID", SqlDbType.Int, 0);


                
                //bookingModel.BookingID = Convert.ToInt32(db.GetParameterValue(dbCMD, "BookingID"));

            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }
        }
        #region PR_Booking_SelectByPage
        public DataTable PR_Booking_SelectByPage(Booking_SearchModel booking_SearchModel,int? UserID,int? VenueID)
        {
            try
            {
                
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Booking_SelectByPage");
                if (booking_SearchModel == null)
                {
                    db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, DBNull.Value);
                    db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, VenueID.HasValue ? (object)VenueID.Value : DBNull.Value);
                    db.AddInParameter(dbCMD, "BookingStartDate", SqlDbType.Decimal, DBNull.Value);
                    db.AddInParameter(dbCMD, "BookingEndDate", SqlDbType.Decimal, DBNull.Value);
                    db.AddInParameter(dbCMD, "PendingAmount", SqlDbType.Decimal, DBNull.Value);
                    db.AddInParameter(dbCMD, "AmountPaid", SqlDbType.Decimal, DBNull.Value);
                    
                }
                else
                {
                    if (booking_SearchModel.SubmitType == "list")
                    {
                        db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, DBNull.Value);
                        db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, DBNull.Value);
                        db.AddInParameter(dbCMD, "BookingStartDate", SqlDbType.DateTime, DBNull.Value);
                        db.AddInParameter(dbCMD, "BookingEndDate", SqlDbType.DateTime, DBNull.Value);
                        db.AddInParameter(dbCMD, "PendingAmount", SqlDbType.Decimal, DBNull.Value);
                        db.AddInParameter(dbCMD, "AmountPaid", SqlDbType.Decimal, DBNull.Value);
                    }
                    else
                    {
                        db.AddInParameter(dbCMD, "UserID", SqlDbType.Int, UserID.HasValue ? (object)UserID.Value : DBNull.Value);
                        db.AddInParameter(dbCMD, "VenueID", SqlDbType.Int, booking_SearchModel.VenueID);
                        db.AddInParameter(dbCMD, "BookingStartDate", SqlDbType.DateTime, booking_SearchModel.BookingStartDate);
                        db.AddInParameter(dbCMD, "BookingEndDate", SqlDbType.DateTime, booking_SearchModel.BookingEndDate);
                        db.AddInParameter(dbCMD, "PendingAmount", SqlDbType.Decimal, booking_SearchModel.PendingAmount);
                        db.AddInParameter(dbCMD, "AmountPaid", SqlDbType.Decimal, booking_SearchModel.AmountPaid);
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
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion
        #region PR_CancelBooking_SelectByUserID
        public DataTable PR_CancelBooking_SelectByUserID(int UserID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_CancelBooking_SelectByUserID");
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
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion
        #region PR_CancelBooking_SelectByVenueID
        public DataTable PR_CancelBooking_SelectByVenueID (int VenueID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_CancelBooking_SelectByVenueID");
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
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion
        #region InsertPaymentRefunded
        public void InsertPaymentRefunded(BookingModel bookingModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("InsertPaymentRefunded");
                db.AddInParameter(dbCMD, "Amount", SqlDbType.Decimal, bookingModel.AdvancePayment);
                db.AddInParameter(dbCMD, "UserID", SqlDbType.NVarChar, bookingModel.UserID);
                db.AddInParameter(dbCMD, "Remarks", SqlDbType.NVarChar, bookingModel.Remarks);
                db.AddInParameter(dbCMD, "BookingID", SqlDbType.Int, bookingModel.BookingID);
                db.AddInParameter(dbCMD, "CancelID", SqlDbType.Int, bookingModel.CancelID);




                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    if (dr.Read())
                    {
                        // Read the BookingID from the result set
                        bookingModel.PaymentID = Convert.ToInt32(dr["PaymentID"]);
                        bookingModel.PaymentAmount = Convert.ToDecimal(dr["Amount"]);
                        bookingModel.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
                    }
                }

            }
            catch (Exception ex)
            {
                // Handle exception
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
        #region PR_CancelBooking_RejectCancelBooking
        public void PR_CancelBooking_RejectCancelBooking(BookingModel bookingModel)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_CancelBooking_RejectCancelBooking");
            
                db.AddInParameter(dbCMD, "CancelID", SqlDbType.Int, bookingModel.CancelID);

                using (IDataReader dr = db.ExecuteReader(dbCMD))
                {
                    if (dr.Read())
                    {
                        // Read the BookingID from the result set
                        bookingModel.VenueID = Convert.ToInt32(dr["VenueID"]);
                        
                    }
                }





            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
            }
        }
        #endregion


    }
}
#endregion
