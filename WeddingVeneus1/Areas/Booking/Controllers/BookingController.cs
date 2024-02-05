using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Security.Cryptography.Xml;
using WeddingVeneus1.Areas.Booking.Models;

using WeddingVeneus1.Areas.City.Models;
using WeddingVeneus1.Areas.VenueDetails.Models;
using WeddingVeneus1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using DinkToPdf;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WeddingVeneus1.Areas.Booking.Controllers
{
    [Area("Booking")]
    [Route("Booking/{Controller}/{Action}")]
    public class BookingController : Controller
    {
        #region GloblaStateDalObject
        Booking_DALBase dal = new Booking_DALBase();
        VenueDetails_DALBase dal1 = new VenueDetails_DALBase();


        #endregion
        
        
        public IActionResult CheckBooking(int? venueID,string? venueName)

        {



            Booking_ViewModel booking_ViewModel1 = new Booking_ViewModel
            {

                bookingModel = new CheckBooking()
                {
                    VenueID = venueID,
                    VenueName = venueName,
                    UserID = HttpContext.Session.GetInt32("UserID").Value

                }
                

    };
            Console.WriteLine("Bookingdate" + booking_ViewModel1.bookingModel.BookingStartDate);
            return View("CheckBooking",booking_ViewModel1);
        }
        [HttpPost]
        public IActionResult CheckBookingStatus(Booking_ViewModel booking_ViewModel,string submit)
        {
            Console.WriteLine(ModelState.IsValid);
            try
            {
                

                if(ModelState.IsValid)
                {
                    DataTable dt = dal.PR_Booking_CheckBookingStatus(booking_ViewModel.bookingModel);
                    if(dt != null)
                    {
                        if(dt.Rows.Count == 0) 
                        {
                            if (submit == "Check")
                            {
                                var bookingViewModel3 = new Booking_ViewModel
                                {
                                    bookingModel = booking_ViewModel.bookingModel



                                };
                                TempData["BookingStatus"] = "Hall is not booked between these dates.";
                                return View("CheckBooking", bookingViewModel3);
                            }
                            else
                            {
                                BookingModel modelBooking = new BookingModel();
                                modelBooking.UserID = HttpContext.Session.GetInt32("UserID").Value;


                                modelBooking.BookingStartDate = booking_ViewModel.bookingModel.BookingStartDate;



                                modelBooking.BookingEndDate = booking_ViewModel.bookingModel.BookingEndDate;
                                Console.WriteLine(modelBooking.BookingEndDate);
                                modelBooking.VenueID = booking_ViewModel.bookingModel.VenueID;





                                DataTable dt2 = dal1.PR_VenueDetails_SelectByPK(modelBooking.VenueID);
                                foreach (DataRow dr in dt2.Rows)
                                {

                                    modelBooking.VenueName = Convert.ToString(dr["VenueName"]);
                                    modelBooking.RentPerDay = Convert.ToDecimal(dr["RentPerDay"]);
                                    modelBooking.AdvancePaymentPer = Convert.ToInt32(dr["AdvancePayment"]);
                                    modelBooking.DefaultAdvancePayPer = Convert.ToDecimal(dr["AdvancePayment"]);
                                    modelBooking.PaymentAfterEventPer = Convert.ToInt32(dr["PaymentAfterEvent"]);


                                }

                                DataTable dt1 = dal.PR_User_SelectUserNameByUserID(modelBooking.UserID);
                                foreach (DataRow dr in dt1.Rows)
                                {

                                    modelBooking.UserName = Convert.ToString(dr["UserName"]);


                                }
                                return View("Create", modelBooking);

                            }

                        }
                        else  
                        {
                            var bookingViewModel1 = new Booking_ViewModel
                            {
                                bookingModel = booking_ViewModel.bookingModel,
                                CheckStatus = dt


                            };
                            return View("CheckBooking", bookingViewModel1);
                        }
                        
                       

                    }
                    



                }
                

                var bookingViewModel2 = new Booking_ViewModel
                {
                    bookingModel = booking_ViewModel.bookingModel



                };

                Console.WriteLine(bookingViewModel2.bookingModel.VenueName);
                Console.WriteLine(bookingViewModel2.bookingModel.BookingStartDate);
                Console.WriteLine(bookingViewModel2.bookingModel.BookingEndDate);
                Console.WriteLine(bookingViewModel2.bookingModel.VenueID);
                Console.WriteLine(TempData["BookingStartDateError"]);
                return View("CheckBooking",bookingViewModel2);




            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                Console.Error.WriteLine(ex.Message);
                return View("Error");
                
            }
        }



        public IActionResult CalculateAmount(int NumberOfDays,decimal RentPerDay)
        {
            try
            {
                BookingModel bookingModel = new BookingModel
                {
                    NumOfDays = NumberOfDays,
                    RentPerDay = RentPerDay
                };
                //Console.WriteLine(bookingModel.NumberOfDays);
                //Console.WriteLine(bookingModel.RentPerDay);
                DataTable dt = dal.PR_Booking_CalculateAmount(bookingModel);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["Amount"] != DBNull.Value)
                    {
                        var rawValue = dr["Amount"]; // Debugging line
                        bookingModel.Amount = Convert.ToDecimal(rawValue);
                        
                    }



                    return Json(new { success = true, data = bookingModel.Amount });

                }
                else
                {
                    return Json(new { success = false });

                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return Json(new { success = false, });
            }
        }
        public IActionResult CalculateAdvancePaymentPer(decimal AdvancePayment, decimal Amount)
        {
            try
            {
                
                
                Console.WriteLine("Amount" + Amount);
                BookingModel bookingModel = new BookingModel
                {
                    AdvancePayment = AdvancePayment,
                    
                    
                    Amount = Amount
                };
                //Console.WriteLine("AdvancePaymentPer" + AdvancePaymentPer);
                //Console.WriteLine("PaymentAfterEventPer" + PaymentAfterEventPer);
                DataTable dt = dal.PR_Booking_CalculateAdvanceAndPaymentPer(bookingModel);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    bookingModel.AdvancePaymentPer = Convert.ToDecimal(dr["AdvancePaymentPer"]);
                    bookingModel.PaymentAfterEventPer = Convert.ToDecimal(dr["PaymentAfterEventPer"]);
                    bookingModel.PaymentAfterEvent = Convert.ToDecimal(dr["PaymentAfterEvent"]);






                    return Json(new { success = true, advancePaymentPer = bookingModel.AdvancePaymentPer, paymentAfterEventPer = bookingModel.PaymentAfterEventPer,paymentAfterEvent = bookingModel.PaymentAfterEvent });

                }
                else
                {
                    return Json(new { success = false });

                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return Json(new { success = false, });
            }
        }
        public IActionResult CalculateAdvancePayment(decimal AdvancePaymentPer, decimal Amount)
        {
            try
            {


                Console.WriteLine("Amount" + Amount);
                BookingModel bookingModel = new BookingModel
                {
                    AdvancePaymentPer = AdvancePaymentPer,


                    Amount = Amount
                };
                //Console.WriteLine("AdvancePaymentPer" + AdvancePaymentPer);
                //Console.WriteLine("PaymentAfterEventPer" + PaymentAfterEventPer);
                DataTable dt = dal.PR_Booking_CalculateAdvanceAndPaymentAfterEvent(bookingModel);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    bookingModel.AdvancePayment = Convert.ToDecimal(dr["AdvancePayment"]);
                    bookingModel.PaymentAfterEvent = Convert.ToDecimal(dr["PaymentAfterEvent"]);
                    






                    return Json(new { success = true, advancePayment = bookingModel.AdvancePayment, paymentAfterEvent = bookingModel.PaymentAfterEvent });

                }
                else
                {
                    return Json(new { success = false });

                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return Json(new { success = false, });
            }
        }
        [HttpPost]
        public IActionResult Create(BookingModel bookingModel) 
        {
            

            Console.WriteLine(bookingModel.AdvancePayment);
            Console.WriteLine(bookingModel.PaymentAfterEvent);
            Console.WriteLine(bookingModel.Amount);
            
            //Console.WriteLine("BookingStatus"+bookingModel.BookingStatus);
            Console.WriteLine(ModelState.IsValid);
            Console.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");




            if (ModelState.IsValid)
            {
                // Validation failed
                Console.WriteLine("Validation failed.");
                dal.InsertBooking(bookingModel);
                Console.WriteLine(bookingModel.BookingID);
                return RedirectToAction("PaymentPage", new { bookingID = bookingModel.BookingID, advancePayment = bookingModel.AdvancePayment});
            }

            

                
                

            
            else
            {
                
                return View("Create");
            }

        }
        public IActionResult PaymentPage(int bookingID,decimal? advancePayment)
        {
           DataTable dt =  dal.PR_Booking_SelectByPK(bookingID);
            BookingModel modelBooking = new BookingModel();
            foreach (DataRow dr in dt.Rows)
            {
                modelBooking.BookingID = Convert.ToInt32(dr["BookingID"]);
                modelBooking.UserID = Convert.ToInt32(dr["UserID"]);
                modelBooking.Amount = Convert.ToDecimal(dr["Amount"]);
                modelBooking.VenueID = Convert.ToInt32(dr["VenueID"]);
                modelBooking.VenueName = Convert.ToString(dr["VenueName"]);
                modelBooking.ISBooked = Convert.ToBoolean(dr["ISBooked"]);
                modelBooking.BookingStartDate = Convert.ToDateTime(dr["BookingStartDate"]);
                modelBooking.BookingEndDate = Convert.ToDateTime(dr["BookingEndDate"]);
                modelBooking.NumOfDays = Convert.ToInt32(dr["NumberOfDays"]);
                
                modelBooking.PaymentAfterEvent = Convert.ToDecimal(dr["PaymentAfterEvent"]);
                modelBooking.PaymentStatus = Convert.ToString(dr["PaymentStatus"]);
                modelBooking.UserName = Convert.ToString(dr["UserName"]);
                modelBooking.ContactNO = Convert.ToString(dr["ContactNO"]);
                modelBooking.Email = Convert.ToString(dr["Email"]);
            }
            

            if(advancePayment != null)
            {
                modelBooking.AdvancePayment = advancePayment;
            }
            return View("PaymentPage", modelBooking);
        }
        [HttpPost]
        public IActionResult PaymentPage(BookingModel bookingModel)
        {
            
                dal.InsertPayment(bookingModel);
            DataTable dt = dal.PR_Booking_SelectByPK(bookingModel.BookingID);
            
            foreach (DataRow dr in dt.Rows)
            {
                bookingModel.BookingID = Convert.ToInt32(dr["BookingID"]);
                bookingModel.UserID = Convert.ToInt32(dr["UserID"]);
                bookingModel.Amount = Convert.ToDecimal(dr["Amount"]);
                bookingModel.VenueID = Convert.ToInt32(dr["VenueID"]);
                bookingModel.VenueName = Convert.ToString(dr["VenueName"]);
                bookingModel.ISBooked = Convert.ToBoolean(dr["ISBooked"]);
                bookingModel.BookingStartDate = Convert.ToDateTime(dr["BookingStartDate"]);
                bookingModel.BookingEndDate = Convert.ToDateTime(dr["BookingEndDate"]);
                bookingModel.NumOfDays = Convert.ToInt32(dr["NumberOfDays"]);
                
                     bookingModel.AdvancePayment = Convert.ToDecimal(dr["AdvancePayment"]);
                
                bookingModel.PaymentAfterEvent = Convert.ToDecimal(dr["PaymentAfterEvent"]);
                bookingModel.PaymentStatus = Convert.ToString(dr["PaymentStatus"]);
                bookingModel.UserName = Convert.ToString(dr["UserName"]);
                bookingModel.ContactNO = Convert.ToString(dr["ContactNO"]);
                bookingModel.Email = Convert.ToString(dr["Email"]);
            }


            if (bookingModel.PaymentID != null)
            {
                Console.WriteLine(bookingModel.VenueID);
                Console.WriteLine(bookingModel.VenueName);
                Console.WriteLine(bookingModel.BookingID);
                return View("Receipt",bookingModel);
            }
            else
            {

                return RedirectToAction("PaymentPage");
            }
        }
        public IActionResult BookingList()
        {
            int UserID = HttpContext.Session.GetInt32("UserID").Value;

            DataTable dt =dal.PR_Booking_SelectByUserID(UserID);
            

            return View("BookingList",dt);
            
            
        }
        public async Task<IActionResult> GeneratePdf(int BookingID,int PaymentID)
        {
            DataTable dt = dal.PR_Booking_SelectByPK(BookingID);
            BookingModel modelBooking = new BookingModel();
            foreach (DataRow dr in dt.Rows)
            {
                modelBooking.BookingID = Convert.ToInt32(dr["BookingID"]);
                modelBooking.UserID = Convert.ToInt32(dr["UserID"]);
                modelBooking.Amount = Convert.ToDecimal(dr["Amount"]);
                modelBooking.VenueID = Convert.ToInt32(dr["VenueID"]);
                modelBooking.VenueName = Convert.ToString(dr["VenueName"]);
                modelBooking.ISBooked = Convert.ToBoolean(dr["ISBooked"]);
                modelBooking.BookingStartDate = Convert.ToDateTime(dr["BookingStartDate"]);
                modelBooking.BookingEndDate = Convert.ToDateTime(dr["BookingEndDate"]);
                modelBooking.NumOfDays = Convert.ToInt32(dr["NumberOfDays"]);
                modelBooking.AdvancePayment = Convert.ToDecimal(dr["AdvancePayment"]);
                modelBooking.PaymentAfterEvent = Convert.ToDecimal(dr["PaymentAfterEvent"]);
                modelBooking.PaymentStatus = Convert.ToString(dr["PaymentStatus"]);
                modelBooking.UserName = Convert.ToString(dr["UserName"]);
                modelBooking.ContactNO = Convert.ToString(dr["ContactNO"]);
                modelBooking.Email = Convert.ToString(dr["Email"]);
            }
            DataTable dt1 = dal.PR_Payment_SelectByPK(PaymentID);
            foreach (DataRow dr in dt1.Rows)
            {
                modelBooking.PaymentID = Convert.ToInt32(dr["PaymentID"]);
                modelBooking.PaymentAmount = Convert.ToDecimal(dr["Amount"]);
                modelBooking.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);
            }
            // Render the view to HTML asynchronously without passing a model
            var viewHtml = await this.RenderViewAsync<object>("Pdf", modelBooking);

            // Use the HTML content from the view
            var converter = new BasicConverter(new PdfTools());
            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = {
            ColorMode = ColorMode.Color,
            Orientation = Orientation.Landscape,
            PaperSize = PaperKind.A4,
        },
                Objects = {
            new ObjectSettings {
                PagesCount = true,
                HtmlContent = viewHtml, // Use the dynamically generated HTML
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Footer" }
            }
        }
            };

            var pdfBytes = converter.Convert(doc);

            // Provide the generated PDF as a download
            return File(pdfBytes, "application/pdf", "my_generated_pdf.pdf");
        }








    }
    public static class ControllerExtensions
    {
        public static async Task<string> RenderViewAsync<TModel>(this Controller controller, string viewName, TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.ActionDescriptor.ActionName;
            }

            controller.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);

                if (viewResult.Success == false)
                {
                    return $"A view with the name {viewName} could not be found";
                }

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }

}



