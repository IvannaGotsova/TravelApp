using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.JourneyModels;
using TravelApp.Data.Models.PostModels;
using TravelApp.Data.Models.RequestModels;
using TravelApp.Data.Models.TownModels;
using TravelApp.Data.Models.TripModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Core.Services
{
    /// <summary>
    /// Holds Request functionality.
    /// </summary>
    public class RequestService : IRequestService
    {
        private readonly IRepository data;

        public RequestService(IRepository data)
        {
            this.data = data;
        }
        /// <summary>
        /// This method is used to add a request.
        /// </summary>
        /// <param name="addRequestModel"></param>
        /// <param name="currentUserId"></param>
        /// <param name="journeyId"></param>
        /// <returns></returns>
        public async Task Add(AddRequestModel addRequestModel, string currentUserId, int journeyId)
        {
            var currentUser = await
                this.data
                .AllReadonly<ApplicationUser>()
                .Where(au => au.Id == currentUserId)
                .FirstOrDefaultAsync();

            var journey = await
                this.data
                .AllReadonly<Journey>()
                .Where(j => j.Id == journeyId)
                .FirstOrDefaultAsync();
            //check if login user is null
            if (currentUser == null)
            {
                throw new ArgumentNullException(null, nameof(currentUser));

            }
            //check if journey is null
            if (journey == null)
            {
                throw new ArgumentNullException(null, nameof(journey));

            }

            var requestToBeAdded = new Request()
            {
                NumberOfPeople = addRequestModel.NumberOfPeople,
                JourneyId = journeyId,
                ApplicationUserId = currentUserId,
                IsApproved = false,
                IsManaged = false
            };

            await this.data.AddAsync(requestToBeAdded);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method approves a request.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task Approve(int requestId)
        {
            var request = await
                this.data
                .AllReadonly<Request>()
                .Include(r => r.Journey)
                .Where(r => r.Id == requestId)
                .FirstOrDefaultAsync();
            //check if request is null;
            if (request == null)
            {
                throw new ArgumentNullException(null, nameof(request));
            }
            //check if request is managed
            if (request.IsManaged == true)
            {
                await this.Decline(requestId);
            }
            //check if request number of people is less or equal to number of people of the journey
            if (request.NumberOfPeople <= request.Journey!.NumberOfPeople)
            {
                request.IsApproved = true;
                request.Journey.NumberOfPeople -= request.NumberOfPeople;
                request.IsManaged = true;

                this.data.Update<Request>(request);
                await this.data.SaveChangesAsync();
            }
            else
            {
                await this.Decline(requestId);
            }

        }
        /// <summary>
        /// This method declines a request.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task Decline(int requestId)
        {
            var request = await
                this.data
                .AllReadonly<Request>()
                .Where(r => r.Id == requestId)
                .FirstOrDefaultAsync();
            //check if the request is null
            if (request == null)
            {
                throw new ArgumentNullException(null, nameof(request));
            }

            request.IsManaged = true;

            this.data.Update<Request>(request);
            await this.data.SaveChangesAsync();
        }
        /// <summary>
        /// This method returns IEnumerable of all requests.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AllRequestsModel>> GetAllRequests()
        {
            var requests = await data
               .AllReadonly<Request>()
               .Include(r => r.Journey)
               .Include(r => r.ApplicationUser)
               .ToListAsync();

            return requests
                .Select(r => new AllRequestsModel()
                {
                    Id = r.Id,                   
                    JourneyName = r.Journey!.Title,
                    FinalSum = r.ApplicationUser!.IsVIP ? (r.NumberOfPeople * r.Journey.Price) * 90/100 : r.NumberOfPeople * r.Journey.Price,
                    Status = r.IsApproved is true ? "Approved" : "Not Approved",
                    Management = r.IsManaged is true ? "Managed" : "Not Managed",
                    JourneyId = r.JourneyId,
                    ApplicationUserId = r.ApplicationUser.UserName.ToString()
                })
                .ToList();
        }
        /// <summary>
        /// This method returns IEnumerable of all requests made by login user.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AllRequestsModel>> GetMyRequests(string currentUserId)
        {
            var requests = await 
                this.data
               .AllReadonly<Request>()
               .Where(r => r.ApplicationUserId == currentUserId)
               .Include(r => r.Journey)
               .Include(r => r.ApplicationUser)
               .ToListAsync();

            return requests
                .Select(r => new AllRequestsModel()
                {
                    Id = r.Id,
                    JourneyName = r.Journey!.Title,
                    FinalSum = r.ApplicationUser!.IsVIP ? (r.NumberOfPeople * r.Journey.Price) * 90 / 100 : r.NumberOfPeople * r.Journey.Price,
                    Status = r.IsApproved is true ? "Approved" : "Not Approved",
                    Management = r.IsManaged is true ? "Managed" : "Not Managed",
                    JourneyId = r.JourneyId
                })
                .ToList();
        }
        /// <summary>
        /// This method returns a particular request with given id.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task<Request> GetRequestById(int requestId)
        {
            var request = await
                this.data
                .AllReadonly<Request>()
                .Where(r => r.Id == requestId)
                .FirstOrDefaultAsync();
            //check if request is null
            if (request == null)
            {
                throw new ArgumentNullException(null, nameof(request));
            }

            return request;
        }
    }
}
