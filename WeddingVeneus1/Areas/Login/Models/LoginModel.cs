using System.ComponentModel.DataAnnotations;
using System.Data;
using WeddingVeneus1.Areas.Login.Models.Validation;

namespace WeddingVeneus1.Areas.Login.Models
{
    public class LoginModel
    {
        
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Unique]
        public string Email { get; set; }
        [Required]
        public int RoleID { get; set; }
        public string? ContactNO { get; set; }
        public IFormFile? File { get; set; }

        public string? PhotoPath { get; set; }
    }
    public class RoleModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
    public class AdminDashboardViewModel
    {
        public DataTable StateCount { get; set; }
        public DataTable CityCount { get; set; }
        public DataTable CategoryCount { get; set; }
        public DataTable VenueCount { get; set; }
        public DataTable UnconfirmedStateCount { get; set; }
        public DataTable UnconfirmedCityCount { get; set; }
        public DataTable UnconfirmedCategoryCount { get; set; }
        public DataTable UnconfirmedVenueCount { get; set; }
        public DataTable ApproveAdminAccessCount { get; set; }
    }
    public class LoginModelForDisplay
    {
        public int? UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        public string? RoleName { get; set; }
        
        
        public string? Email { get; set; }
        [Required]
        public string? ContactNO { get; set; }
        public string? PhotoPath { get; set; }
    }
    public class UpdatePassword
    {
        public int? UserID { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ReEnterNewPassword{ get; set; }
        public string? Status { get; set; }
    }
    public class UpdatePhoto
    {
        public string? PhotoPath { get; set; }
        public IFormFile? File { get; set; }
        public int UserID { get; set; }
    }
    public class UpdateEmail
    {
        public string? Email { get; set; }
        [Required]
        [EmailAddress]
        [Unique]
        public string? UpdatedEmail { get; set; }
        public int? UserID { get; set; }
    }
    public class LoginViewModel
    {
        public LoginModelForDisplay loginModelForDisplay { get; set; }
        public UpdatePassword updatePassword { get; set; }
        public UpdatePhoto updatePhoto { get; set;}
        public UpdateEmail updateEmail { get; set; }
    }
}
