using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Core.Contracts;
using TravelApp.Data.Entities;
using TravelApp.Data.Models.CountryModels;
using TravelApp.Data.Models.PostModels;
using TravelApp.Data.Models.TownModels;
using TravelApp.Data.Models.TripModels;
using TravelApp.Data.Repositories;

namespace TravelApp.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository data;

        public PostService(IRepository data)
        {
            this.data = data;
        }

        public async Task Add(AddPostModel addPostModel)
        {
            var postToBeAdded = new Post()
            {
                Title = addPostModel.Title,
                TripId = addPostModel.TripId,
                Description = addPostModel.Description,
                Image = addPostModel.Image            
            };

            await this.data.AddAsync(postToBeAdded);
            await this.data.SaveChangesAsync();
        }

        public async Task Delete(int postId)
        {
            await this.data.DeleteAsync<Post>(postId);
            await this.data.SaveChangesAsync();
        }

        public async Task<DeletePostModel> DeleteCreateForm(int postId)
        {
            var postToBeDeleted = await
               GetPostById(postId);

            var deletePostModel = new DeletePostModel()
            {
                Id = postToBeDeleted.Id,
                Title = postToBeDeleted.Title,
                Description = postToBeDeleted.Description,
            };

            return deletePostModel;
        }

        public async Task Edit(int postId, EditPostModel editPostModel)
        {
            var postToBeEdited = await
                     GetPostById(postId);

            postToBeEdited.Title = editPostModel.Title;
            postToBeEdited.Image = editPostModel.Image;
            postToBeEdited.Description = editPostModel.Description;

            this.data.Update(postToBeEdited);
            await this.data.SaveChangesAsync();
        }

        public async Task<EditPostModel> EditCreateForm(int postId)
        {
            var postToBeEdited = await
              GetPostById(postId);


            var editPostModel = new EditPostModel()
            {
                Title = postToBeEdited.Title,
                Description = postToBeEdited.Description,
                Image = postToBeEdited.Image,
                Trips = new List<Trip>(),

            };

            return editPostModel;
        }

        public async Task<IEnumerable<AllPostsModel>> GetAllPosts()
        {
            var posts = await data
                 .AllReadonly<Post>()
                 .Include(p => p.Trip)
                 .ToListAsync();

            return posts
                .Select(p => new AllPostsModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description, 
                    Image = p.Image,
                    TripName = p.Trip!.Title,
                    TripId = p.TripId
                })
                .ToList();
        }

        public async Task<IEnumerable<Post>> GetAllPostsByTrip(int tripId)
        {
            return await
                     this.data
                     .AllReadonly<Post>()
                     .Include(p => p.Trip)
                     .Where(p => p.TripId == tripId)
                     .ToListAsync();

        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await
               this.data
               .AllReadonly<Post>()
               .Where(p => p.Id == postId)
               .FirstOrDefaultAsync();

            if (post == null)
            {
                throw new ArgumentNullException();
            }

            return post;
        }

        public async Task<DetailsPostModel> GetPostDetailsById(int postId)
        {
           var post = await
             this.data
             .AllReadonly<Post>()
             .Include(p => p.Trip)
             .Where(p => p.Id == postId)
             .Select(t => new DetailsPostModel()
             {
                 Id = t.Id,
                 Title = t.Title,
                 TripName = t.Trip!.Title,
                 Image = t.Image,               
                 Description = t.Description,
                 ApplicationUserId = t.Trip.ApplicationUserId
             })
             .FirstOrDefaultAsync();


            if (post == null)
            {
                throw new ArgumentNullException();
            }

            return post;
        }

        public async Task<IEnumerable<Post>> GetPostsForSelect()
        {
            return await
             this.data
             .AllReadonly<Post>()
             .ToListAsync();
        }
    }
}
