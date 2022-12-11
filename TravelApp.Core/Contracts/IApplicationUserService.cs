using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.ApplicationUserModels;
using TravelApp.Data.Models.CommentModels;

namespace TravelApp.Core.Contracts
{
    /// <summary>
    /// Holds Interface for Application user functionality.
    /// </summary>
    public interface IApplicationUserService
    {
        /// <summary>
        /// This method returns IEnumerable of all users.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AllUsersModelView>>GetApplicationUsers();
        /// <summary>
        /// This method returns IEnumerable of all VIP users.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<AllUsersModelView>> GetApplicationVIPUsers();
        /// <summary>
        /// This method returns particular user with given id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetApplicaionUserById(string userId);
        /// <summary>
        /// This method creates form for deleting a particular user with given id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<AllUsersModelView> DeleteCreateForm(string userId);
        /// <summary>
        /// This method deletes a particular user with given id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task Delete(string userId);
        /// <summary>
        /// This method gives a particular user with given id VIP Status.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task MakeVIP(string userId);
        /// <summary>
        /// This method removes VIP Status from a particular user with given id .
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task RemoveVIP(string userId);
      
    }
}
