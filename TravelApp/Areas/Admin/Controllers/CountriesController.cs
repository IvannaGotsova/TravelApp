using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.CountryModels;
using static TravelApp.ErrorConstants.ErrorConstants.GlobalErrorConstants;

namespace TravelApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CountriesController : Controller
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var modelCountry = new AddCountryModel();

            return View(modelCountry);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCountryModel modelCountry)
        {
            if (!ModelState.IsValid)
            {
                return View(modelCountry);
            }

            try
            {
                await countryService.Add(modelCountry);

                return RedirectToAction("All", "Countries", new { area = "" });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(modelCountry);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await countryService
                .GetCountryDetailsById(id) == null)
            {
                return BadRequest();
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

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCountryModel editCountryModel)
        {
            if (await countryService
                .GetCountryById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await countryService
                    .Edit(id, editCountryModel);

                return RedirectToAction("All", "Countries", new { area = ""});
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(editCountryModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
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

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCountryModel deleteCountryModel)
        {
            if (await countryService.GetCountryById(deleteCountryModel.Id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await countryService.Delete(deleteCountryModel.Id);

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
