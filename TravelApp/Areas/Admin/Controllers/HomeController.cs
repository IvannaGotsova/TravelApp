using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace TravelApp.Areas.Admin.Controllers
{
    /// <summary>
    /// Redirects to the Admin Controller, Index Action.
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
        }

    }
}
