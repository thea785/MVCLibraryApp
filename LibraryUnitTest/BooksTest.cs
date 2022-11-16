using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData;
using System.Linq;
using LibraryBusinessLogic;

namespace LibraryUnitTest
{
    [TestClass]
    public class BooksTest
    {
        private const string hash = "SM3wWClN1KFFBCm9GLMs9m6lJ4yzNQ8zVfAntSym1Xty1wMBDZmA9OiE1IB9ovDruRP8fW44hrpQ8Ca6W9JmQPhl0MA4uDsluQVrhQyMN0FexhgKvH9eCAqz4G88GfM5gqS+G/M6myVUlzKi6KwWc/B73IcJiPN2tnV8+neY0wkAMLz/yIJ6b8vqOYoziF+vWRsXnSDOw1Q/PKCcnKZYtFa7B2NI1dFW/uOHo8HpM0rNEyMOKsvHp0lv1rIuA8VIsamJ/UJ5hmNwdSvE5Vd4428KjDTK2S23xTjwZgeugzb6ac9k7fVveoJkkER6NG5W/vizNlUHSWdFAKy0kSz11g==";
        private const string salt = "GtB5unQeau/QPTip/Q27b9g0ut5avFacTLsM/rnUZfzslM9LU0Wnk5CdYAr4rzQiSk6gIy74gOOmiennFu4jIQ==";
        [TestMethod]
        public void CreateAndDeleteBookTest()
        {
            // Create a book
            int _bookID = BooksData.CreateBook("1TestTitle", "1TestAuthor");

            // Check if it is returned by GetBooks
            Assert.IsTrue(BooksData.GetBooks().Any(b => b.Title == "1TestTitle"));

            // Delete the book and check if it is returned by GetBooks
            BooksData.DeleteBook(_bookID);
            Assert.IsFalse(BooksData.GetBooks().Any(b => b.Title == "1TestTitle"));
        }

        [TestMethod]
        public void CheckoutAndReturnBookTest()
        {
            // Create user and book
            int _bookID = BooksData.CreateBook("2TestTitle", "2TestAuthor");
            int _userID = UsersData.CreateUser(2, "test4@gmail.com", "4Fname", "4Lname", hash, salt);

            // Checkout the book and test if it is returned by GetBooks
            BooksData.CheckoutBook(_bookID, _userID);
            Assert.IsTrue(BooksData.GetBooks().Any(b => b.Title == "2TestTitle" && b.CheckedOutBy == _userID));

            // Return the book and test if it is returned by GetBooks
            BooksData.ReturnBook(_bookID);
            Assert.IsFalse(BooksData.GetBooks().Any(b => b.Title == "2TestTitle" && b.CheckedOutBy == _userID));

            // Delete the user and book
            BooksData.DeleteBook(_bookID);
            UsersData.DeleteUser(_userID);
        }

        [TestMethod]
        public void SearchBooksTest()
        {
            // Create a book
            int _bookID = BooksData.CreateBook("3TestTitle", "3TestAuthor");

            // Search by title
            Assert.IsTrue(BooksBL.SearchBooks("3TestTi").Any(b => b.BookID == _bookID));

            // Search by author
            Assert.IsTrue(BooksBL.SearchBooks("3TestAuth").Any(b => b.BookID == _bookID));

            // Incorrect Search
            Assert.IsFalse(BooksBL.SearchBooks("3TestIncorrect").Any(b => b.BookID == _bookID));

            // Delete the book
            BooksData.DeleteBook(_bookID);
        }

        [TestMethod]
        public void GetBooksRelatedToUserTest()
        {
            // Create user and book
            int _bookID = BooksData.CreateBook("4TestTitle", "4TestAuthor");
            int _userID = UsersData.CreateUser(2, "test5@gmail.com", "5Fname", "5Lname", hash, salt);

            // Checkout the book
            BooksData.CheckoutBook(_bookID, _userID);

            // Test if the book is returned by GetBooksRelatedToUser
            Assert.IsTrue(BooksBL.GetBooksRelatedToUser(_userID).Any(b => b.BookID == _bookID));

            // Delete the user and book
            BooksData.DeleteBook(_bookID);
            UsersData.DeleteUser(_userID);
        }

        [TestMethod]
        public void EditBookTest()
        {
            // Create a book
            int _bookID = BooksData.CreateBook("7TestTitle", "7TestAuthor");

            // Edit the book title and author
            BooksBL.EditBook(_bookID, "8TestTitle", "8TestAuthor");

            // Check if the book was edited
            Assert.AreEqual(BooksBL.GetBookByID(_bookID).Title, "8TestTitle");
            Assert.AreEqual(BooksBL.GetBookByID(_bookID).Author, "8TestAuthor");

            // Delete the book
            BooksData.DeleteBook(_bookID);
        }
    }
}