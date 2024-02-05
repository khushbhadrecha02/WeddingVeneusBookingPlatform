using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using WeddingVeneus1.Areas.Booking.Models;

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
                db.AddInParameter(dbCMD, "Name", SqlDbType.NVarChar, bookingModel.UserID);
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
        #region PR_Booking_SelectByUserID
        public DataTable PR_Booking_SelectByUserID(int UserID)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(ConnString);
                DbCommand dbCMD = db.GetStoredProcCommand("PR_Booking_SelectByUserID");
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

    }
}
#endregion
