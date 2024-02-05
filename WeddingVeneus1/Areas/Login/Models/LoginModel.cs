using System.ComponentModel.DataAnnotations;

namespace WeddingVeneus1.Areas.Login.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int RoleID { get; set; }
        public string ContactNO { get; set; }
        public IFormFile? File { get; set; }

        public string PhotoPath { get; set; }
    }
    public class RoleModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}
