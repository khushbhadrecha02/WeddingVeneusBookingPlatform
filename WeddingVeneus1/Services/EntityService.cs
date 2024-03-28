using MailKit.Net.Smtp;
using MimeKit;
using System.Data;
using WeddingVeneus1.Areas.State.Models;
using WeddingVeneus1.DAL;

namespace WeddingVeneus1.Services
{
    public class EntityService
    {

        public void RejectEntities<TDal>(int[] entityIds) where TDal : DAL_Helpers, new()
        {
            TDal dal = new TDal();

            foreach (var entityId in entityIds)
            {
                dal.RejectEntity(entityId);
                DataTable dt = dal.SelectUserIDByEntityID(entityId);

                foreach (DataRow dr in dt.Rows)
                {
                    // Assume you have a method to create and populate a model from a DataRow
                    string EntityType = Convert.ToString(dr["EntityType"]);
                    var model = CreateModelFromDataRow(dr,EntityType);
                    SendEmail(model,EntityType);
                }
            }
        }

        private void SendEmail(EmailModel model,string entityType)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(new MailboxAddress("Khush Bhadrecha", "khushbhadrecha02@gmail.com"));
                
                
                    email.To.Add(new MailboxAddress(model.UserName, model.Email));
                if (entityType == "state")
                {
                    email.Subject = "Regarding your request for adding new state.";
                    email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                    {
                        Text = "Hey " + model.UserName + "your request for adding state named " + model.Name + " had been rejected by Mandap.com. "
                    };
                }
                else if (entityType == "city")
                {
                    email.Subject = "Regarding your request for adding new city.";
                    email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                    {
                        Text = "Hey " + model.UserName + "your request for adding city named " + model.Name + " had been rejected by Mandap.com. "
                    };
                }
                else if (entityType == "category")
                {
                    email.Subject = "Regarding your request for adding new category.";
                    email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                    {
                        Text = "Hey " + model.UserName + "your request for adding category named " + model.Name + " had been rejected by Mandap.com. "
                    };
                }
                else if (entityType == "venue")
                {
                    email.Subject = "Regarding your request for adding new venue.";
                    email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
                    {
                        Text = "Hey " + model.UserName + "your request for adding venue named " + model.Name + " had been rejected by Mandap.com. "
                    };
                }







                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, false);

                    // Note: only needed if the SMTP server requires authentication
                    smtp.Authenticate("khushbhadrecha02@gmail.com", "evasrxlbzwmuogsr");

                    smtp.Send(email);
                    smtp.Disconnect(true);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //return null;
            }
        }

        private EmailModel CreateModelFromDataRow(DataRow dr, string entityType)
        {
            // Create and return your model based on the DataRow
            EmailModel model = new EmailModel();
            if (entityType == "state")
            {
                model.Name = Convert.ToString(dr["StateName"]);
            }
            else if (entityType == "city")
            {
                model.Name = Convert.ToString(dr["CityName"]);
            }
            else if(entityType == "category")
            {
                model.Name = Convert.ToString(dr["CategoryName"]);
            }
            else if (entityType == "venue")
            {
                model.Name = Convert.ToString(dr["VenueName"]);
            }
            model.Email = Convert.ToString(dr["Email"]);
            model.UserName = Convert.ToString(dr["UserName"]);
            

            return model;
        }



    }
    public class EmailModel
    {
        public string? Email { get; set; }
        public int? UserID { get; set; }
        
        public string? Name { get; set; }
        public string? UserName { get; set; }

    }
}
