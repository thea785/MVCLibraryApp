﻿using LibraryBusinessLogic;
using LibraryWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApplication.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Dashboard/Dashboard.cshtml", Mapper.GetDashboardModel());
        }

        public IActionResult DeleteUser(int id)
        {
            UsersBL.DeleteUser(id);

            return RedirectToAction("Index");
        }

        public IActionResult Return(int id)
        {
            BooksBL.ReturnBook(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditUser(string email)
        {
            EditUserModel um = Mapper.UserToUserModel(UsersBL.GetUserByEmail(email));

            return View(um);
        }

        [HttpPost]
        public IActionResult EditUser(EditUserModel m)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            UsersBL.EditUser(m.UserID, m.RoleID, m.Email, m.FirstName, m.LastName);
            return RedirectToAction("Index");
        }
    }
}
