using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using static TravelApp.ErrorConstants.ErrorConstants.GlobalErrorConstants;
using static TravelApp.Constants.CacheConstants;

namespace TravelApp.Areas.Admin.Controllers
{
    /// <summary>
    /// Controls adding, editing and deletion of countries.
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
        /// This method creates form for adding a country.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            var modelCountry = new AddCountryModel();

            return View(modelCountry);
        }
        /// <summary>
        /// This method adds a country to the database.
        /// </summary>
        /// <param name="modelCountry"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddCountryModel modelCountry)
        {
            //check if model state is valid
            if (!ModelState.IsValid)
            {
                return View(modelCountry);
            }
            
            try
            {
                TempData["message"] = $"You have successfully added a country!";

                await countryService.Add(modelCountry);

                this.memoryCache.Remove(CountryCacheKey);

                return RedirectToAction("All", "Countries", new { area = "" });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(modelCountry);
            }
        }
        /// <summary>
        /// This method adds a country to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //check if the country is null
            if (await countryService
                .GetCountryDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var editFormModel =
                    await
                    countryService
                    .EditCreateForm(id);

                return View(editFormModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// This method edits a country with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editCountryModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCountryModel editCountryModel)
        {
            //check if the country is null
            if (await countryService
                .GetCountryById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                TempData["message"] = $"You have successfully edited a country!";

                await countryService
                    .Edit(id, editCountryModel);             

                this.memoryCache.Remove(CountryCacheKey);

                return RedirectToAction("All", "Countries", new { area = ""});
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(editCountryModel);
            }
        }
        /// <summary>
        /// This method creates form for deleting a country.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //check if the country is null
            if (await countryService
                .GetCountryDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var deleteFormModel = await
                countryService
                .DeleteCreateForm(id);

                return View(deleteFormModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// This method deletes a country from the database.
        /// </summary>
        /// <param name="deleteCountryModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCountryModel deleteCountryModel)
        {
            //check if the country is null
            if (await countryService.GetCountryById(deleteCountryModel.Id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                TempData["message"] = $"You have successfully deleted a country!";

                await countryService.Delete(deleteCountryModel.Id);

                this.memoryCache.Remove(CountryCacheKey);

                return RedirectToAction("All", "Countries", new { area = ""});
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(deleteCountryModel);
            }
        }
    }
}
