using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Caching.Memory;
using TravelApp.Core.Contracts;
using TravelApp.Core.Services;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.ApplicationUserModels;
using TravelApp.Data.Models.TownModels;
using static TravelApp.ErrorConstants.ErrorConstants.GlobalErrorConstants;
using static TravelApp.Constants.CacheConstants;

namespace TravelApp.Areas.Admin.Controllers
{
    /// <summary>
    ///  Controls all users and VIP users;
    ///  Controls deletion of users and their VIP Status;
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IApplicationUserService applicationUserService;
        

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IApplicationUserService applicationUserService       
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationUserService = applicationUserService;          
        }
        /// <summary>
        /// This method returns Admin Index Page.
        /// </summary>
        public IActionResult Index()
        {
            
            return View();
        }
        /// <summary>
        /// This method returns all the registered users.
        /// </summary>
        public async Task<IActionResult> AllUsers()
        {
            try
            {
                var allUsers = await
                    applicationUserService.GetApplicationUsers();

                return View(allUsers);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });

            }
        }
        /// <summary>
        /// This method gives user a VIP Status.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> MakeVIP(string id)
        {
            //check if the user is null
            if (await applicationUserService.GetApplicaionUserById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {            

                await applicationUserService
                    .MakeVIP(id);

                TempData["message"] = $"You have successfully added new VIP user!";

                return RedirectToAction("AllUsers", "Admin");

            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });

            }

        }
        /// <summary>
        /// This method remove VIP status of user who is already VIP.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> RemoveVIP(string id)
        {
            //check if the user is null
            if (await applicationUserService
                .GetApplicaionUserById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await applicationUserService
                    .RemoveVIP(id);

                TempData["message"] = $"You have successfully remove VIP user!";

                return RedirectToAction("VIPUsers", "Admin");

            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });

            }
           
        }
        /// <summary>
        /// This method returns all the VIP users.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> VIPUsers()
        {

            try
            {
                var vipUsers = await 
                    applicationUserService
                    .GetApplicationVIPUsers();

                return View(vipUsers);

            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });

            }
            
        }
        /// <summary>
        /// This method creates form for user with given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            //check if the user is null
            if (await applicationUserService
                .GetApplicaionUserById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                var editFormModel = await
                applicationUserService
                .DeleteCreateForm(id);

                return View(editFormModel);
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
            
        }
        /// <summary>
        /// This method deletes user with given id.
        /// </summary>
        /// <param name="allUsersModelView"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(AllUsersModelView allUsersModelView)
        {
             //check if the user is null
            if (await applicationUserService
                .GetApplicaionUserById(allUsersModelView.Id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await applicationUserService
                    .Delete(allUsersModelView.Id);

                TempData["message"] = $"You have successfully deleted user!";

                return RedirectToAction("AllUsers", "Admin", new { area = "Admin" });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something go wrong.");

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// Logout method.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await signInManager
                .SignOutAsync();

            return RedirectToAction("Index", "Home", new { area = ""});
        }
    }
}
