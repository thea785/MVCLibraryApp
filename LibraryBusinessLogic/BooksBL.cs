using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryCommon;
using LibraryData;

namespace LibraryBusinessLogic
{
    public static class BooksBL
    {
        public static List<Book> GetBooks()
        {
            return BooksData.GetBooks();
        }

        public static Book GetBookByID(int bookID)
        {
            return BooksData.GetBooks().FirstOrDefault(b => b.BookID == bookID);
        }

        public static void CheckoutBook(int bookID, int userID)
        {
            BooksData.CheckoutBook(bookID, userID);
        }

        public static void ReturnBook(int bookID)
        {
            BooksData.ReturnBook(bookID);
        }

        public static void HoldBook(int bookID, int userID)
        {
            BooksData.HoldBook(bookID, userID);
        }

        public static void CreateBook(string title, string author)
        {
            BooksData.CreateBook(title, author);
        }

        public static void DeleteBook(int bookID)
        {
            BooksData.DeleteBook(bookID);
        }

        public static List<Book> SearchBooks(string searchExpression)
        {
            List<Book> books = BooksData.GetBooks();
            List<Book> searchResult = new List<Book>();
            foreach(Book b in books)
            {
                if (b.Title.Contains(searchExpression) || b.Author.Contains(searchExpression))
                searchResult.Add(b);
            }
            return searchResult;
        }

        public static List<Book> GetBooksRelatedToUser(int userID)
        {
            List<Book> books = BooksData.GetBooks();
            List<Book> result = new List<Book>();

            foreach (Book b in books)
            {
                if (b.CheckedOutBy == userID || b.OnHoldBy == userID)
                    result.Add(b);
            }
            return result;
        }

        public static void EditBook(int bookID, string title, string author)
        {
            BooksData.EditBook(bookID, title, author);
        }
    }
}
