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
    /// <summary>
    /// Shows all towns and details about them.
    /// </summary>
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
        /// <summary>
        /// This method returns all the available towns.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            //var towns = await townService
            //    .GetAllTowns();

            //creates cache
            var towns = this.memoryCache
                   .Get<IEnumerable<AllTownsModel>>(TownCacheKey);

            towns ??= await townService
                    .GetAllTowns();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

            this.memoryCache.Set(TownCacheKey, towns, cacheOptions);

            return View(towns);
        }
        /// <summary>
        /// This method returns a details about town with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            //check if town is null
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
