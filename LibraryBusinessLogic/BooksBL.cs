using System;
using System.Collections.Generic;
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
    }
}
