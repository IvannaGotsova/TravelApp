using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Common;
using TravelApp.Core.Contracts;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.JourneyModels;
using static TravelApp.ErrorConstants.ErrorConstants.GlobalErrorConstants;
using static TravelApp.ErrorConstants.ErrorConstants.JourneyErrorConstants;
using static TravelApp.Common.GetCurrentUser;

namespace TravelApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class JourneysController : Controller
    {
        private readonly ICountryService countryService;
        private readonly ITownService townService;
        private readonly IJourneyService journeyService;

        public JourneysController(ITownService townService,
                                  ICountryService countryService,
                                  IJourneyService journeyService)
        {
            this.townService = townService;
            this.countryService = countryService;
            this.journeyService = journeyService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var modelJourney = new AddJourneyModel()
            {              
                Countries = await countryService.GetCountriesForSelect(),
                Towns = await townService.GetTownsForSelect()
            };            

            return View(modelJourney);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddJourneyModel addJourneyModel)
        {
            string currentUserId = this.User
                .GetCurrentUserId();

            addJourneyModel
                .ApplicationUserId = currentUserId;

            if (!ModelState.IsValid)
            {
                addJourneyModel.Countries = await 
                    countryService.GetCountriesForSelect();
                addJourneyModel.Towns = await 
                    townService.GetTownsForSelect();

                return View(addJourneyModel);
            }

            if (addJourneyModel.StartDate < DateTime.Now)
            {
                ModelState.AddModelError("", startBeforeToday);

                addJourneyModel.Countries = await 
                    countryService.GetCountriesForSelect();
                addJourneyModel.Towns = await 
                    townService.GetTownsForSelect();

                return View(addJourneyModel);
            }

            if (addJourneyModel.StartDate >= addJourneyModel.EndDate)
            {
                ModelState.AddModelError("", endBeforeStart);

                addJourneyModel.Countries = await 
                    countryService.GetCountriesForSelect();
                addJourneyModel.Towns = await 
                    townService.GetTownsForSelect();

                return View(addJourneyModel);
            }

            TimeSpan daysOfJourney = addJourneyModel.EndDate.Subtract(addJourneyModel.StartDate);

            if (daysOfJourney.Days + 1 != addJourneyModel.Days)
            {
                ModelState.AddModelError("", wrongNumberDays);

                addJourneyModel.Countries = await 
                    countryService.GetCountriesForSelect();
                addJourneyModel.Towns = await 
                    townService.GetTownsForSelect();

                return View(addJourneyModel);

            }


            try
            {

                await journeyService.Add(addJourneyModel, currentUserId);

                return RedirectToAction("All", "Journeys", new { area = "" });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                addJourneyModel.Countries = await 
                    countryService.GetCountriesForSelect();
                addJourneyModel.Towns = await 
                    townService.GetTownsForSelect();

                return View(addJourneyModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await journeyService
                .GetJourneyDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var editFormModel = await
               journeyService
               .EditCreateForm(id);

                editFormModel.Countries = await 
                    countryService.GetCountriesForSelect();
                editFormModel.Towns = await 
                    townService.GetTownsForSelect();

                return View(editFormModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditJourneyModel editJourneyModel)
        {
            if (await journeyService
                .GetJourneyById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            if (editJourneyModel.StartDate < DateTime.Now)
            {
                ModelState.AddModelError("", startBeforeToday);
            }

            if (editJourneyModel.StartDate >= editJourneyModel.EndDate)
            {
                ModelState.AddModelError("", endBeforeStart);
            }

            TimeSpan daysOfJourney = editJourneyModel.EndDate.Subtract(editJourneyModel.StartDate);

            if (daysOfJourney.Days + 1 != editJourneyModel.Days)
            {
                ModelState.AddModelError("", wrongNumberDays);

            }

            try
            {
                await journeyService
                    .Edit(id, editJourneyModel);

                return RedirectToAction("All", "Journeys", new { area = "" });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                editJourneyModel.Countries = await countryService.GetCountriesForSelect();
                editJourneyModel.Towns = await townService.GetTownsForSelect();

                return View(editJourneyModel);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await journeyService.GetJourneyDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var deleteFormModel = await
               journeyService
               .DeleteCreateForm(id);

                return View(deleteFormModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteJourneyModel deleteJourneyModel)
        {
            string currentUserId = this.User.GetCurrentUserId();

            if (await journeyService
                .GetJourneyById(deleteJourneyModel.Id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await journeyService
                    .Delete(deleteJourneyModel.Id, currentUserId);

                return RedirectToAction("All", "Journeys", new { area = "" });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(deleteJourneyModel);
            }
        }
    }
}

