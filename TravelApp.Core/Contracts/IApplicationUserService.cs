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
    public interface IApplicationUserService
    {
        Task<IEnumerable<AllUsersModelView>>GetApplicationUsers();
        Task<IEnumerable<AllUsersModelView>> GetApplicationVIPUsers();

        Task<ApplicationUser> GetApplicaionUserById(string userId);
        Task<AllUsersModelView> DeleteCreateForm(string userId);
        Task Delete(string userId);
        Task MakeVIP(string userId);
        Task RemoveVIP(string userId);

    }
}
