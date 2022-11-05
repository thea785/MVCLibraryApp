namespace LibraryWebApplication.Models
{
    public class BookModel
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int CheckedOutBy { get; set; }
        public int OnHoldBy { get; set; }
    }
}
