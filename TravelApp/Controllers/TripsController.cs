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
    /// <summary>
    /// Controls adding, editing and deleting trips.
    /// Shows all trips of the login user.
    /// </summary>
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
        /// <summary>
        /// This method returns all trips of the login user.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> All()
        {
            //returns id of the login user
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
        /// <summary>
        /// This method is used to create form for adding a trip.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// This method is used to add a trip to the trips of the current user.
        /// </summary>
        /// <param name="addTripModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddTripModel addTripModel)
        {
            //returns id of the login user
            string currentUserId = this.User
                .GetCurrentUserId();
            //check if the model state is valid
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
        /// <summary>
        /// This method creates form for deleting a particular trip with given trip.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            //check if the trip is null
            if (await tripService
                .GetTripDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            var tripOwner = await 
                tripService.GetTripDetailsById(id);
           
            //check if the login user is the owner of the trip
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
        /// <summary>
        /// This method creates a form for editing a particular town with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //check if the trip is null
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

                //gets journeys for select
                editFormModel.Journeys = await 
                    journeyService
                    .GetJourneysForSelect();

                return View(editFormModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// This method is used to edit a particular trip with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editTripModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTripModel editTripModel)
        {
            //check if the trip is null
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
        /// <summary>
        /// This method is used to create a form for deleting a particular trip with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //check if the trip is null
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
        /// <summary>
        /// This method is used to delete a particular trip with given id.
        /// </summary>
        /// <param name="deleteCommentModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCommentModel deleteCommentModel)
        {
            //check if the trip is null
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

