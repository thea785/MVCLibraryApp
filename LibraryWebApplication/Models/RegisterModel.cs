using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models
{
	public class RegisterModel
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
