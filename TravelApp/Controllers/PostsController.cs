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
using static TravelApp.Common.GetCurrentUser;
namespace TravelApp.Controllers
{
    /// <summary>
    /// Controls adding, editing and deleting posts.
    /// Shows all posts and details about them.
    /// </summary>
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
        /// <summary>
        /// This method returns all the available posts.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> All()
        {
            var posts = await postService
                .GetAllPosts();

            return View(posts);
        }
        /// <summary>
        /// This method creates a form for adding a post.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string userId = this.User
                .GetCurrentUserId();

            var modelPost = new AddPostModel()
            {
                Trips = await 
                tripService.GetTripsForSelect(userId),
            };

            return View(modelPost);
        }
        /// <summary>
        /// This method is used to add a post.
        /// </summary>
        /// <param name="addPostModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddPostModel addPostModel)
        {
            string userId = this.User
              .GetCurrentUserId();

            //check if the model state is valid
            if (!ModelState.IsValid)
            {
              
                addPostModel.Trips = await 
                    tripService.GetTripsForSelect(userId);

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
                    tripService.GetTripsForSelect(userId);

                return View(addPostModel);
            }
        }
        /// <summary>
        /// This method returns a details about particular post with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            //check ifthe post is null
            if (await postService.GetPostDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var postModel = await postService
               .GetPostDetailsById(id);

                //add all comments about this post
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
        /// <summary>
        /// This metod creates a form for editing a particular post with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = this.User
             .GetCurrentUserId();

            //check if the post is null
            if (await postService
                .GetPostDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var editFormModel = await
                     postService
                    .EditCreateForm(id);
                //get trips for the select 
                editFormModel.Trips = await 
                    tripService.GetTripsForSelect(userId);

                return View(editFormModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
            
        }
        /// <summary>
        /// This method is used to edit a particular post with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editPostModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditPostModel editPostModel)
        {
            string userId = this.User
          .GetCurrentUserId();

            //check if the post is null
            if (await postService
                .GetPostById(id) == null)
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
                    tripService.GetTripsForSelect(userId);

                return View(editPostModel);
            }
        }
        /// <summary>
        /// This metod creates a form for deleting a particular post with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //check if the post is null
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
        /// <summary>
        /// This method is used to delete a particular post with given id.
        /// </summary>
        /// <param name="deletePostModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(DeletePostModel deletePostModel)
        {
            //check if the post is null
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
