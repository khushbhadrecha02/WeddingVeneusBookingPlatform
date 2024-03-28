using Microsoft.AspNetCore.Mvc;
using System.Data;
using WeddingVeneus1.Areas.Booking.Models;
using WeddingVeneus1.Areas.VenueDetails.Models;
using WeddingVeneus1.DAL;
using DinkToPdf;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using MailKit.Net.Smtp;
using WeddingVeneus1.CF;


namespace WeddingVeneus1.Areas.Booking.Controllers
{
    [Area("Booking")]
    [Route("Booking/{Controller}/{Action}")]
    
    #region BookingController
    public class BookingController : Microsoft.AspNetCore.Mvc.Controller
    {
        #region GloblaStateDalObject
        Booking_DALBase dal = new Booking_DALBase();
        VenueDetails_DALBase dal1 = new VenueDetails_DALBase();


        #endregion

        #region CheckBookingView
        [CheckAccess]
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
                return View("CheckBooking", booking_ViewModel1);

            }
            

        
        #endregion

        #region CheckBookingStatus
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
        #endregion

        #region CalculateAmount
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
        #endregion

        #region CalculateAdvancePaymentPercentage
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
        #endregion

        #region CalculateAdvancePayment
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
        #endregion

        #region Create
        [Microsoft.AspNetCore.Mvc.HttpPost]
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
        #endregion

