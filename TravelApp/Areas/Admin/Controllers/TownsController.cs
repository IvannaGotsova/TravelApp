using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.JourneyModels;
using TravelApp.Data.Models.TownModels;
using static TravelApp.ErrorConstants.ErrorConstants.GlobalErrorConstants;
using static TravelApp.Constants.CacheConstants;

namespace TravelApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TownsController : Controller
    {
        private readonly ICountryService countryService;
        private readonly ITownService townService;
        private readonly IMemoryCache memoryCache;

        public TownsController(ICountryService countryService, 
                               ITownService townService, 
                               IMemoryCache memoryCache)
        {
            this.countryService = countryService;
            this.townService = townService;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var modelTown = new AddTownModel()
            {
                Countries = await 
                countryService.GetCountriesForSelect()
            };

            return View(modelTown);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTownModel addTownModel)
        {
            if (!ModelState.IsValid)
            {
                addTownModel.Countries = await 
                    countryService.GetCountriesForSelect();

                return View(addTownModel);
            }

            try
            {
                TempData["message"] = $"You have successfully added a town!";
                
                await townService
                    .Add(addTownModel);

                this.memoryCache.Remove(CountryCacheKey);

                return RedirectToAction("All", "Towns", new { area = "" });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                addTownModel.Countries = await 
                    countryService.GetCountriesForSelect();

                return View(addTownModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await townService
                .GetTownDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var editFormModel = await
               townService
               .EditCreateForm(id);

                editFormModel.Countries = await 
                    countryService.GetCountriesForSelect();

                return View(editFormModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTownModel editTownModel)
        {
            if (await townService
                .GetTownById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                TempData["message"] = $"You have successfully edited a town!";
             
                await townService
                    .Edit(id, editTownModel);

                this.memoryCache.Remove(CountryCacheKey);

                return RedirectToAction("All", "Towns", new { area = "" });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                editTownModel.Countries = await 
                    countryService.GetCountriesForSelect();

                return View(editTownModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await townService
                .GetTownDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var editFormModel = await
               townService
               .DeleteCreateForm(id);

                return View(editFormModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteTownModel deleteTownModel)
        {
            if (await townService
                .GetTownById(deleteTownModel.Id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                TempData["message"] = $"You have successfully deleted a town!";
                
                await townService
                    .Delete(deleteTownModel.Id);

                this.memoryCache.Remove(CountryCacheKey);

                return RedirectToAction("All", "Towns", new { area = ""});
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(deleteTownModel);
            }
        }
    }
}
