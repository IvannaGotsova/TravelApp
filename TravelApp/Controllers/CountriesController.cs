using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Models.CountryModels;
using static TravelApp.Constants.CacheConstants;

namespace TravelApp.Controllers
{
    /// <summary>
    /// Shows all countries and details about them.
    /// </summary>
    [Authorize]
    public class CountriesController : Controller
    {
        private readonly ICountryService countryService;
        private readonly IMemoryCache memoryCache;
        public CountriesController(ICountryService countryService, 
                                   IMemoryCache memoryCache)
        {
            this.countryService = countryService;
            this.memoryCache = memoryCache;
        }
        /// <summary>
        /// This method returns all the available countries.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                // var countries = await countryService
                //.GetAllCountries();

                //creates cache
                var countries = this.memoryCache
                    .Get<IEnumerable<AllCountriesModel>>(CountryCacheKey);
                
                countries ??= await countryService
                        .GetAllCountries();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                this.memoryCache.Set(CountryCacheKey, countries, cacheOptions);

                return View(countries);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// This method returns a details about country with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            //check if the country is null
            if (await countryService
                .GetCountryDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var countryModel = await countryService
                .GetCountryDetailsById(id);

                //remove cache
                this.memoryCache.Remove(CountryCacheKey);

                return View(countryModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
            
        }


    }
}
