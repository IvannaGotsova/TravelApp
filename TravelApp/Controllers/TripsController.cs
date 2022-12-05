using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Common;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.TownModels;
using TravelApp.Data.Models.TripModels;
using static TravelApp.ErrorConstants.ErrorConstants.GlobalErrorConstants;
using static TravelApp.Common.GetCurrentUser;

namespace TravelApp.Controllers
{
    [Authorize]
    public class TripsController : Controller
    {
        private readonly ITripService tripService;
        private readonly IJourneyService journeyService;
        private readonly IPostService postService;

        public TripsController(ITripService tripService, 
                               IJourneyService journeyService,
                               IPostService postService)
        {
            this.tripService = tripService;
            this.journeyService = journeyService;
            this.postService = postService;
        }

        public async Task<IActionResult> All()
        {
            string currentUserId = this.User
                .GetCurrentUserId();

            try
            {
                var trips = await tripService
                .GetAllTrips(currentUserId);

                return View(trips);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var modelTrip = new AddTripModel()
            {
                Journeys = await journeyService
                .GetJourneysForSelect()
            };

            return View(modelTrip);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTripModel addTripModel)
        {
            string currentUserId = this.User
                .GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                addTripModel.Journeys = await 
                    journeyService
                    .GetJourneysForSelect();

                return View(addTripModel);
            }

            try
            {
                await tripService
                    .Add(addTripModel, currentUserId);

                TempData["message"] = $"You have successfully added a trip!";

                return RedirectToAction("All", "Trips");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                addTripModel.Journeys = await 
                    journeyService.GetJourneysForSelect();

                return View(addTripModel);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            if (await tripService.GetTripDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            var tripOwner = await tripService.GetTripDetailsById(id);
           
            if (tripOwner.ApplicationUser != 
                this.User.GetCurrentUserId())
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
            
            try
            {
                var tripModel = await tripService
                .GetTripDetailsById(id);

                tripModel.PostsAboutTrip = await
                    postService
                    .GetAllPostsByTrip(id);

                return View(tripModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await tripService
                .GetTripDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var editFormModel = await
                tripService
                .EditCreateForm(id);

                editFormModel.Journeys = await 
                    journeyService.GetJourneysForSelect();

                return View(editFormModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTripModel editTripModel)
        {
            if (await tripService
                .GetTripById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await tripService
                    .Edit(id, editTripModel);

                TempData["message"] = $"You have successfully edited a trip!";

                return RedirectToAction("All", "Trips");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                editTripModel.Journeys = await 
                    journeyService.GetJourneysForSelect();

                return View(editTripModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await tripService
                .GetTripDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var deleteFormModel = await
                tripService
                .DeleteCreateForm(id);

                return View(deleteFormModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCommentModel deleteCommentModel)
        {
            if (await tripService
                .GetTripById(deleteCommentModel.Id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await tripService
                    .Delete(deleteCommentModel.Id);

                TempData["message"] = $"You have successfully deleted a trip!";

                return RedirectToAction("All", "Trips");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(deleteCommentModel);
            }
        }
    }
}

