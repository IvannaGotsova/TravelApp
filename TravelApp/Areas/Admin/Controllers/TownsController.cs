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
    /// <summary>
    /// Controls adding, editing and deletion of towns.
    /// </summary>
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
        /// <summary>
        /// This method creates form for adding a town.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// This method adds a town to the database.
        /// </summary>
        /// <param name="addTownModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddTownModel addTownModel)
        {
            //check if model state is valid
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

                //remove cache 

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
        /// <summary>
        /// This method creates form for editing a town.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            //check if the town is null
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
        /// <summary>
        /// This method edits a town with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editTownModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTownModel editTownModel)
        {
            //check if the town is null
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

                //remove cache 

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
        /// <summary>
        /// This method creates form for deleting a town.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //check if the town is null
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
        /// <summary>
        /// This method deletes a town from the database.
        /// </summary>
        /// <param name="deleteTownModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteTownModel deleteTownModel)
        {
            //check if the town is null
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

                //remove cache 

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
