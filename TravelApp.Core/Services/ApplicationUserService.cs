using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.ApplicationUserModels;
using TravelApp.Data.Models.CommentModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Core.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IRepository data;

        public ApplicationUserService(IRepository data)
        {
            this.data = data;
        }

        public async Task Delete(string userId)
        {
            await this.data
                .DeleteAsync<ApplicationUser>(userId);
            await this.data
                .SaveChangesAsync();
        }

        public async Task<AllUsersModelView> DeleteCreateForm(string userId)
        {
            var userToBeDeleted = await
                GetApplicaionUserById(userId);

            var deleteApplicationUserModel = new AllUsersModelView()
            {
                Id = userToBeDeleted!.Id,
                UserName = userToBeDeleted!.UserName,
                FirstName = userToBeDeleted!.FirstName,
                LastName = userToBeDeleted!.LastName,
            };

            return deleteApplicationUserModel;
        }

        public async Task<ApplicationUser> GetApplicaionUserById(string userId)
        {
            if (await this.data
                .GetByIdAsync<ApplicationUser>(userId) == null)
            {
                throw new ArgumentNullException();
            }

            return await
               this.data
               .AllReadonly<ApplicationUser>()
               .Where(au => au.Id == userId)
               .FirstAsync();
        }

        public async Task<IEnumerable<AllUsersModelView>> GetApplicationUsers()
        {
            var allUsers = await
                this.data
                .AllReadonly<ApplicationUser>()
                .Include(a => a.Trips)
                .ToListAsync();
        
            return allUsers
                .Select(u => new AllUsersModelView()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    TripsCount = u.Trips.Count(),
                    IsVIP = u.IsVIP is true ? "VIP" : "Regular"
                    
                })
                .ToList();

        }

        public async Task<IEnumerable<AllUsersModelView>> GetApplicationVIPUsers()
        {
            var vipUsers = await
               this.data
               .AllReadonly<ApplicationUser>()
               .Where(au => au.IsVIP == true)
               .Include(a => a.Trips)
               .ToListAsync();

            return vipUsers
                .Select(u => new AllUsersModelView()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    TripsCount = u.Trips.Count(),
                    IsVIP = u.IsVIP is true ? "VIP" : "Regular"

                })
                .ToList();
        }

        public async Task MakeVIP(string userId)
        {
           
            var user = await
                this.data
                .AllReadonly<ApplicationUser>()
                .Where(au => au.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            user.IsVIP = true;

            this.data.Update<ApplicationUser>(user);
            await this.data.SaveChangesAsync();
        }

        public async Task RemoveVIP(string userId)
        {          
            var user = await
                this.data
                .AllReadonly<ApplicationUser>()
                .Where(au => au.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            user.IsVIP = false;

            this.data.Update<ApplicationUser>(user);
            await this.data.SaveChangesAsync();
        }

    }
}
