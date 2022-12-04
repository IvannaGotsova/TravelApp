using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Core.Contracts;

namespace TravelApp.Controllers
{
    [Authorize]
    public class JourneysController : Controller
    {
        private readonly IJourneyService journeyService;

        public JourneysController(IJourneyService journeyService)
        {
            this.journeyService = journeyService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                var journeys = await journeyService
                .GetAllJourneys();

                return View(journeys);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            if (await journeyService
                .GetJourneyDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var journeyModel = await journeyService
               .GetJourneyDetailsById(id);

                return View(journeyModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

    }
}
