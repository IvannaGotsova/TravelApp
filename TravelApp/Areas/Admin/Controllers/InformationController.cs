//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Data;
//using TravelApp.Core.Contracts;

//namespace TravelApp.Areas.Admin.Controllers
//{
//    /// <summary>
//    ///  Holds all statistical information.
//    /// </summary>
//    [Area("Admin")]
//    [Authorize(Roles = "Admin")]
//    public class InformationController : Controller
//    {
//        private readonly IApplicationUserService applicationUserService;
//        private readonly ICommentService commentService;
//        private readonly ICountryService countryService;
//        private readonly IJourneyService journeyService;
//        private readonly IPostService postService;
//        private readonly ITownService townService;
//        private readonly ITripService tripService;
//        private readonly IRequestService requestService;

//        public InformationController(IApplicationUserService applicationUserService, 
//                                     ICommentService commentService, 
//                                     ICountryService countryService, 
//                                     IJourneyService journeyService, 
//                                     IPostService postService, 
//                                     ITownService townService, 
//                                     ITripService tripService, 
//                                     IRequestService requestService)
//        {
//            this.applicationUserService = applicationUserService;
//            this.commentService = commentService;
//            this.countryService = countryService;
//            this.journeyService = journeyService;
//            this.postService = postService;
//            this.townService = townService;
//            this.tripService = tripService;
//            this.requestService = requestService;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var users = await
//                applicationUserService
//                .GetApplicationUsers();

//            var comments = await
//                 commentService
//                 .GetAllComments();

//            var countries = await
//                countryService
//                .GetAllCountries();

//            var journeys = await
//                journeyService
//                .GetAllJourneys();

//            var requests = await
//                requestService
//                .GetAllRequests();

//            var towns = await
//                townService
//                .GetAllTowns();

//            var trips = await
//                tripService
//                .GetTrips();

//            var posts = await
//                postService
//                .GetAllPosts();
//            return View(users, comments, );
//        }
//    }
//}
