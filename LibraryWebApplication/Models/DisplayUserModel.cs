using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models
{
    public class DisplayUserModel
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
