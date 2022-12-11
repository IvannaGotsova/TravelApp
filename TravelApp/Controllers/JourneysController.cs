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
    /// <summary>
    /// Shows all journeys and details about them.
    /// </summary>
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
        /// <summary>
        /// This method returns all the available journeys.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                //var journeys = await journeyService
                //.GetAllJourneys();

                //creates cache
                var journeys = this.memoryCache
                   .Get<IEnumerable<AllJourneysModel>>(JourneyCacheKey);

                journeys ??= await journeyService
                        .GetAllJourneys();

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
        /// <summary>
        /// This method returns a details about journey with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            //check if the journey is null
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
