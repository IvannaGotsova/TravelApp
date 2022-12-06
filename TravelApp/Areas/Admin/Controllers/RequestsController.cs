using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TravelApp.Core.Contracts;

namespace TravelApp.Areas.Admin.Controllers
{/// <summary>
/// Controls all the requests - shows, approve and decline them.
/// </summary>
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RequestsController : Controller
    {
        private readonly IRequestService requestService;

        public RequestsController(IRequestService requestService)
        {
            this.requestService = requestService;
        }
        /// <summary>
        /// This method approves pending request.
        /// </summary>
        public async Task<IActionResult> Approve(int id)
        {
            //check if the request is null
            if (await requestService
                .GetRequestById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
            try
            {
                await requestService
                    .Approve(id);

                TempData["message"] = $"You have successfully approved a request!";

                return RedirectToAction("All", "Requests", new { area = "Admin" });
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// This method declines pending request.
        /// </summary>
        public async Task<IActionResult> Decline(int id)
        {
            //check if the request is null
            if (await requestService
                .GetRequestById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await requestService
                    .Decline(id);

                TempData["message"] = $"You have successfully declined a request!";

                return RedirectToAction("All", "Requests", new { area = "Admin" });
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }
        /// <summary>
        /// This method returns all the requests..
        /// </summary>
        public async Task<IActionResult> All()
        {
            
            try
            {
                var requests = await
                requestService
                .GetAllRequests();

                return View(requests);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });

            }
        }
        /// <summary>
        /// This method returns all approved requests.
        /// </summary>
        public async Task<IActionResult> AllApproved()
        {
            try
            {
                var requests = await 
                    requestService
                    .GetAllRequests();

                return View(requests
                    .Where(r => r.Status == "Approved"));
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });

            }
        }
        /// <summary>
        /// This method returns all not approved requests.
        /// </summary>
        public async Task<IActionResult> AllNotApproved()
        {
            try
            {
                var requests = await 
                    requestService
                   .GetAllRequests();

                return View(requests
                      .Where(r => r.Status == "Not Approved"));
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
            
        }
        /// <summary>
        /// This method returns all managed requests.
        /// </summary>
        public async Task<IActionResult> AllManaged()
        {
            try
            {
                var requests = await 
                    requestService
                   .GetAllRequests();

                return View(requests
                      .Where(r => r.Management == "Managed"));
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
           
        }
        /// <summary>
        /// This method returns all not managed requests.
        /// </summary>
        public async Task<IActionResult> AllNotManaged()
        {
            try
            {
                var requests = await 
                    requestService
                   .GetAllRequests();

                return View(requests
                     .Where(r => r.Management == "Not Managed"));
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
            
        }
    }
}
