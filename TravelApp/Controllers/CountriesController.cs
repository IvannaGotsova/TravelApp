using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Models.CountryModels;

namespace TravelApp.Controllers
{
    [Authorize]
    public class CountriesController : Controller
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                var countries = await countryService
               .GetAllCountries();

                return View(countries);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            if (await countryService
                .GetCountryDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var countryModel = await countryService
                .GetCountryDetailsById(id);

                return View(countryModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
            
        }


    }
}
