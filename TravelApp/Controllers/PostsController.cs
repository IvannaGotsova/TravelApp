using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Common;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.PostModels;
using TravelApp.Data.Models.TownModels;
using TravelApp.Data.Models.TripModels;
using static TravelApp.ErrorConstants.ErrorConstants.GlobalErrorConstants;

namespace TravelApp.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly ITripService tripService;
        private readonly ICommentService commentService;


        public PostsController (IPostService postService, 
                                ITripService tripService,
                                ICommentService commentService)
        {
            this.postService = postService;
            this.tripService = tripService;
            this.commentService = commentService;
        }

        public async Task<IActionResult> All()
        {
            var posts = await postService
                .GetAllPosts();

            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var modelPost = new AddPostModel()
            {
                Trips = await 
                tripService.GetTripsForSelect(),
            };

            return View(modelPost);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPostModel addPostModel)
        {
            if (!ModelState.IsValid)
            {
                addPostModel.Trips = await 
                    tripService.GetTripsForSelect();

                return View(addPostModel);
            }

            try
            {
                await postService
                    .Add(addPostModel);

                TempData["message"] = $"You have successfully added a post!";

                return RedirectToAction("All", "Posts");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                addPostModel.Trips = await 
                    tripService.GetTripsForSelect();

                return View(addPostModel);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            if (await postService.GetPostDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var postModel = await postService
               .GetPostDetailsById(id);

                postModel.CommentsAboutPost = await
                   commentService
                   .GetAllCommentsByPost(id);

                return View(postModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await postService.GetPostDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var editFormModel = await
                     postService
                    .EditCreateForm(id);

                editFormModel.Trips = await 
                    tripService.GetTripsForSelect();

                return View(editFormModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPostModel editPostModel)
        {
            if (await postService.GetPostById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await postService
                    .Edit(id, editPostModel);

                TempData["message"] = $"You have successfully edited a post!";

                return RedirectToAction("All", "Posts");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                editPostModel.Trips = await 
                    tripService.GetTripsForSelect();

                return View(editPostModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await postService
                .GetPostDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var deleteFormModel = await
                postService
                .DeleteCreateForm(id);

                return View(deleteFormModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeletePostModel deletePostModel)
        {
            if (await postService
                .GetPostById(deletePostModel.Id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await postService
                    .Delete(deletePostModel.Id);

                TempData["message"] = $"You have successfully deleted a post!";

                return RedirectToAction("All", "Posts");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(deletePostModel);
            }
        }
    }
}
