using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models
{
	public class RegisterModel
	{
        [Required]
        [EmailAddress]
        [Remote(action:"VerifyEmail", controller:"Home")]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
