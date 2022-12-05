using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Common;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.PostModels;
using TravelApp.Data.Models.RequestModels;
using static TravelApp.ErrorConstants.ErrorConstants.GlobalErrorConstants;
using static TravelApp.ErrorConstants.ErrorConstants.RequestErrorConstants;

namespace TravelApp.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly IRequestService requestService;
        private readonly IJourneyService journeyService;


        public RequestsController(IRequestService requestService, 
                                  IJourneyService journeyService)
        {
            this.requestService = requestService;
            this.journeyService = journeyService;
        }

        public async Task<IActionResult> Mine()
        {
            string currentUserId = 
                this.User.GetCurrentUserId();

            try
            {
                var requests = await requestService
                .GetMyRequests(currentUserId);

                return View(requests);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        
        [HttpGet]
        public IActionResult Add(int id)
        {

            var modelRequest = new AddRequestModel()
            {
                JourneyId = id
            };

            return View(modelRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRequestModel addRequestModel, int id)
        {
             addRequestModel.ApplicationUserId = 
                this.User.GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                addRequestModel.JourneyId = id;

                return View(addRequestModel);
            }

            var journey = await journeyService
                .GetJourneyDetailsById(id);

            if (journey == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            if (addRequestModel.NumberOfPeople > journey.NumberOfPeople)
            {
                ModelState.AddModelError("", wrongNumberOfPeople);

                addRequestModel.JourneyId = id;

                return View(addRequestModel);
            }

            try
            {
                await requestService
                    .Add(addRequestModel, addRequestModel.ApplicationUserId, id);

                TempData["message"] = $"You have successfully made a request!";

                return RedirectToAction("All", "Journeys");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                addRequestModel.JourneyId = id;

                return View(addRequestModel);
            }
        }

    }
}
