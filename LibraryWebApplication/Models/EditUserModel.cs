using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models
{
    public class EditUserModel
    {
        public int UserID { get; set; }
        [Range(2, 4)]
        public int RoleID { get; set; }
        [Required]
        [EmailAddress]
        [Remote(action: "VerifyEmail", controller: "Dashboard")]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
