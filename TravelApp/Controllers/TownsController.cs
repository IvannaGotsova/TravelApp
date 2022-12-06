using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Models.JourneyModels;
using TravelApp.Data.Models.TownModels;
using static TravelApp.Constants.CacheConstants;

namespace TravelApp.Controllers
{
    [Authorize]
    public class TownsController : Controller
    {
        private readonly ITownService townService;
        private readonly IMemoryCache memoryCache;
        public TownsController (ITownService townService, 
                                IMemoryCache memoryCache)
        {
            this.townService = townService;
            this.memoryCache = memoryCache;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            //var towns = await townService
            //    .GetAllTowns();

            var towns = this.memoryCache
                   .Get<IEnumerable<AllTownsModel>>(TownCacheKey);

            if (towns == null)
            {
                towns = await townService
                    .GetAllTowns();
            }

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            this.memoryCache.Set(TownCacheKey, towns, cacheOptions);

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

                this.memoryCache.Remove(TownCacheKey);

                return View(townModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }


    }
}
