using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;

namespace TravelApp.Controllers
{
    [Authorize]
    public class TownsController : Controller
    {
        private readonly ITownService townService;

        public TownsController (ITownService townService)
        {
            this.townService = townService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var towns = await townService
                .GetAllTowns();

            return View(towns);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (await townService
                .GetTownDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var townModel = await townService
                .GetTownDetailsById(id);

                return View(townModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }


    }
}
