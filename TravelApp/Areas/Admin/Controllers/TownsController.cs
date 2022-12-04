using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.JourneyModels;
using TravelApp.Data.Models.TownModels;
using static TravelApp.ErrorConstants.ErrorConstants.GlobalErrorConstants;


namespace TravelApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TownsController : Controller
    {
        private readonly ICountryService countryService;
        private readonly ITownService townService;

        public TownsController(ICountryService countryService, 
            ITownService townService)
        {
            this.countryService = countryService;
            this.townService = townService;
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
                await townService
                    .Add(addTownModel);

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
                await townService
                    .Edit(id, editTownModel);

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
                await townService
                    .Delete(deleteTownModel.Id);

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
