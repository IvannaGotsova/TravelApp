using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Models.RequestModels;
using TravelApp.Data.Entities;

namespace TravelApp.Core.Contracts
{
    public interface IRequestService
    {
        Task Add(AddRequestModel addRequestModel, string currentUserId, int journeyId);
        Task<IEnumerable<AllRequestsModel>> GetAllRequests();
        Task<IEnumerable<AllRequestsModel>> GetMyRequests(string currentUserId);
        Task<Request> GetRequestById(int requestId);
        Task Approve(int requestId);
        Task Decline(int requestId);


    }
}
