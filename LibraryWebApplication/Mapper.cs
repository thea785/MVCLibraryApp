using LibraryWebApplication.Models;
using LibraryCommon;

namespace LibraryWebApplication
{
    public static class Mapper
    {
        public static BookModel BookToBookModel(Book b)
        {
            return new BookModel()
            {
                BookID = b.BookID,
                Title = b.Title,
                Author = b.Author,
                CheckedOutBy = b.CheckedOutBy,
                OnHoldBy = b.OnHoldBy
            };
        }
    }
}
