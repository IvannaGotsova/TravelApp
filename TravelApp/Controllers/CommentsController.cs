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

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

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

        [HttpPost]
        public async Task<IActionResult> Add(AddCommentModel addCommentModel)
        {
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

        public async Task<IActionResult> Details(int id)
        {
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await commentService.GetCommentDetailsById(id) == null)
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

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCommentModel editCommentModel)
        {
            if (await commentService
                .GetCommentById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await commentService
                    .Edit(id, editCommentModel);

                return RedirectToAction("All", "Comments");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", somethingWrong);

                return View(editCommentModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
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

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCommentModel deleteCommentModel)
        {
            if (await commentService
                .GetCommentById(deleteCommentModel.Id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await commentService
                    .Delete(deleteCommentModel.Id);

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
