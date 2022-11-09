using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData;
using System.Linq;

namespace LibraryUnitTest
{
    [TestClass]
    public class BooksDataTest
    {
        [TestMethod]
        public void CreateBookTest()
        {
            int bookID = BooksData.CreateBook("1TestTitle", "1TestAuthor");
            Assert.IsTrue(BooksData.GetBooks().Any(b => b.Title == "1TestTitle"));
            BooksData.DeleteBook(bookID);
            Assert.IsFalse(BooksData.GetBooks().Any(b => b.Title == "1TestTitle"));
        }

        [TestMethod]
        public void CheckoutBookTest()
        {
            int bookID = BooksData.CreateBook("2TestTitle", "2TestAuthor");
        }
    }
}