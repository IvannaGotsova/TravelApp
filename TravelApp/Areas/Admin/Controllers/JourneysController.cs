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
using Microsoft.Extensions.Caching.Memory;
using static TravelApp.Constants.CacheConstants;

namespace TravelApp.Areas.Admin.Controllers
{/// <summary>
/// Controls adding, editing and deletion of journeys.
/// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class JourneysController : Controller
    {
        private readonly ICountryService countryService;
        private readonly ITownService townService;
        private readonly IJourneyService journeyService;
        private readonly IMemoryCache memoryCache;

        public JourneysController(ITownService townService,
                                  ICountryService countryService,
                                  IJourneyService journeyService, 
                                  IMemoryCache memoryCache)
        {
            this.townService = townService;
            this.countryService = countryService;
            this.journeyService = journeyService;
            this.memoryCache = memoryCache;
        }
        /// <summary>
        /// This method creates form for adding a journey.
        /// </summary>
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
        /// <summary>
        /// This method adds a journey to the database.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add(AddJourneyModel addJourneyModel)
        {
            //returns current user Id
            string currentUserId = this.User
                .GetCurrentUserId();

            addJourneyModel
                .ApplicationUserId = currentUserId;

            //check if model state is valid
            if (!ModelState.IsValid)
            {
                addJourneyModel.Countries = await 
                    countryService.GetCountriesForSelect();
                addJourneyModel.Towns = await 
                    townService.GetTownsForSelect();

                return View(addJourneyModel);
            }

            //check if start date of the journey is before today
            if (addJourneyModel.StartDate < DateTime.Now)
            {
                ModelState.AddModelError("", startBeforeToday);

                addJourneyModel.Countries = await 
                    countryService.GetCountriesForSelect();
                addJourneyModel.Towns = await 
                    townService.GetTownsForSelect();

                return View(addJourneyModel);
            }

            //check if start date of journey is greater or equal to end date of journey
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

            //check if days calculated from user start date and end date are equal to the days generated from the user
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
                TempData["message"] = $"You have successfully added a journey!";

                await journeyService.Add(addJourneyModel, currentUserId);

                this.memoryCache.Remove(JourneyCacheKey);

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
        /// <summary>
        /// This method creates form for editing a journey.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //check if the journey is null
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
        /// <summary>
        /// This method edits a journey with given id.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditJourneyModel editJourneyModel)
        {
            //check if the journey is null
            if (await journeyService
                .GetJourneyById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            //check if start date of the journey is before today
            if (editJourneyModel.StartDate < DateTime.Now)
            {
                ModelState.AddModelError("", startBeforeToday);
            }

            // check if start date of journey is greater or equal to end date of journey
            if (editJourneyModel.StartDate >= editJourneyModel.EndDate)
            {
                ModelState.AddModelError("", endBeforeStart);
            }

            TimeSpan daysOfJourney = editJourneyModel.EndDate.Subtract(editJourneyModel.StartDate);

            //check if days calculated from user start date and end date are equal to the days generated from the user
            if (daysOfJourney.Days + 1 != editJourneyModel.Days)
            {
                ModelState.AddModelError("", wrongNumberDays);

            }

            try
            {              
                TempData["message"] = $"You have successfully edited a journey!";

                await journeyService
                    .Edit(id, editJourneyModel);

                this.memoryCache.Remove(CountryCacheKey);

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
        /// <summary>
        /// This method creates form for deleting a journey.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //check if the journey is null
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
        /// <summary>
        /// This method deletes a journey from the database.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteJourneyModel deleteJourneyModel)
        {
            //returns current user Id
            string currentUserId = this.User.GetCurrentUserId();

            //check if the journey is null
            if (await journeyService
                .GetJourneyById(deleteJourneyModel.Id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                TempData["message"] = $"You have successfully deleted a journey!";

                await journeyService
                    .Delete(deleteJourneyModel.Id, currentUserId);

                this.memoryCache.Remove(CountryCacheKey);

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

