using System.ComponentModel.DataAnnotations;

namespace LibraryWebApplication.Models
{
    public class CreateBookModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
    }
}