        #region PaymentPage
        public IActionResult PaymentPage(int bookingID,decimal? advancePayment,int? CancelID)
        {
            Console.WriteLine(CancelID);
           DataTable dt =  dal.PR_Booking_SelectByPK(bookingID);
            BookingModel modelBooking = new BookingModel();
            foreach (DataRow dr in dt.Rows)
            {
                modelBooking.BookingID = Convert.ToInt32(dr["BookingID"]);
                
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
            modelBooking.UserID = HttpContext.Session.GetInt32("UserID").Value;


            if (CancelID != null)
            {
                modelBooking.CancelID = CancelID;
            }
            if(advancePayment != null)
            {
                modelBooking.AdvancePayment = advancePayment;
            }
            return View("PaymentPage", modelBooking);
        }
        #endregion

        #region PaymentPagePostMethod
        [HttpPost]
        public IActionResult PaymentPage(BookingModel bookingModel)
        {
            if(bookingModel.CancelID != null)
            {
                dal.InsertPaymentRefunded(bookingModel);
            }
            else
            {
                dal.InsertPayment(bookingModel);
            }
            
                
            DataTable dt = dal.PR_Payment_SelectByPK(bookingModel.PaymentID);
            
            foreach (DataRow dr in dt.Rows)
            {
                bookingModel.BookingID = Convert.ToInt32(dr["BookingID"]);
                bookingModel.UserID = Convert.ToInt32(dr["UserID"]);
                bookingModel.Amount = Convert.ToDecimal(dr["Amount"]);


                bookingModel.BookingStartDate = Convert.ToDateTime(dr["BookingStartDate"]);
                bookingModel.BookingEndDate = Convert.ToDateTime(dr["BookingEndDate"]);
                bookingModel.NumOfDays = Convert.ToInt32(dr["NumberOfDays"]);
                
                     bookingModel.AdvancePayment = Convert.ToDecimal(dr["AdvancePayment"]);
                
                bookingModel.PaymentAfterEvent = Convert.ToDecimal(dr["PaymentAfterEvent"]);
                bookingModel.PaymentAmount = Convert.ToDecimal(dr["PaymentAmount"]);

                bookingModel.UserName = Convert.ToString(dr["UserName"]);
                bookingModel.ContactNO = Convert.ToString(dr["ContactNO"]);
                bookingModel.Email = Convert.ToString(dr["Email"]);
            }
            DataTable dt1 = dal.PR_Booking_SelectByPK(bookingModel.BookingID);
            foreach(DataRow dr in  dt1.Rows)
            {
                bookingModel.VenueName = Convert.ToString(dr["VenueName"]);
                bookingModel.Receiver = Convert.ToString(dr["UserName"]);
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
        #endregion

       

        #region ComboBox
        public void PopulateDropdownLists()
        {
            DataTable dt = dal1.PR_MST_Venue_SelectByComboBox();
            List<VenueDropdownModel> list = new List<VenueDropdownModel>();
            foreach (DataRow dr in dt.Rows)
            {
                VenueDropdownModel vlst = new VenueDropdownModel();
                vlst.VenueID = Convert.ToInt32(dr["VenueID"]);
                vlst.VenueName = Convert.ToString(dr["VenueName"]);
                list.Add(vlst);

            }
            ViewBag.VenueList = list;
        }
            #endregion

        #region Search
            public IActionResult Search(Booking_ViewModel1 booking_ViewModel, string? submit,int? VenueID)
        {
            
            int? UserID = null;
            int? VenueID1 = null;
            if(VenueID != null)
            {
                VenueID1 = VenueID;
            }

            if (HttpContext.Session.GetString("Role") == "User")

            {

                int UserID1 = HttpContext.Session.GetInt32("UserID").Value;
                UserID = UserID1;

                Console.WriteLine(UserID);
            }
            if (submit != null)
            {
                booking_ViewModel.booking_SearchModel.SubmitType = submit;
            }
            DataTable dt = dal.PR_Booking_SelectByPage(booking_ViewModel.booking_SearchModel, UserID,VenueID1);
            var viewModel = new Booking_ViewModel1()
            {

                bookingList = dt,
                booking_SearchModel = new Booking_SearchModel(),
            };
            PopulateDropdownLists();            

            return View("BookingList", viewModel);
        }
        #endregion

        #region CancelBooking
        public IActionResult CancelBooking(int BookingID)
        {

            DataTable dt = dal.PR_Booking_SelectByPK(BookingID);
            CancelModel bookingModel = new CancelModel();

            foreach (DataRow dr in dt.Rows)
            {
                    
                bookingModel.BookingID = Convert.ToInt32(dr["BookingID"]);
                bookingModel.VenueID = Convert.ToInt32(dr["VenueID"]);
                
                
                bookingModel.VenueName = Convert.ToString(dr["VenueName"]);
                
                bookingModel.BookingStartDate = Convert.ToDateTime(dr["BookingStartDate"]);
                bookingModel.BookingEndDate = Convert.ToDateTime(dr["BookingEndDate"]);
                bookingModel.NumOfDays = Convert.ToInt32(dr["NumberOfDays"]);

               
                bookingModel.CancellationPolicy = Convert.ToDecimal(dr["CancellationPolicy"]);
                bookingModel.UserID = Convert.ToInt32(dr["UserID"]);
                
            }



            return View("CancelBooking",bookingModel);


        }
        #endregion

        #region CancellationPolicy
        [HttpPost]
        public IActionResult CancellationPolicy(CancelModel bookingModel)
        {
            Console.WriteLine(ModelState.IsValid);
            if(ModelState.IsValid)
            {
                dal.InsertCancelBookingRequest(bookingModel);
                return RedirectToAction("CancelList");
            }
            else
            {
                return View("CancelBooking");
            }
        }
        #endregion

        #region CancelList
        public IActionResult CancelList()
        {
            int UserID = HttpContext.Session.GetInt32("UserID").Value;
            DataTable dt = dal.PR_CancelBooking_SelectByUserID(UserID);
            return View("CancelList", dt);

        }
        #endregion

        #region CancelListByVenueID
        public IActionResult CancelListByVenueID(int venueID)
        {
            
            DataTable dt = dal.PR_CancelBooking_SelectByVenueID(venueID);
            return View("CancelListByVenueID", dt);

        }
        #endregion
        #region RejectCancelRequest
        public IActionResult RejectCancelRequest(int CancelID)
        {
                BookingModel bookingModel = new BookingModel();
            bookingModel.CancelID = CancelID;
              dal.PR_CancelBooking_RejectCancelBooking(bookingModel);
            return RedirectToAction("CancelListByVenueID", new { venueID = bookingModel.VenueID });


        }
        #endregion

        #region GeneratePdf
        public async Task<IActionResult> GeneratePdf(int BookingID,int PaymentID,string? flag)
        {
            
            

            DataTable dt = dal.PR_Payment_SelectByPK(PaymentID);
            BookingModel bookingModel = new BookingModel();
            foreach (DataRow dr in dt.Rows)
            {
                bookingModel.BookingID = Convert.ToInt32(dr["BookingID"]);
                bookingModel.UserID = Convert.ToInt32(dr["UserID"]);
                bookingModel.Amount = Convert.ToDecimal(dr["Amount"]);
                bookingModel.PaymentID = Convert.ToInt32(dr["PaymentID"]);
                bookingModel.PaymentDate = Convert.ToDateTime(dr["PaymentDate"]);

                bookingModel.BookingStartDate = Convert.ToDateTime(dr["BookingStartDate"]);
                bookingModel.BookingEndDate = Convert.ToDateTime(dr["BookingEndDate"]);
                bookingModel.NumOfDays = Convert.ToInt32(dr["NumberOfDays"]);

                bookingModel.AdvancePayment = Convert.ToDecimal(dr["AdvancePayment"]);

                bookingModel.PaymentAfterEvent = Convert.ToDecimal(dr["PaymentAfterEvent"]);
                bookingModel.PaymentAmount = Convert.ToDecimal(dr["PaymentAmount"]);

                bookingModel.UserName = Convert.ToString(dr["UserName"]);
                bookingModel.ContactNO = Convert.ToString(dr["ContactNO"]);
                bookingModel.Email = Convert.ToString(dr["Email"]);
            }
            DataTable dt1 = dal.PR_Booking_SelectByPK(BookingID);
            foreach (DataRow dr in dt1.Rows)
            {
                bookingModel.VenueName = Convert.ToString(dr["VenueName"]);
                bookingModel.Receiver = Convert.ToString(dr["UserName"]);
                bookingModel.ReceiverEmail = Convert.ToString(dr["Email"]);
                bookingModel.VenueID = Convert.ToInt32(dr["VenueID"]);
            }
            DataTable dt3 = dal1.PR_VenueDetails_SelectByPK(bookingModel.VenueID);
            foreach (DataRow dr in dt3.Rows)
            {
                
                bookingModel.VenueOwner = Convert.ToString(dr["VenueOwner"]);
                bookingModel.VenueOwnerEmail = Convert.ToString(dr["VenueOwnerEmail"]);
                bookingModel.UserID = Convert.ToInt32(dr["VenueOwnerID"]);
            }

            var VIEWHTML = "khush";
            // Render the view to HTML asynchronously without passing a model
            if(flag != null)
            {
                var viewHtml = await this.RenderViewAsync<object>("Pdf", bookingModel);
                VIEWHTML = viewHtml;
            }
            else
            {
                var viewHtml = await this.RenderViewAsync<object>("Pdf1", bookingModel);
                VIEWHTML = viewHtml;


            }

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
                HtmlContent = VIEWHTML, // Use the dynamically generated HTML
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Footer" }
            }
        }
            };

            var pdfBytes = converter.Convert(doc);
            string uploadFolder = "wwwroot/assets/upload";
            string path = Path.Combine(Directory.GetCurrentDirectory(), uploadFolder);

            // Create the folder if it doesn't exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Generate a unique file name for the PDF
            string fileName = $"generated_pdf_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Path.Combine(path, fileName);

            // Save the generated PDF to the specified folder
            System.IO.File.WriteAllBytes(filePath, pdfBytes);
            var pdfFilePath = "wwwroot/assets/upload/"+fileName;
            var pdfAttachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(System.IO.File.OpenRead(pdfFilePath), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(pdfFilePath)
            };

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            if (flag != null)
            {
                builder.TextBody = @"Thank you for choosing"+ " " + bookingModel.VenueName + "for your special day! We're excited to be a part of your wedding celebration.";
            }
            else
            {
                builder.TextBody = @"We hope this email finds you well. We would like to inform you that a refund has been processed for your booking at" + " " + bookingModel.VenueName + ".";

            }


            // Attach the PDF file to the email
            builder.Attachments.Add(pdfAttachment);
            InternetAddressList list = new InternetAddressList();
            list.Add(new MailboxAddress(bookingModel.VenueOwner, bookingModel.VenueOwnerEmail));
            list.Add(new MailboxAddress(bookingModel.Receiver, bookingModel.ReceiverEmail));
            

            // Construct the message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Khush Bhadrecha", "khushbhadrecha02@gmail.com"));
            message.To.AddRange(list);
            if (flag != null)
            {
                message.Subject = "Booking Receipt for your Wedding Venue" + " " + bookingModel.VenueName;
            }
            else
            {
                message.Subject = "Refund Receipt for your booking at" + " " + bookingModel.VenueName;

            }


            message.Body = builder.ToMessageBody();

            // Send the email
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("khushbhadrecha02@gmail.com", "evasrxlbzwmuogsr");
                client.Send(message);
                client.Disconnect(true);
            }

            // Provide the generated PDF as a download
            return File(pdfBytes, "application/pdf", fileName);
        }
        #endregion

        








    }
    #endregion

    #region ControllerExtensions
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
    #endregion

}



