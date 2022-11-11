using LibraryWebApplication.Models;
using LibraryCommon;
using System.Collections.Generic;
using LibraryBusinessLogic;
using System.Net;

namespace LibraryWebApplication
{
    public static class Mapper
    {
        public static EditUserModel UserToUserModel(User u)
        {
            return new EditUserModel()
            {
                UserID = u.UserID,
                RoleID = u.RoleID,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName
            };
        }

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
        public static DashboardModel GetDashboardModel()
        {
            DashboardModel dm = new DashboardModel();
            dm.users = new List<DisplayUserModel>();
            dm.books = new List<BookModel>();

            List<User> userList = UsersBL.GetUsers();
            foreach(User u in userList)
            {
                dm.users.Add(new DisplayUserModel()
                {
                    UserID = u.UserID,
                    RoleID = u.RoleID,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                });
            }

            List<Book> bookList = BooksBL.GetBooks();
            foreach(Book b in bookList)
            {
                dm.books.Add(new BookModel()
                {
                    BookID=b.BookID,
                    Title=b.Title,
                    Author=b.Author,
                    CheckedOutBy=b.CheckedOutBy,
                    OnHoldBy=b.OnHoldBy
                });
            }

            return dm;
        }
    }
}
