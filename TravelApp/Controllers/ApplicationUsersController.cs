using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection.Metadata;
using TravelApp.Common;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.ApplicationUser;
using static TravelApp.ErrorConstants.ErrorConstants.UserErrorConstants;

namespace TravelApp.Controllers
{
   /// <summary>
   /// Controls user functionalities.
   /// </summary>
    public class ApplicationUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
       
        public ApplicationUsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        /// <summary>
        /// This method creates form to register a user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            //check if the user is login already
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            RegisterModelView modelToBeRegistered = new RegisterModelView();

            return View(modelToBeRegistered);
        }
        /// <summary>
        /// This method is used to register user.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModelView modelToBeRegistered)
        {
            //check if the model state is valid
            if (!ModelState.IsValid)
            {
                return View(modelToBeRegistered);
            }
            //creates new ApplicationUser
            ApplicationUser userToBeRegistered = new ApplicationUser()
            {
                UserName = modelToBeRegistered.UserName,
                Email = modelToBeRegistered.Email,
                FirstName = modelToBeRegistered.FirstName,
                LastName= modelToBeRegistered.LastName
            };

            var resultUserToBeRegistered = await userManager
                .CreateAsync(userToBeRegistered, modelToBeRegistered.Password);
            //check if the user registration is correct
            if (!resultUserToBeRegistered.Succeeded)
            {
                foreach (var error in resultUserToBeRegistered.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(modelToBeRegistered);
            }

           

            return RedirectToAction("Login", "ApplicationUsers");
        }

        /// <summary>
        /// This method creates form for login.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
           
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin"});
            }

            //check if the user is login already 
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }

            LoginModelView modelToBeLogin = new LoginModelView();

            TempData["message"] = $"Hello! Have a great time!";

            return View(modelToBeLogin);
        }
        /// <summary>
        /// This method is used to login user.
        /// </summary>
        /// <param name="modelToBeLogin"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModelView modelToBeLogin)
        {   //check if the model state is valid
            if (!ModelState.IsValid)
            {
                return View(modelToBeLogin);
            }

            var userToBeLogin = await userManager
                .FindByNameAsync(modelToBeLogin.UserName);
            //check if the user is null
            if (userToBeLogin != null)
            {
                var resultUserToBeLogin = await signInManager
                    .PasswordSignInAsync(userToBeLogin, modelToBeLogin.Password, true, false);

                if (resultUserToBeLogin.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
               
            }

            ModelState.AddModelError("", invalidLogin);

            return View(modelToBeLogin);

        }
        /// <summary>
        /// This method is used to logout user
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            TempData["message"] = $"Goodbye! We are waiting for you to come back";

            return RedirectToAction("Index", "Home");
        }

    }

}
