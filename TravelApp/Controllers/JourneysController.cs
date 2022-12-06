using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.JourneyModels;
using static TravelApp.Constants.CacheConstants;

namespace TravelApp.Controllers
{
    [Authorize]
    public class JourneysController : Controller
    {
        private readonly IJourneyService journeyService;
        private readonly IMemoryCache memoryCache;
        public JourneysController(IJourneyService journeyService, 
                                  IMemoryCache memoryCache)
        {
            this.journeyService = journeyService;
            this.memoryCache = memoryCache;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                //var journeys = await journeyService
                //.GetAllJourneys();

                var journeys = this.memoryCache
                   .Get<IEnumerable<AllJourneysModel>>(JourneyCacheKey);

                if (journeys == null)
                {
                    journeys = await journeyService
                        .GetAllJourneys();
                }

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                this.memoryCache.Set(JourneyCacheKey, journeys, cacheOptions);

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

                this.memoryCache.Remove(JourneyCacheKey);


                return View(journeyModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

    }
}
