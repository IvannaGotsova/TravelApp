using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Common;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Models.PostModels;
using static TravelApp.ErrorConstants.ErrorConstants.GlobalErrorConstants;

namespace TravelApp.Controllers
{
    /// <summary>
    /// Controls adding, editing and deleting comments.
    /// Shows all comments and details about them.
    /// </summary>
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IPostService postService;

        public CommentsController(ICommentService commentService, IPostService postService)
        {
            this.commentService = commentService;
            this.postService = postService;
        }
        /// <summary>
        /// This method returns all available comments.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> All()
        {
            try
            {
                var comments = await 
                    commentService
                   .GetAllComments();

                return View(comments);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });            }
        }
        /// <summary>
        /// This method creates a form for adding a comment.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var modelComment = new AddCommentModel()
            {
                Posts = await 
                postService.GetPostsForSelect(),                     
            };

            return View(modelComment);
        }
        /// <summary>
        /// This method is used to add a comment.
        /// </summary>
        /// <param name="addCommentModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddCommentModel addCommentModel)
        {
            //check if the model state is valid
            if (!ModelState.IsValid)
            {
                addCommentModel.Posts = await 
                    postService.GetPostsForSelect();             

                return View(addCommentModel);
            }

            try
            {
                addCommentModel.Author = this.User.
                    GetCurrentUserName();
                await commentService
                    .Add(addCommentModel);

                TempData["message"] = $"You have successfully added a comment!";

                return RedirectToAction("All", "Comments");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                addCommentModel.Posts = await 
                    postService.GetPostsForSelect();

                return View(addCommentModel);
            }

        }
        /// <summary>
        /// This method returns a details about comment with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            //check if the comment is null
            if (await commentService
                .GetCommentDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var commentModel = await
                commentService
                .GetCommentDetailsById(id);

                return View(commentModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
            
        }
        /// <summary>
        /// This metod creates a form for editing a particular comment.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //check if the comment is null
            if (await commentService
                .GetCommentDetailsById(id) == null)
            {
                return BadRequest();
            }

            try
            {
                var editFormModel = await
                       commentService
                       .EditCreateForm(id);

                return View(editFormModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
           

            
        }
        /// <summary>
        /// This method is used to edit a particular comment.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editCommentModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCommentModel editCommentModel)
        {
            //check if the comment is null
            if (await commentService
                .GetCommentById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await commentService
                    .Edit(id, editCommentModel);

                TempData["message"] = $"You have successfully edited a comment!";

                return RedirectToAction("All", "Comments");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(editCommentModel);
            }
        }
        /// <summary>
        /// This metod creates a form for deleting a particular comment.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            //check if the comment is null
            if (await commentService
                .GetCommentDetailsById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var editFormModel = await
               commentService
               .DeleteCreateForm(id);

                return View(editFormModel);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
           
        }
        /// <summary>
        /// This method is used to delete a particular comment.
        /// </summary>
        /// <param name="deleteCommentModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCommentModel deleteCommentModel)
        {
            //check if the comment is null
            if (await commentService
                .GetCommentById(deleteCommentModel.Id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await commentService
                    .Delete(deleteCommentModel.Id);

                TempData["message"] = $"You have successfully deleted a comment!";

                return RedirectToAction("All", "Comments");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(deleteCommentModel);
            }
        }
    } 
}
