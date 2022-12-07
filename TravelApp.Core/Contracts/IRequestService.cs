using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Models.RequestModels;
using TravelApp.Data.Entities;

namespace TravelApp.Core.Contracts
{
    /// <summary>
    /// Holds Interface for Request functionality.
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// This method is used to add a request.
        /// </summary>
        /// <param name="addRequestModel"></param>
        /// <param name="currentUserId"></param>
        /// <param name="journeyId"></param>
        /// <returns></returns>
        Task Add(AddRequestModel addRequestModel, string currentUserId, int journeyId);
        /// <summary>
        /// This method returns IEnumerable of all requests.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AllRequestsModel>> GetAllRequests();
        /// <summary>
        /// This method returns IEnumerable of all requests made by login user.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        Task<IEnumerable<AllRequestsModel>> GetMyRequests(string currentUserId);
        /// <summary>
        /// This method returns a particular request with given id.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        Task<Request> GetRequestById(int requestId);
        /// <summary>
        /// This method approves a request.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        Task Approve(int requestId);
        /// <summary>
        /// This method declines a request.
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        Task Decline(int requestId);


    }
}
