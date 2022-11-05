using LibraryBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LibraryCommon;
using LibraryWebApplication.Models;

namespace LibraryWebApplication.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            // Get books and convert them to BookModel objects
            List<Book> books = BooksBL.GetBooks();
            List<BookModel> bookModels = new List<BookModel>();
            foreach (Book book in books)
            {
                bookModels.Add(Mapper.BookToBookModel(book));
            }


            return View(bookModels);
        }

        public IActionResult Delete(int id)
        {
            BooksBL.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
