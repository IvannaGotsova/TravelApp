using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TravelApp.Core.Contracts;

namespace TravelApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RequestsController : Controller
    {
        private readonly IRequestService requestService;

        public RequestsController(IRequestService requestService)
        {
            this.requestService = requestService;
        }
        public async Task<IActionResult> Approve(int id)
        {
            if (await requestService
                .GetRequestById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }
            try
            {
                await requestService
                    .Approve(id);

                return RedirectToAction("All", "Requests", new { area = "Admin" });
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

        public async Task<IActionResult> Decline(int id)
        {

            if (await requestService
                .GetRequestById(id) == null)
            {
                return RedirectToAction("Error", "Home", new { area = "" });
            }

            try
            {
                await requestService
                    .Decline(id);

                return RedirectToAction("All", "Requests", new { area = "Admin" });
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Home", new { area = "" });
            }
        }

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
