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
    /// <summary>
    /// Controls adding a request.
    /// Shows all requests of the login user.
    /// </summary>
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
        /// <summary>
        /// This method returns all the requests of the login user.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Mine()
        {
            //returns id of the login user
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

        /// <summary>
        /// This method creates form for adding a request.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add(int id)
        {

            var modelRequest = new AddRequestModel()
            {
                JourneyId = id
            };

            return View(modelRequest);
        }
        /// <summary>
        /// This method is used to add a request.
        /// </summary>
        /// <param name="addRequestModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddRequestModel addRequestModel, int id)
        {
            //returns id of login user
             addRequestModel.ApplicationUserId = 
                this.User.GetCurrentUserId();

            //check if the model state is valid
            if (!ModelState.IsValid)
            {
                addRequestModel.JourneyId = id;

                return View(addRequestModel);
            }

            var journey = await journeyService
                .GetJourneyDetailsById(id);

            //check if the journey is null
            if (journey == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
            //check if the number of people requested of the user is greater or equal to the available 
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
