using LibraryBusinessLogic;
using LibraryCommon;
using LibraryWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LibraryWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel m)
        {
            if (!ModelState.IsValid)
                return View();

            // Create user in the database
            int userID = UsersBL.CreateUser(2, m.Email, m.FirstName, m.LastName, m.Password);

            // Set session for new user
            HttpContext.Session.SetInt32("UserID", userID);
            HttpContext.Session.SetInt32("RoleID", 2);

            // Go to index
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            // Set session to Guest
            HttpContext.Session.SetInt32("UserID", 0);
            HttpContext.Session.SetInt32("RoleID", 1);
            // Go to index
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
