﻿using LibraryBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LibraryCommon;
using LibraryWebApplication.Models;
using Microsoft.AspNetCore.Http;
using System.Web;
using System;

namespace LibraryWebApplication.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            // Get books and convert them to BookModel objects
            List<Book> books = BooksBL.GetBooks();
            List<BookModel> bookModels = new List<BookModel>();

            if (books == null) { return View(); }

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

        public IActionResult Checkout(int id)
        {
            BooksBL.CheckoutBook(id, (int)HttpContext.Session.GetInt32("UserID"));
            return RedirectToAction("Index");
        }

        public IActionResult Reserve(int id)
        {
            BooksBL.HoldBook(id, (int)HttpContext.Session.GetInt32("UserID"));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(CreateBookModel m)
        {
            BooksBL.CreateBook(m.Title, m.Author);
            return RedirectToAction("Index");
        }

        public IActionResult Search(IFormCollection form)
        {
            // Get books and convert them to BookModel objects
            List<Book> books = BooksBL.SearchBooks(form["expr"]);
            List<BookModel> bookModels = new List<BookModel>();

            if (books == null) { return RedirectToAction("Index"); }

            foreach (Book book in books)
            {
                bookModels.Add(Mapper.BookToBookModel(book));
            }

            return View("Index", bookModels);
        }

        [HttpGet]
        public IActionResult EditBook(int id)
        {
            EditBookModel bm = Mapper.BookToEditBookModel(BooksBL.GetBookByID(id));

            return View(bm);
        }

        [HttpPost]
        public IActionResult EditBook(EditBookModel bm)
        {
            BooksBL.EditBook(bm.BookID, bm.Title, bm.Author);

            return RedirectToAction("Index");
        }
    }   
}
