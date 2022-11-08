using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApplication.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Dashboard/Dashboard.cshtml");
        }
    }
}
