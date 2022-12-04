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
    public class RequestService : IRequestService
    {
        private readonly IRepository data;

        public RequestService(IRepository data)
        {
            this.data = data;
        }

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

            if (currentUser == null || journey == null)
            {
                throw new ArgumentNullException();

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

        public async Task Approve(int requestId)
        {
            var request = await
                this.data
                .AllReadonly<Request>()
                .Include(r => r.Journey)
                .Where(r => r.Id == requestId)
                .FirstOrDefaultAsync();

            if (request == null)
            {
                throw new ArgumentNullException();
            }

            if (request.IsManaged == true)
            {
                await this.Decline(requestId);
            }

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

        public async Task Decline(int requestId)
        {
            var request = await
                this.data
                .AllReadonly<Request>()
                .Where(r => r.Id == requestId)
                .FirstOrDefaultAsync();

            if (request == null)
            {
                throw new ArgumentNullException();
            }

            request.IsManaged = true;

            this.data.Update<Request>(request);
            await this.data.SaveChangesAsync();
        }

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

        public async Task<Request> GetRequestById(int requestId)
        {
            var request = await
                this.data
                .AllReadonly<Request>()
                .Where(r => r.Id == requestId)
                .FirstOrDefaultAsync();

            if (request == null)
            {
                throw new ArgumentNullException();
            }

            return request;
        }
    }
}
