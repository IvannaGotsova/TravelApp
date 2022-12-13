using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TravelApp.Core.Contracts;

namespace TravelApp.Areas.Admin.Controllers
{
    /// <summary>
    ///  Holds all statistical information.
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InformationsController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly ICommentService commentService;
        private readonly ICountryService countryService;
        private readonly IJourneyService journeyService;
        private readonly IPostService postService;
        private readonly ITownService townService;
        private readonly ITripService tripService;
        private readonly IRequestService requestService;

        public InformationsController(IApplicationUserService applicationUserService,
                                     ICommentService commentService,
                                     ICountryService countryService,
                                     IJourneyService journeyService,
                                     IPostService postService,
                                     ITownService townService,
                                     ITripService tripService,
                                     IRequestService requestService)
        {
            this.applicationUserService = applicationUserService;
            this.commentService = commentService;
            this.countryService = countryService;
            this.journeyService = journeyService;
            this.postService = postService;
            this.townService = townService;
            this.tripService = tripService;
            this.requestService = requestService;
        }
        /// <summary>
        /// This method returns Information Index Page.
        /// </summary>
        public IActionResult Index()
        {          

            return View();
        }
        /// <summary>
        /// This method returns all the registered users.
        /// </summary>
        public async Task<IActionResult> ApplicationUsers()
        {
            var users = await
                applicationUserService
                .GetApplicationUsers();           

            return View(users);
        }

        public async Task<IActionResult> Comments()
        {

            var comments = await
                 commentService
                 .GetAllComments();

            return View(comments);
        }

        public async Task<IActionResult> Countries()
        {
 
            var countries = await
                countryService
                .GetAllCountries();

            return View(countries);
        }

        public async Task<IActionResult> Journeys()
        {
           
            var journeys = await
                journeyService
                .GetAllJourneys();

          
            return View(journeys);
        }

        public async Task<IActionResult> Posts()
        {
           

            var posts = await
                postService
                .GetAllPosts();

            return View(posts);
        }

        public async Task<IActionResult> Requests()
        {
            
            var requests = await
                requestService
                .GetAllRequests();


            return View(requests);
        }

        public async Task<IActionResult> Trips()
        {
        

            var trips = await
                tripService
                .GetTrips();

        

            return View(trips);
        }

        public async Task<IActionResult> Towns()
        {
         
            var towns = await
                townService
                .GetAllTowns();

          

            return View(towns);
        }

    }
}
