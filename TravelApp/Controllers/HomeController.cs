using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    /// <summary>
    /// Controls Index page, Privacy page, Error page.
    /// </summary>
    public class HomeController : Controller
    {
       /// <summary>
       /// This method returns index page.
       /// </summary>
       /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// This method returns privacy page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// This method returns error page.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}