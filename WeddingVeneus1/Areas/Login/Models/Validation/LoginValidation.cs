using System.ComponentModel.DataAnnotations;
using System.Data;
using WeddingVeneus1.Areas.Booking.Models;
using WeddingVeneus1.DAL;

namespace WeddingVeneus1.Areas.Login.Models.Validation
{
    public class Unique : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string email = Convert.ToString(value);
                Login_DALBase dal = new Login_DALBase();
                DataTable dt = dal.PR_Login_CheckUniqueConstraint(email);
                if (dt.Rows.Count == 0)
                {
                    return ValidationResult.Success;
                }
            }  
            return new ValidationResult("Email is already registered");


        }

    }
}
